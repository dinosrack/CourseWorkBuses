using CourseWorkBuses.Models;
using Microsoft.EntityFrameworkCore;
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
    public partial class AddEditRepair : Window
    {
        public AddEditRepair()
        {
            InitializeComponent();
        }
        CourseWorkBusesContext _db = new CourseWorkBusesContext();
        Repair _repair;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.repair == null)
            {
                Title = "Новый ремонт";
                Save.Content = "Добавить";
                _repair = new Repair();
                Save.Style = (Style)FindResource("ButtonAdd");
            }
            else
            {
                Title = "Изменить ремонт";
                Save.Content = "Изменить";
                Save.Style = (Style)FindResource("ButtonEdit");
                Icon = new BitmapImage(new Uri("pack://application:,,,/AddEditPages/Edit.png"));
                _repair = _db.Repairs.Find(Data.repair.RepairId);
            }
            DataContext = _repair;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string busId = BusID.Text;
            string repairStatus = RepairStatus.Text;
            string repairNotes = RepairNotes.Text;

            if (string.IsNullOrEmpty(busId) || string.IsNullOrEmpty(repairStatus) || string.IsNullOrEmpty(repairNotes))
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

            if (repairStatus == "На ремонте")
            {
                if (bus != null)
                {
                    bus.BusStatus = "На ремонте";

                    if (Data.repair == null)
                    {
                        var flights = _db.Flights.Where(f => f.BusId == bus.BusId).ToList();
                        foreach (var flight in flights)
                        {
                            var tickets = _db.Tickets.Where(t => t.FlightId == flight.FlightId).ToList();
                            _db.Tickets.RemoveRange(tickets);
                            _db.Flights.Remove(flight);
                        }
                    }
                }
            }
            else if (repairStatus == "Завершен")
            {
                if (bus != null)
                {
                    bus.BusStatus = "В эксплуатации";
                }
            }

            if (Data.repair == null)
            {
                MessageBoxResult result = MessageBox.Show("ВНИМАНИЕ!\nЭто действие приведет к удалению связанных рейсов и билетов!\n\nСохранить запись?", "Запись", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _db.Repairs.Add(_repair);
                    _db.SaveChanges();
                    MessageBox.Show("Запись сохранена", "Запись", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (result == MessageBoxResult.No)
                {
                    return;
                }

                this.Close();
            }
            else
            {
                _db.Entry(_repair).State = EntityState.Modified;
                _db.SaveChanges();
                MessageBox.Show("Запись сохранена", "Запись", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
