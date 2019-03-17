namespace PC_Verwaltung
{
    // Definition von Ergebnissen der Anmeldung zur besseren Leserlichkeit
    enum LoginResult { Success, NameError, PasswordError };

    /// <Zusammenfassung>
    /// Die Klasse User beinhaltet Anmeldedaten und Funktionalitäten zur Überprüfung jener Daten
    /// </Zusammenfassung>
    class User
    {
        // Voreingestellte Anmeldedaten, falls dem Konstruktor keine übergeben werden
        // Name und Passwort lauten "admin"
        private const string DEFAULT_NAME = "admin";
        private const string DEFAULT_PASSWORD_HASH = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918";

        public string Name { get; }
        private readonly string password;

        // Standardkonstruktor setzt den definierten Name und Passworthash
        public User()
        {
            Name = DEFAULT_NAME;
            password = DEFAULT_PASSWORD_HASH;
        }

        // Konstruktor setzt übergebenen Name und gehashtes Passwort
        public User(string name, string pw)
        {
            Name = name.ToLower();
            password = SecureHash.GetHashString(pw);
        }

        // Hier findet der Anmeldedatenvergleich statt
        public LoginResult Anmelden(string name, string pw)
        {
            // Name prüfen
            if (name.ToLower() != Name)
            {
                // Bei Ungleichheit Error zurückgeben
                return LoginResult.NameError;
            }
            // Passworthash prüfen
            else if (SecureHash.GetHashString(pw) != password)
            {
                // Bei Ungleichheit Error zurückgeben
                return LoginResult.PasswordError;
            }
            else
            {
                // Wenn beide Daten passen Erfolg melden
                return LoginResult.Success;
            }
        }
    }
}
