using CourseWorkBuses.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWorkBuses
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenClients_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.ShowDialog();
        }

        private void OpenFlights_Click(object sender, RoutedEventArgs e)
        {
            FlightsWindow flightsWindow = new FlightsWindow();
            flightsWindow.ShowDialog();
        }

        private void OpenTickets_Click(object sender, RoutedEventArgs e)
        {
            TicketsWindow ticketsWindow = new TicketsWindow();
            ticketsWindow.ShowDialog();
        }

        private void OpenBuses_Click(object sender, RoutedEventArgs e)
        {
            BusesWindow busesWindow = new BusesWindow();
            busesWindow.ShowDialog();
        }

        private void OpenRepair_Click(object sender, RoutedEventArgs e)
        {
            RepairWindow repairWindow = new RepairWindow();
            repairWindow.ShowDialog();
        }

        private void OpenWorkers_Click(object sender, RoutedEventArgs e)
        {
            WorkersWindow workersWindow = new WorkersWindow();
            workersWindow.ShowDialog();
        }
    }
}