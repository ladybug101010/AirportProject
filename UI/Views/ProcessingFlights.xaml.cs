using System.ComponentModel;
using System.Windows;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for ProcessingFlights.xaml
    /// </summary>
    public partial class ProcessingFlights : Window
    {
        public ProcessingFlights()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
    }
}
