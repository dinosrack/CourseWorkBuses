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
    public partial class TicketsWindow : Window
    {
        public TicketsWindow()
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
                Table.ItemsSource = _db.Tickets.ToList();

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
            AddEditTicket f = new AddEditTicket();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedItem != null)
            {
                Data.ticket = (Ticket)Table.SelectedItem;
                AddEditTicket f = new AddEditTicket();
                f.Owner = this;
                f.ShowDialog();
                Data.ticket = null;
                LoadDBInDataGrid();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Ticket row = (Ticket)Table.SelectedItem;
                    if (row != null)
                    {
                        using (CourseWorkBusesContext _db = new CourseWorkBusesContext())
                        {
                            _db.Tickets.Remove(row);
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
            else Table.Focus();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            List<Ticket> listItem = (List<Ticket>)Table.ItemsSource;

            if (int.TryParse(ID.Text, out int flightId))
            {
                var filtered = listItem.Where(p => p.FlightId == flightId).ToList();

                if (filtered.Count > 0)
                {
                    Table.SelectedItems.Clear();

                    foreach (var item in filtered)
                    {
                        Table.SelectedItems.Add(item);
                    }

                    Table.ScrollIntoView(filtered.First());
                    Table.Focus();
                }
                else
                {
                    MessageBox.Show("Записи с указанным ID не найдены", "Поиск", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
