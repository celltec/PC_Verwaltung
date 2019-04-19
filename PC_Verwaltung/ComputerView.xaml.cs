using System;
using System.Windows.Controls;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse ComputerView beinhaltet Event Handler für das UserControl
    /// </Zusammenfassung>
    public partial class ComputerView : UserControl
    {
        public ComputerView()
        {
            InitializeComponent();
        }

        // Event handler zum Löschen dieses ComputerView Objektes
        public event DeleteHandler Delete;
        public delegate void DeleteHandler(ComputerView computer);

        // Event handler des Löschen Knopfes
        private void DeleteClickEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            // Delete Event auslösen
            Delete?.Invoke(this);
        }
    }
}
