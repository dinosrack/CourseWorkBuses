using CourseWorkBuses.Models;
using Microsoft.IdentityModel.Tokens;
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
    public partial class AddEditBus : Window
    {
        public AddEditBus()
        {
            InitializeComponent();
        }
        CourseWorkBusesContext _db = new CourseWorkBusesContext();
        Bus _bus;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.bus == null)
            {
                Title = "Новый автобус";
                Save.Content = "Добавить";
                _bus = new Bus();
                Save.Style = (Style)FindResource("ButtonAdd");
            }
            else
            {
                Title = "Изменить автобус";
                Save.Content = "Изменить";
                Save.Style = (Style)FindResource("ButtonEdit");
                Icon = new BitmapImage(new Uri("pack://application:,,,/AddEditPages/Edit.png"));
                _bus = _db.Buses.Find(Data.bus.BusId);
            }
            DataContext = _bus;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Получаем значения из полей
            string busBrand = Brand.Text;
            string busModel = BusModel.Text;
            string busYearOfManufacture = BusYearOfManufacture.Text;
            string busLicensePlate = BusLicensePlate.Text;
            string busCapacity = BusCapacity.Text;
            string busStatus = (StatusBus.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(busBrand) || string.IsNullOrEmpty(busModel) ||
                string.IsNullOrEmpty(busYearOfManufacture) || string.IsNullOrEmpty(busLicensePlate) ||
                string.IsNullOrEmpty(busCapacity) || busStatus == null)
            {
                MessageBox.Show("Пожалуйста, заполни все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка формата номерного знака
            string licensePlatePattern = @"^[АВЕКМНОРСТХУ]{1}\d{3}[АВЕКМНОРСТХУ]{2} \d{2,3}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(busLicensePlate, licensePlatePattern))
            {
                MessageBox.Show("Неверный номерной знак!\n\nПравильный формат: А000АА 62", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка, что Год производства и Вместимость - это числа
            if (!int.TryParse(busYearOfManufacture, out int year))
            {
                MessageBox.Show("В поле «Год производства» должно быть число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(busCapacity, out int capacity))
            {
                MessageBox.Show("В поле «Вместимость» должно быть число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка года производства
            if (year < 1990)
            {
                // Если автобус слишком стар, и статус "В эксплуатации", то выводим ошибку
                if (busStatus == "В эксплуатации")
                {
                    MessageBox.Show("Этот автобус слишком стар для эксплуатации, его можно добавить только на списание!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Проверка вместимости
            if (capacity < 8 || capacity > 200)
            {
                MessageBox.Show("Вместимость автобуса не может быть меньше 8 и больше 200 человек!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Сохранение данных
            if (Data.bus == null)
            {
                _db.Buses.Add(_bus);
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
