using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Login beinhaltet die Logik für das Anmelden
    /// </Zusammenfassung>
    public partial class Login : UserControl
    {
        // Fenster Initialisieren
        public Login()
        {
            InitializeComponent();

            // Standardfenstergröße setzen
            Switcher.Window.Height = Convert.ToInt32(FindResource("Height"));
            Switcher.Window.Width = Convert.ToInt32(FindResource("Width"));
            
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
            // Zum Registrierungsbildschirm wechseln
            Switcher.Switch(new Register());
        }

        // Event handler für Änderungen in den Eingabefeldern
        private void TextChangedEvent(object sender, RoutedEventArgs e)
        {
            // Zurücksetzen der roten Boxen um die Eingabefelder
            RectTextBoxError.Opacity = 0;
            RectPasswordBoxError.Opacity = 0;

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
                    // Wenn Enter gedrückt wurde überprüfen, dass UserName und Passwort nicht leer sind
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
            foreach (User user in Register.Users)
            {
                // Anmeldeversuch und Rückgabe eines Ergebnisses
                LoginResult result = user.Anmelden(name, pw);

                // Ergebnis der Anmeldung verarbeiten
                switch (result)
                {
                    case LoginResult.Success:
                        // Bei erfolgreicher Anmeldung einloggen und zum Verwaltungsbildschirm wechseln
                        Switcher.Switch(new Manager());
                        return true;
                    case LoginResult.NameError:
                        // Wenn der UserName nicht stimmt weiter die Liste durchsuchen
                        continue;
                    case LoginResult.PasswordError:
                        // Sichtbarmachen der roten Boxen um die Eingaben wenn das Passwort falsch ist
                        RectTextBoxError.Opacity = 1;
                        RectPasswordBoxError.Opacity = 1;
                        return false;
                    default:
                        break;
                }
            }

            // Sichtbarmachen der roten Boxen um die Eingaben, wenn der UserName nicht in der Liste war
            RectTextBoxError.Opacity = 1;
            RectPasswordBoxError.Opacity = 1;

            return false;
        }
    }
}
