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
        // UserName und Passwort lauten "admin"
        private const string DEFAULT_NAME = "admin";
        private const string DEFAULT_PASSWORD_HASH = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918";

        public string UserName { get; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        private readonly string password;

        // Standardkonstruktor setzt den definierten UserName und Passworthash
        public User()
        {
            UserName = DEFAULT_NAME;
            password = DEFAULT_PASSWORD_HASH;
        }

        // Konstruktor setzt übergebenen UserName und gehashtes Passwort
        public User(string userName, string pw, string displayName = "", string email = "", string department = "")
        {
            UserName = userName.ToLower();
            password = SecureHash.GetHashString(pw);
            DisplayName = displayName;
            Email = email;
            Department = department;
        }

        // Hier findet der Anmeldedatenvergleich statt
        public LoginResult Anmelden(string name, string pw)
        {
            // UserName prüfen
            if (name.ToLower() != UserName)
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
