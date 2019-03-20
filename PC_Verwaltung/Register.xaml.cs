using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace PC_Verwaltung
{
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        // Event handler für den Zurückknopf
        private void BackClickEvent(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Login());
        }

        // Event handler für den Registrierenknopf
        private void RegisterClickEvent(object sender, RoutedEventArgs e)
        {
            RegisterUser();
        }

        // Event handler für Änderungen in den Eingabefeldern
        private void TextChangedEvent(object sender, RoutedEventArgs e)
        {
            // Zurücksetzen der roten Boxen um die Eingabefelder
            TextBoxError.Opacity = 0;
            PasswordBoxError.Opacity = 0;

            // Wenn beide Eingabefelder leer sind ist der Anmeldeknopf deaktiviert
            if (TextBoxUserName.Text.Equals(string.Empty) || PasswordBox.Password.Equals(string.Empty))
            {
                ButtonRegister.IsEnabled = false;
            }
            else
            {
                ButtonRegister.IsEnabled = true;
            }
        }

        // Event handler für Tastenanschläge
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            // Eingabe auf bestimmte Taste prüfen
            switch (e.Key)
            {
                case Key.Enter:
                    // Wenn Enter gedrückt wurde ins nächste Feld springen
                    break;
                default:
                    break;
            }
        }

        private void RegisterUser()
        {
            /// ToDo: register logic

            Switcher.Switch(new Login());
        }
    }
}
