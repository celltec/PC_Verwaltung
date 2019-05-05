using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse ComputerView beinhaltet Event Handler und Textbox Validierungen für das UserControl
    /// </Zusammenfassung>
    public partial class ComputerView : UserControl
    {
        public ComputerView()
        {
            InitializeComponent();
        }

        // Sorgt dafür, dass nur Zahlen eingegeben werden können
        private void TextBoxPriceValidation(object sender, TextCompositionEventArgs e)
        {
            // Gültigen Bereich auf Zahlen beschränken
            Regex regex = new Regex("[0-9]+");

            // Wenn Handled true ist, wird das Event nicht weiter bearbeitet und die Eingabe erscheint nicht in der Textbox
            e.Handled = !regex.IsMatch(e.Text);
        }

        // Event handler zum Verschieben dieses ComputerView Objektes nach oben
        public event MoveUpHandler MoveUp;
        public delegate void MoveUpHandler(ComputerView computer);

        // Event handler zum Verschieben dieses ComputerView Objektes nach unten
        public event MoveDownHandler MoveDown;
        public delegate void MoveDownHandler(ComputerView computer);

        // Event handler zum Löschen dieses ComputerView Objektes
        public event DeleteHandler Delete;
        public delegate void DeleteHandler(ComputerView computer);

        // Event handler des Nach-Oben-Verschieben Knopfes
        private void MoveUpClickEvent(object sender, RoutedEventArgs e)
        {
            // Delete Event auslösen
            MoveUp?.Invoke(this);
        }

        // Event handler des Nach-Unten-Verschieben Knopfes
        private void MoveDownClickEvent(object sender, RoutedEventArgs e)
        {
            // Delete Event auslösen
            MoveDown?.Invoke(this);
        }

        // Event handler des Löschen Knopfes
        private async void DeleteClickEvent(object sender, RoutedEventArgs e)
        {
            // Zeit zum Abspielen der Animation lassen
            await Task.Delay(((Duration)FindResource("AnimationDuration")).TimeSpan);

            // Delete Event auslösen
            Delete?.Invoke(this);
        }
    }
}
    