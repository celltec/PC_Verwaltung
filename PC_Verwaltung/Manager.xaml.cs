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

            // Anmeldung aufrufen
            if (new Entry().ShowDialog() == false)
            {
                // Anwendung schließen, wenn niemand angemeldet wurde
                Close();
            }
        }

        // Event handler zum schließen der Anwendung
        private void CloseClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
