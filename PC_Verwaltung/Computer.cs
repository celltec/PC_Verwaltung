using System;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse Computer beinhaltet Daten über einen Computer
    /// Der Inhalt dieser Klasse wird in die Xml Datei serialisiert
    /// </Zusammenfassung>
    public class Computer
    {
        private DateTime DateAddedValue;
        private int PriceValue;

        public string Name { get; set; }
        public string MacAddress { get; set; }
        public string Properties { get; set; }
        public string Price
        {
            // Wenn der Wert negativ ist, einen leeren String zurückgeben
            get { return PriceValue < 0 ? "" : PriceValue.ToString(); }
            // Wenn die Eingabe ein leerer String ist, einen negativen Wert setzen
            set { PriceValue = value == "" ? int.MinValue : int.Parse(value); }
        }
        public string DateAdded
        {
            // Datum in lesbaren String umwandeln
            get { return DateAddedValue.ToString("dd.MM.yy"); }
            set { DateAddedValue = DateTime.Parse(value); }
        }

        // Leerer Standardkonstruktor
        public Computer() { }

        // Konstruktor setzt übergebene Attribute
        public Computer(string name, string mac, string properties, string price, string dateAdded)
        {
            Name = name;
            MacAddress = mac;
            Properties = properties;
            Price = price;
            DateAdded = dateAdded;
        }
    }
}
