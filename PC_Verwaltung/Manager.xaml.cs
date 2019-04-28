using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Manager beinhaltet die Logik für die Eingabe von Daten und Einstellungen.
    /// Dieses Fenster ist das Hauptfenster in dem sich nach der Anmeldung alles abspielt.
    /// </Zusammenfassung>
    public partial class Manager : Window
    {
        // Xml Konverter Objekt
        private readonly XmlSerializer Xml;

        // Pfad der XML Datei
        private string XmlFilePath = "";

        public Manager()
        {
            InitializeComponent();

            /*
            // Anmeldung aufrufen
            if (new Entry().ShowDialog() == false)
            {
                // Anwendung schließen, wenn niemand angemeldet wurde
                Close();
            }
            */

            User.CurrentUser = Register.Users[0]; // for debugging

            // TODO: change implementation of user assignment
            Xml = new XmlSerializer(typeof(List<Computer>), new XmlRootAttribute(User.CurrentUser.UserName));
        }

        // Nummerierung der Computeransichten aktualisieren
        // TODO: make event triggered
        private void UpdateIndexes()
        {
            foreach (ComputerView view in ComputerViewList.Children)
            {
                view.LabelIndex.Content = (ComputerViewList.Children.IndexOf(view) + 1).ToString();
            }
        }

        // Platziere Computer Objekte in der Anzeigeliste
        private void UpdateConfiguration(List<Computer> computers)
        {
            // Liste zurücksetzen
            ComputerViewList.Children.Clear();

            foreach (Computer computer in computers)
            {
                ComputerView view = new ComputerView();

                // Inhalt setzen
                view.TextBoxName.Text = computer.Name;
                view.TextBoxMac.Text = computer.MacAddress;

                AddHandlers(view);

                // Computer der Liste hinzufügen
                ComputerViewList.Children.Add(view);
            }
        }

        // Läd eine Konfiguration aus einer XML Datei
        private void OpenConfiguration()
        {
            OpenFileDialog XmlFileDialog = new OpenFileDialog() { Filter = "XML Dateien (*.xml)|*.xml" };

            // Fragen welche Datei geöffnet werden soll
            if (XmlFileDialog.ShowDialog() == true)
            {
                // Globalen Dateipfad der XML setzen
                XmlFilePath = XmlFileDialog.FileName;

                // Erstelle Leseobjekt
                FileStream Reader = new FileStream(XmlFilePath, FileMode.Open, FileAccess.Read);

                try
                {
                    // Konvertiere die Daten der XML Datei in Computer Objekte
                    UpdateConfiguration((List<Computer>)Xml.Deserialize(Reader));
                    UpdateIndexes();
                }
                catch (Exception e)
                {
                    // Zeige eine Fehlermeldung wenn etwas schief gegangen ist
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // Leseobjekt muss geschlossen werden
                    Reader.Close();
                }
            }
        }

        // Schreibt die derzeitige Konfiguration in die geöffnete XML Datei
        private void SaveConfiguration()
        {
            // Liste mit allen Computern, die dem derzeitigen Benutzer zugewiesen sind
            List<Computer> Computers = new List<Computer>();

            foreach (ComputerView view in ComputerViewList.Children)
            {
                Computers.Add(new Computer(view.TextBoxName.Text, view.TextBoxMac.Text));
            }

            // Erstelle Schreibeobjekt
            FileStream Writer = new FileStream(XmlFilePath, FileMode.Create, FileAccess.Write);

            // Für bessere Ästhetik unnötige Angaben über den Namespace unterdrücken 
            XmlSerializerNamespaces EmptyNameSpace = new XmlSerializerNamespaces();
            EmptyNameSpace.Add(string.Empty, string.Empty);

            try
            {
                // Konvertiere die Daten der Computer Objekte in das XML Format
                Xml.Serialize(Writer, Computers, EmptyNameSpace);
            }
            catch (Exception e)
            {
                // Zeige eine Fehlermeldung wenn etwas schief gegangen ist
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Schreibeobjekt muss geschlossen werden
                Writer.Close();
            }
        }

        // Ruft die Methode zum Speichern für eine neue XML Datei auf
        private void SaveAsConfiguration()
        {
            SaveFileDialog XmlFileDialog = new SaveFileDialog{ Filter = "XML Dateien (*.xml)|*.xml" };

            // Fragen wo die Datei gespeichert werden soll
            if (XmlFileDialog.ShowDialog() == true)
            {
                // Globalen Dateipfad der XML setzen
                XmlFilePath = XmlFileDialog.FileName;
                SaveConfiguration();
            }
        }

        // Übergibt die Event Funktionen an die ComputerView Event handler
        private void AddHandlers(ComputerView view)
        {
            view.MoveUp += new ComputerView.MoveUpHandler(MoveUpComputer);
            view.MoveDown += new ComputerView.MoveDownHandler(MoveDownComputer);
            view.Delete += new ComputerView.DeleteHandler(DeleteComputer);
        }

        // Funktion zum Verschieben eines Computers nach oben (wird Event handler übergeben)
        private void MoveUpComputer(ComputerView view)
        {
            int viewIndex = ComputerViewList.Children.IndexOf(view);

            if(viewIndex > 0)
            {
                ComputerViewList.Children.Remove(view);
                ComputerViewList.Children.Insert(viewIndex - 1, view);
            }

            UpdateIndexes();
        }

        // Funktion zum Verschieben eines Computers nach unten (wird Event handler übergeben)
        private void MoveDownComputer(ComputerView view)
        {
            int viewIndex = ComputerViewList.Children.IndexOf(view);

            if (viewIndex < ComputerViewList.Children.Count - 1)
            {
                ComputerViewList.Children.Remove(view);
                ComputerViewList.Children.Insert(viewIndex + 1, view);
            }

            UpdateIndexes();
        }

        // Funktion zum Löschen eines Computers (wird Event handler übergeben)
        private void DeleteComputer(ComputerView view)
        {
            // Computer auf Liste entfernen
            ComputerViewList.Children.Remove(view);
            UpdateIndexes();
        }

        // Event handler zum Hinzufügen einer Konfiguration
        private void AddNewClickEvent(object sender, RoutedEventArgs e)
        {
            // Leeren Computer erstellen
            ComputerView view = new ComputerView();

            AddHandlers(view);

            // Computer an erster Stelle in die Liste einfügen
            ComputerViewList.Children.Insert(0, view);
            UpdateIndexes();
        }

        // Event handler zum Öffnen einer Konfiguration
        private void OpenClickEvent(object sender, RoutedEventArgs e)
        {
            OpenConfiguration();
        }

        // Event handler zum Speichern der Konfiguration in einer bereits geöffneten XML Datei
        private void SaveClickEvent(object sender, RoutedEventArgs e)
        {
            // Auswahldialog anzeigen, wenn noch keine Datei geöffnet wurde wurde
            if (XmlFilePath == "")
            {
                SaveAsConfiguration();
            }
            else
            {
                SaveConfiguration();
            }
        }

        // Event handler zum Speichern der Konfiguration in einer neuen XML Datei
        private void SaveAsClickEvent(object sender, RoutedEventArgs e)
        {
            SaveAsConfiguration();
        }

        // Event handler zum Schließen der Anwendung
        private void CloseClickEvent(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Event handler zum Anzeigen des Infofensters
        private void AboutClickEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Dies ist eine einfache PC Verwaltung\n"
                + "ohne wirklichen Nutzen, außer ein\n"
                + "hoffentlich passables Schulprojekt zu sein.\n\n"
                + "Erstellt mit Liebe von Jeremy Peters.", 
                FindResource("AppTitle").ToString());
        }
    }
}
