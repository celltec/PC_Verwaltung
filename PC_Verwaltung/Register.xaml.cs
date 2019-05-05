using System;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Login beinhaltet die Logik für das Registrieren
    /// </Zusammenfassung>
    public partial class Register : Page
    {
        // Benutzerliste anlegen und Standardbenutzer einfügen
        internal static List<User> Users { get; } = new List<User>() { new User() };

        // Fenster Initialisieren
        public Register()
        {
            InitializeComponent();

            // Cursor in Eingabefeld legen
            TextBoxUserName.Focus();
        }

        // Event handler für den Zurückknopf
        private void BackClickEvent(object sender, RoutedEventArgs e)
        {
            // Zum Anmeldebildschirm wechseln
            Switcher.Navigate(new Login());
        }

        // Event handler für den Registrierenknopf
        private void RegisterClickEvent(object sender, RoutedEventArgs e)
        {
            RegisterUser();
        }

        // Event handler für Änderungen in den Eingabefeldern
        private void TextChangedEvent(object sender, RoutedEventArgs e)
        {
            // Zurücksetzen der Fehlermeldung und der roten Boxen um die Eingabefelder
            LabelErrorMessage.Content = "";
            RectUserNameError.Opacity = 0;
            RectPasswordError.Opacity = 0;
            RectEmailError.Opacity = 0;

            // Wenn beide Eingabefelder leer sind ist der Registrierenknopf deaktiviert
            if (TextBoxUserName.Text.Equals(string.Empty) || PasswordBox.Password.Equals(string.Empty))
            {
                ButtonRegister.IsEnabled = false;
            }
            else
            {
                ButtonRegister.IsEnabled = true;
            }
        }

        private void RegisterUser()
        {
            if (CheckUserExists(TextBoxUserName.Text))
            {
                // Fehlernachricht setzen und rote Box ums Benutzernamefeld sichtbar machen
                LabelErrorMessage.Content = "Benutzername vergeben";
                RectUserNameError.Opacity = 1;
                return;
            }
            else if (PasswordBox.Password.Length < 5)
            {
                // Fehlernachricht setzen und rote Box ums Passwortfeld sichtbar machen
                LabelErrorMessage.Content = "Das Passwort muss mind. 5 Zeichen lang sein";
                RectPasswordError.Opacity = 1;
                return;
            }
            else
            {
                // Sicherheitshalber nachfragen, wenn E-Mail leer gelassen wird
                if (TextBoxEmail.Text.Equals(string.Empty))
                {
                    if (MessageBoxResult.No == MessageBox.Show("Ohne E-Mail kann das Passwort nicht zurückgesetzt werden!\n\nMöchten sie ohne E-Mail Adresse fortfahren?", FindResource("AppTitle").ToString(), MessageBoxButton.YesNo, MessageBoxImage.Warning))
                    {
                        return;
                    }
                }
                else if (!IsValidEmail(TextBoxEmail.Text)) // E-Mail formatierung überprüfen
                {
                    // Fehlernachricht setzen und rote Box ums E-Mailfeld sichtbar machen
                    LabelErrorMessage.Content = "Keine gültige E-Mail Adresse";
                    RectEmailError.Opacity = 1;
                    return;
                }

                // Neuen Benutzer erstellen und in Liste speichern
                Users.Add(new User(TextBoxUserName.Text, PasswordBox.Password, TextBoxEmail.Text, TextBoxDisplayName.Text, TextBoxDepartment.Text));

                // Erfolgsmeldung
                MessageBox.Show("Registrierung erfolgreich!", FindResource("AppTitle").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);

                // Zum Anmeldebildschirm wechseln
                Switcher.Navigate(new Login());
            }
        }

        private bool CheckUserExists(string name)
        {
            // Benutzerliste nach Namen durchsuchen
            foreach (User user in Users)
            {
                if (user.UserName == name)
                {
                    return true;
                }
            }

            return false;
        }

        // IsValidEmail ist eine Hilfsfunktion zum Überprüfen des Formats E-Mail.
        // Quelle: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
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
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
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
