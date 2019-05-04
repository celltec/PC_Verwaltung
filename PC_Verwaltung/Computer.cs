namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Computer beinhaltet technische Daten über einen PC
    /// </Zusammenfassung>
    public class Computer
    {
        public string Name { get; set; }
        public string MacAddress { get; set; }

        // Leerer Standardkonstruktor
        public Computer(){}

        // Konstruktor setzt übergebene Attribute
        public Computer(string name, string mac)
        {
            Name = name;
            MacAddress = mac;
        }
    }
}
