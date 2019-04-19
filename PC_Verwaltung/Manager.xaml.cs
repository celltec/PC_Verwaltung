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

        private FileDialog XmlFileDialog;

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
            //*/

            User.CurrentUser = Register.Users[0]; // for debugging

            Xml = new XmlSerializer(typeof(List<Computer>), new XmlRootAttribute(User.CurrentUser.UserName));


            // SaveConfiguration(); // for debugging
            // Close(); // for debugging
        }

        private void UpdateConfiguration(List<Computer> computers)
        {
            ComputerViewList.Children.Clear();

            // Konvertiere die Daten der XML Datei in Computer Objekte und platziere sie in der Anzeigeliste
            foreach (Computer computer in computers)
            {
                ComputerView view = new ComputerView();
                view.TextBoxId.Text = computer.ComputerId.ToString();
                view.TextBoxName.Text = computer.ComputerName;
                view.TextBoxMac.Text = computer.MacAddress;
                ComputerViewList.Children.Add(view);
            }
        }

        // Läd eine Konfiguration aus einer .xml Datei
        private void OpenConfiguration()
        {
            XmlFileDialog = new OpenFileDialog() { Filter = "XML Dateien (*.xml)|*.xml" };

            // Fragen welche Datei geöffnet werden soll
            if (XmlFileDialog.ShowDialog() == true)
            {
                // Erstelle Leseobjekt
                FileStream Reader = new FileStream(XmlFileDialog.FileName, FileMode.Open, FileAccess.Read);

                try
                {
                    ComputerViewList.Children.Clear();

                    // Konvertiere die Daten der XML Datei in Computer Objekte und platziere sie in der Anzeigeliste
                    foreach (Computer computer in (List<Computer>)Xml.Deserialize(Reader))
                    {
                        ComputerView view = new ComputerView();
                        view.TextBoxId.Text = computer.ComputerId.ToString();
                        view.TextBoxName.Text = computer.ComputerName;
                        view.TextBoxMac.Text = computer.MacAddress;
                        view.Delete += new ComputerView.DeleteHandler(DeleteComputer);
                        ComputerViewList.Children.Add(view);
                    }
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

        // Schreibt die derzeitige Konfiguration in eine .xml Datei
        private void SaveConfiguration()
        {
            XmlFileDialog = new SaveFileDialog{ Filter = "XML Dateien (*.xml)|*.xml" };

            // Fragen wo die Datei gespeichert werden soll
            if (XmlFileDialog.ShowDialog() == true)
            {
                // Liste mit allen Computern, die dem derzeitigen Benutzer zugewiesen sind
                List<Computer> Computers = new List<Computer>();

                foreach (ComputerView view in ComputerViewList.Children)
                {
                    // TODO: check for illegal characters
                    Computers.Add(new Computer(int.Parse(view.TextBoxId.Text), view.TextBoxName.Text, view.TextBoxMac.Text));
                }

                // Erstelle Schreibeobjekt
                FileStream Writer = new FileStream(XmlFileDialog.FileName, FileMode.Create, FileAccess.Write);

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
        }

        // Callback Funktion zum Löschen eines Computers (wird Event handler übergeben)
        private void DeleteComputer(ComputerView computer)
        {
            // Computer auf Liste entfernen
            ComputerViewList.Children.Remove(computer);
        }

        // Event handler zum Hinzufügen einer Konfiguration
        private void AddNewClickEvent(object sender, RoutedEventArgs e)
        {
            // Leeren Computer erstellen
            ComputerView view = new ComputerView();

            // Callback Funktion dem Löschen Event handler übergeben
            view.Delete += new ComputerView.DeleteHandler(DeleteComputer);

            // Computer an erster Stelle in die Liste einfügen
            ComputerViewList.Children.Insert(0, view);
        }

        // Event handler zum Öffnen einer Konfiguration
        private void OpenClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenConfiguration();
        }

        // Event handler zum Speichern einer Konfiguration
        private void SaveClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveConfiguration();
        }

        // Event handler zum Speichern einer Konfiguration
        private void SaveAsClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveConfiguration();
        }

        // Event handler zum Schließen der Anwendung
        private void CloseClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        // Event handler zum Anzeigen des Infofensters
        private void AboutClickEvent(object sender, System.Windows.RoutedEventArgs e)
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
