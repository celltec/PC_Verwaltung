using System.Windows;
using System.Windows.Controls;

namespace PC_Verwaltung
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Switcher.Window = this;
            Switcher.Switch(new Login());
        }

        public void Navigate(UserControl content)
        {
            Content = content;
        }
    }
}
