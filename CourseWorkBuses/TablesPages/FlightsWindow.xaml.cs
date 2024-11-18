using CourseWorkBuses.AddEditPages;
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

namespace CourseWorkBuses.Pages
{
    public partial class FlightsWindow : Window
    {
        public FlightsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        void LoadDBInDataGrid()
        {
            using (CourseWorkBusesContext _db = new CourseWorkBusesContext())
            {
                int selectedIndex = Table.SelectedIndex;
                Table.ItemsSource = _db.Flights.ToList();

                if (selectedIndex != -1)
                {
                    if (selectedIndex == Table.Items.Count) selectedIndex--;
                    Table.SelectedIndex = selectedIndex;
                    Table.ScrollIntoView(Table.SelectedItem);
                }
                Table.Focus();
            }
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditFlight f = new AddEditFlight();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedItem != null)
            {
                Data.flight = (Flight)Table.SelectedItem;
                AddEditFlight f = new AddEditFlight();
                f.Owner = this;
                f.ShowDialog();
                Data.flight = null;
                LoadDBInDataGrid();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("ВНИМАНИЕ!\nЭто действие приведет к удалению связанных билетов!\n\nУдалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Flight row = (Flight)Table.SelectedItem;
                    if (row != null)
                    {
                        using (CourseWorkBusesContext _db = new CourseWorkBusesContext())
                        {
                            var relatedTickets = _db.Tickets.Where(t => t.FlightId == row.FlightId);
                            _db.Tickets.RemoveRange(relatedTickets);
                            _db.Flights.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadDBInDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Выбери запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}\n{ex.InnerException?.Message}\n{ex.StackTrace}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Table.Focus();
            }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            List<Flight> listItem = (List<Flight>)Table.ItemsSource;

            var filtered = listItem.Where(p => p.FlightId.ToString().Contains(ID.Text));
            if (filtered.Count() > 0)
            {
                var item = filtered.First();
                Table.SelectedItem = item;
                Table.ScrollIntoView(item);
                Table.Focus();
            }
            else
            {
                MessageBox.Show("Записи с указанным ID не найдены", "Поиск", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}