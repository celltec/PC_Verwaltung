using System.Windows;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Manager beinhaltet die Logik für die Eingabe von Daten und Einstellungen.
    /// Dieses Fenster ist das Hauptfenster in dem sich nach der Anmeldung alles abspielt.
    /// </Zusammenfassung>
    public partial class Manager : Window
    {
        public Manager()
        {
            InitializeComponent();

            // for debugging
            User.CurrentUser = Register.Users[0];

            /*
            // Anmeldung aufrufen
            if (new Entry().ShowDialog() == false)
            {
                // Anwendung schließen, wenn niemand angemeldet wurde
                Close();
            }
            */
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

        // Event handler zum Schließen der Anwendung
        private void CloseClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
