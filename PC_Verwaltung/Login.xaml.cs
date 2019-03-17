using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;

namespace PC_Verwaltung
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        // Neue Benutzerliste anlegen
        private List<User> users = new List<User>();

        // Fenster Initialisieren
        public Login()
        {
            InitializeComponent();

            // Standardbenutzer anlegen
            users.Add(new User());

            // Cursor in Eingabefeld legen
            TextBox.Focus();
        }

        // Event handler für den Anmeldeknopf
        private void LoginClickEvent(object sender, RoutedEventArgs e)
        {
            // Anmeldeversuch mit eingegebenem Text starten
            CheckLogin(TextBox.Text, PasswordBox.Password);
        }

        // Event handler für den Registrierenknopf
        private void RegisterClickEvent(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Register());
        }

        // Event handler für Änderungen in den Eingabefeldern
        private void TextChangedEvent(object sender, RoutedEventArgs e)
        {
            // Zurücksetzen der roten Boxen um die Eingabefelder
            TextBoxError.Opacity = 0;
            PasswordBoxError.Opacity = 0;

            // Wenn beide Eingabefelder leer sind ist der Anmeldeknopf deaktiviert
            if (TextBox.Text.Equals(string.Empty) || PasswordBox.Password.Equals(string.Empty))
            {
                ButtonLogin.IsEnabled = false;
            }
            else
            {
                ButtonLogin.IsEnabled = true;
            }
        }

        // Event handler für Tastenanschläge
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            // Eingabe auf bestimmte Taste prüfen
            switch (e.Key)
            {
                case Key.Enter:
                    // Wenn Enter gedrückt wurde überprüfen, dass Name und Passwort nicht leer sind
                    if (TextBox.Text != "" && PasswordBox.Password != "")
                    {
                        // Anmeldeversuch mit eingegebenem Text starten
                        CheckLogin(TextBox.Text, PasswordBox.Password);
                    }
                    break;
                default:
                    break;
            }
        }

        private bool CheckLogin(string name, string pw)
        {
            // Benutzerliste durchlaufen
            foreach (User user in users)
            {
                // Anmeldeversuch und Rückgabe eines Ergebnisses
                LoginResult result = user.Anmelden(name, pw);

                // Ergebnis der Anmeldung verarbeiten
                switch (result)
                {
                    case LoginResult.Success:
                        // Bei erfolgreicher Anmeldung SplashScreen anzeigen
                        MessageBox.Show("Not implemented yet");
                        return true;
                    case LoginResult.NameError:
                        // Wenn der Name nicht stimmt weiter die Liste durchsuchen
                        continue;
                    case LoginResult.PasswordError:
                        // Sichtbarmachen der roten Box um die Passworteingabe wenn das Passwort falsch ist
                        PasswordBoxError.Opacity = 100;
                        return false;
                    default:
                        break;
                }
            }

            // Sichtbarmachen der roten Box um die Texteingabe, wenn der Name nicht in der Liste war
            TextBoxError.Opacity = 100;

            return false;
        }
    }
}
