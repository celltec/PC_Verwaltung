﻿using System;
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

        // Dateidialog Objekt
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
            */

            User.CurrentUser = Register.Users[0]; // for debugging

            // TODO: change implementation of user assignment
            Xml = new XmlSerializer(typeof(List<Computer>), new XmlRootAttribute(User.CurrentUser.UserName));
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
                view.TextBoxId.Text = computer.ComputerId.ToString();
                view.TextBoxName.Text = computer.ComputerName;
                view.TextBoxMac.Text = computer.MacAddress;

                // Löschen Funktion dem Event handler übergeben
                view.Delete += new ComputerView.DeleteHandler(DeleteComputer);

                // Computer der Liste hinzufügen
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
                    // Konvertiere die Daten der XML Datei in Computer Objekte
                    UpdateConfiguration((List<Computer>)Xml.Deserialize(Reader));

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

        // Schreibt die derzeitige Konfiguration in die geöffnete .xml Datei
        private void SaveConfiguration()
        {
            // Liste mit allen Computern, die dem derzeitigen Benutzer zugewiesen sind
            List<Computer> Computers = new List<Computer>();

            foreach (ComputerView view in ComputerViewList.Children)
            {
                // TODO: check for empty or illegal characters
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

        // Ruft die Methode zum Speichern für eine neue .xml Datei auf
        private void SaveAsConfiguration()
        {
            XmlFileDialog = new SaveFileDialog{ Filter = "XML Dateien (*.xml)|*.xml" };

            // Fragen wo die Datei gespeichert werden soll
            if (XmlFileDialog.ShowDialog() == true)
            {
                SaveConfiguration();
            }
        }

        // Funktion zum Löschen eines Computers (wird Event handler übergeben)
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

            // Löschen Funktion dem Event handler übergeben
            view.Delete += new ComputerView.DeleteHandler(DeleteComputer);

            // Computer an erster Stelle in die Liste einfügen
            ComputerViewList.Children.Insert(0, view);
        }

        // Event handler zum Öffnen einer Konfiguration
        private void OpenClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenConfiguration();
        }

        // Event handler zum Speichern der Konfiguration
        private void SaveClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            if (XmlFileDialog == null || XmlFileDialog.FileName == "")
            {
                SaveAsConfiguration();
            }
            else
            {
                SaveConfiguration();
            }
        }

        // Event handler zum Speichern der Konfiguration in einer neuen Datei
        private void SaveAsClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveAsConfiguration();
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
