namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Computer beinhaltet technische Daten über einen PC
    /// </Zusammenfassung>
    class Computer
    {
        public string ComputerId { get; set; }
        public string ComputerName { get; set; }
        public string MacAddress { get; set; }

        // Konstruktor setzt übergebene Attribute
        public Computer(string computerId, string computerName, string macAddress)
        {
            ComputerId = computerId;
            ComputerName = computerName;
            MacAddress = macAddress;
        }
    }
}
