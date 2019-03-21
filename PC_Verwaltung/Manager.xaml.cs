using System;
using System.Windows;
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

            // Veränderung der Fenstergröße zulassen
            Switcher.Window.MaxHeight = SystemParameters.PrimaryScreenHeight;
            Switcher.Window.MaxWidth = SystemParameters.PrimaryScreenWidth;
        }

        private void CloseClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Window.Close();
        }
    }
}
