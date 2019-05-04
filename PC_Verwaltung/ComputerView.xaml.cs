using System.Windows;
using System.Threading.Tasks;
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
            await Task.Delay((int)FindResource("AnimationTime"));

            // Delete Event auslösen
            Delete?.Invoke(this);
        }
    }
}
    