using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Login beinhaltet die Logik für das Registrieren
    /// </Zusammenfassung>
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
            RectUserNameError.Opacity = 0;
            RectPasswordError.Opacity = 0;
            RectEmailError.Opacity = 0;
            LabelErrorMessage.Content = "";

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
            if (TextBoxEmail.Text.Equals(string.Empty))
            {
                if (MessageBoxResult.No == MessageBox.Show("Ohne E-Mail kann das Passwort nicht zurückgesetzt werden!\n\nMöchten sie ohne E-Mail Adresse fortfahren?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    return;
                }
            }
            else if (!IsValidEmail(TextBoxEmail.Text))
            {
                LabelErrorMessage.Content = "Keine gültige E-Mail Adresse";
                RectEmailError.Opacity = 1;
                return;
            }

            /// ToDo: register logic

            Switcher.Switch(new Login());
        }

        // IsValidEmail ist eine Hilfsfunktion zum Überprüfen des Formats E-Mail. Quelle: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
