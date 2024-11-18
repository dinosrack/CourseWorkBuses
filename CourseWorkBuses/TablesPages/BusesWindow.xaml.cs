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
    public partial class BusesWindow : Window
    {
        public BusesWindow()
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
                Table.ItemsSource = _db.Buses.ToList();

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
            AddEditBus f = new AddEditBus();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedItem != null)
            {
                Data.bus = (Bus)Table.SelectedItem;
                AddEditBus f = new AddEditBus();
                f.Owner = this;
                f.ShowDialog();
                Data.bus = null;
                LoadDBInDataGrid();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("ВНИМАНИЕ!\nЭто действие приведет к удалению связанных рейсов и ремонтных работ!\n\nУдалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Bus row = (Bus)Table.SelectedItem;
                    if (row != null)
                    {
                        using (CourseWorkBusesContext _db = new CourseWorkBusesContext())
                        {
                            var relatedFlights = _db.Flights.Where(f => f.BusId == row.BusId);
                            _db.Flights.RemoveRange(relatedFlights);

                            var relatedRepairs = _db.Repairs.Where(f => f.BusId == row.BusId);
                            _db.Repairs.RemoveRange(relatedRepairs);

                            _db.Buses.Remove(row);
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
            List<Bus> listItem = (List<Bus>)Table.ItemsSource;

            var filtered = listItem.Where(p => p.BusId.ToString().Contains(ID.Text));
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
