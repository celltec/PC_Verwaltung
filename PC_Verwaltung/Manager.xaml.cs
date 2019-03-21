using System;
using System.Windows.Controls;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Manager beinhaltet die Logik für das Verwalten der Geräte
    /// </Zusammenfassung>
    public partial class Manager : UserControl
    {
        public Manager()
        {
            InitializeComponent();

            // Fenster vergrößern
            Switcher.Window.Height = Convert.ToInt32(FindResource("Height")) * 1.5;
            Switcher.Window.Width = Convert.ToInt32(FindResource("Width")) * 2;
        }

        private void CloseClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Window.Close();
        }
    }
}
