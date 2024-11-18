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
    public partial class AddEditFlight : Window
    {
        public AddEditFlight()
        {
            InitializeComponent();
        }
        CourseWorkBusesContext _db = new CourseWorkBusesContext();
        Flight _flight;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.flight == null)
            {
                Title = "Новый рейс";
                Save.Content = "Добавить";
                _flight = new Flight();
                Save.Style = (Style)FindResource("ButtonAdd");
            }
            else
            {
                Title = "Изменить рейс";
                Save.Content = "Изменить";
                Save.Style = (Style)FindResource("ButtonEdit");
                Icon = new BitmapImage(new Uri("pack://application:,,,/AddEditPages/Edit.png"));
                _flight = _db.Flights.Find(Data.flight.FlightId);
            }
            DataContext = _flight;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string busId = BusID.Text;
            string departurePoint = DeparturePoint.Text;
            string arrivalPoint = ArrivalPoint.Text;
            string departureDate = DepartureDate.Text;
            string departureTime = DepartureTime.Text;
            string arrivalDate = ArrivalDate.Text;
            string arrivalTime = ArrivalTime.Text;

            if (string.IsNullOrEmpty(busId) || string.IsNullOrEmpty(departurePoint) || string.IsNullOrEmpty(arrivalPoint) ||
                string.IsNullOrEmpty(departureDate) || string.IsNullOrEmpty(departureTime) ||
                string.IsNullOrEmpty(arrivalDate) || string.IsNullOrEmpty(arrivalTime))
            {
                MessageBox.Show("Пожалуйста, заполни все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var bus = _db.Buses.FirstOrDefault(b => b.BusId.ToString() == busId);
            if (bus == null)
            {
                MessageBox.Show("Автобуса с таким ID не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string placePattern = @"^[А-Яа-яЁё\s\-]+$"; 
            if (!System.Text.RegularExpressions.Regex.IsMatch(departurePoint, placePattern))
            {
                MessageBox.Show("В поле «Место отправления» должны быть буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(arrivalPoint, placePattern))
            {
                MessageBox.Show("В поле «Место прибытия» должны быть буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string datePattern = @"^\d{4}-\d{2}-\d{2}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(departureDate, datePattern))
            {
                MessageBox.Show("Неверный формат даты в поле «Дата отправления»!\n\nПравильный: YYYY-MM-DD", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(arrivalDate, datePattern))
            {
                MessageBox.Show("Неверный формат даты в поле «Дата прибытия»!\n\nПравильный: YYYY-MM-DD", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string timePattern = @"^\d{2}:\d{2}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(departureTime, timePattern))
            {
                MessageBox.Show("Неверный формат времени в поле «Время отправления»!\n\nПравильный: HH:MM", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(arrivalTime, timePattern))
            {
                MessageBox.Show("Неверный формат времени в поле «Время прибытия»!\n\nПравильный: HH:MM", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Data.flight == null)
            {
                _db.Flights.Add(_flight);
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