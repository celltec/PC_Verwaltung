using System.Windows;
using System.Windows.Controls;

namespace PC_Verwaltung
{
    public partial class Entry : Window
    {
        public Entry()
        {
            InitializeComponent();
            Switcher.Window = this;
            Switcher.Navigate(new Login());
        }
    }
}
