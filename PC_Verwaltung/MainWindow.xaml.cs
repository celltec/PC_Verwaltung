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

        public void Navigate(UserControl content, bool centerWindow = false)
        {
            Content = content;

            if (centerWindow)
            {
                CenterOnScreen();
            }
        }

        private void CenterOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}
