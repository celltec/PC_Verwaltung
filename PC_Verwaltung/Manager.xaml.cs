using System.Windows;

namespace PC_Verwaltung
{
    public partial class Manager : Window
    {
        public Manager()
        {
            InitializeComponent();
            new Entry().ShowDialog();

            if(User.CurrentUser is null)
            {
                Close();
            }
        }

        private void CloseClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
