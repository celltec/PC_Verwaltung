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
        // Liste mit allen Computern, die dem derzeitigen Benutzer zugewiesen sind
        private List<Computer> Computers;

        // Xml Konverter Objekt
        private readonly XmlSerializer Xml;

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

            Computers = new List<Computer>();
            Xml = new XmlSerializer(typeof(List<Computer>), new XmlRootAttribute(User.CurrentUser.UserName));

            Computers.Add(new Computer(1, "PC1", "00:80:41:AE:FD:7E")); // for debugging
            Computers.Add(new Computer(2, "PC2", "60:57:18:E6:49:03")); // for debugging
            Computers.Add(new Computer(3, "PC3", "00:1B:44:11:3A:B7")); // for debugging

            // SaveConfiguration(); // for debugging
            // Close(); // for debugging

        }

        // Loads a configuration from a .xml file
        private void OpenConfiguration()
        {
            OpenFileDialog XmlFileDialog = new OpenFileDialog() { Filter = "XML Dateien (*.xml)|*.xml" };

            // Ask for the location of the file
            if (XmlFileDialog.ShowDialog() == true)
            {
                // Create reader object
                FileStream Reader = new FileStream(XmlFileDialog.FileName, FileMode.Open, FileAccess.Read);

                try
                {
                    // Transfer the data from the XML format to the objects
                    Computers = (List<Computer>)Xml.Deserialize(Reader);
                }
                catch (Exception e)
                {
                    // Show error message if something went wrong
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // Cleanup
                    Reader.Close();
                }
            }
        }

        // Saves the current configuration to a .xml file
        private void SaveConfiguration()
        {
            SaveFileDialog XmlFileDialog = new SaveFileDialog{ Filter = "XML Dateien (*.xml)|*.xml" };

            // Ask where to save the file
            if (XmlFileDialog.ShowDialog() == true)
            {
                // Create writer object
                FileStream Writer = new FileStream(XmlFileDialog.FileName, FileMode.Create, FileAccess.Write);

                // Clear namespace clutter for better looks
                XmlSerializerNamespaces EmptyNameSpace = new XmlSerializerNamespaces();
                EmptyNameSpace.Add(string.Empty, string.Empty);

                try
                {
                    // Transfer the data from the objects into the XML format
                    Xml.Serialize(Writer, Computers, EmptyNameSpace);
                }
                catch (Exception e)
                {
                    // Show error message if something went wrong
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // Clean up
                    Writer.Close();
                }
            }
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
