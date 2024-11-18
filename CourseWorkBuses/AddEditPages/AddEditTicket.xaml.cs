using CourseWorkBuses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWorkBuses.AddEditPages
{
    public partial class AddEditTicket : Window
    {
        public AddEditTicket()
        {
            InitializeComponent();
        }

        CourseWorkBusesContext _db = new CourseWorkBusesContext();
        Ticket _ticket;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.ticket == null)
            {
                Title = "Новый билет";
                Save.Content = "Добавить";
                _ticket = new Ticket();
                Save.Style = (Style)FindResource("ButtonAdd");
            }
            else
            {
                Title = "Изменить билет";
                Save.Content = "Изменить";
                Save.Style = (Style)FindResource("ButtonEdit");
                Icon = new BitmapImage(new Uri("pack://application:,,,/AddEditPages/Edit.png"));
                _ticket = _db.Tickets.Find(Data.ticket.TicketId);
            }
            DataContext = _ticket;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string flightId = FlightID.Text;
            string ticketType = TicketType.Text;
            string ticketPrice = TicketPrice.Text;
            string ticketStatus = TicketStatus.Text;

            if (string.IsNullOrEmpty(flightId) || string.IsNullOrEmpty(ticketType) || string.IsNullOrEmpty(ticketPrice) || string.IsNullOrEmpty(ticketStatus))
            {
                MessageBox.Show("Пожалуйста, заполни все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var flight = _db.Flights.FirstOrDefault(f => f.FlightId.ToString() == flightId);
            if (flight == null)
            {
                MessageBox.Show("Рейса с таким ID не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(ticketPrice, out decimal price))
            {
                MessageBox.Show("В поле «Цена» должны быть цифры!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Data.ticket == null)
            {
                _db.Tickets.Add(_ticket);
                _db.SaveChanges();
            }
            else
            {
                _db.SaveChanges();
            }

            MessageBox.Show("Запись сохранена", "Запись", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
