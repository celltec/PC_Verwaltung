namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Computer beinhaltet technische Daten über einen PC
    /// </Zusammenfassung>
    public class Computer
    {
        public int ComputerId { get; set; }
        public string ComputerName { get; set; }
        public string MacAddress { get; set; }

        // Leerer Standardkonstruktor
        public Computer(){}

        // Konstruktor setzt übergebene Attribute
        public Computer(int computerId, string computerName, string macAddress)
        {
            ComputerId = computerId;
            ComputerName = computerName;
            MacAddress = macAddress;
        }
    }
}
