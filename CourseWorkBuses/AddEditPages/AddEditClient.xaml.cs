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
    public partial class AddEditClient : Window
    {
        public AddEditClient()
        {
            InitializeComponent();
        }
        CourseWorkBusesContext _db = new CourseWorkBusesContext();
        Client _client;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.client == null)
            {
                Title = "Новый клиент";
                Save.Content = "Добавить";
                _client = new Client();
                Save.Style = (Style)FindResource("ButtonAdd");
            }
            else
            {
                Title = "Изменить клиента";
                Save.Content = "Изменить";
                Save.Style = (Style)FindResource("ButtonEdit");
                Icon = new BitmapImage(new Uri("pack://application:,,,/AddEditPages/Edit.png"));
                _client = _db.Clients.Find(Data.client.ClientId);
            }
            DataContext = _client;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string clientLastName = ClientLastName.Text;
            string clientFirstName = ClientFirstName.Text;
            string clientMiddleName = ClientMiddleName.Text;
            string clientContacts = ClientContacts.Text;

            if (string.IsNullOrEmpty(clientLastName) || string.IsNullOrEmpty(clientFirstName) || string.IsNullOrEmpty(clientContacts))
            {
                MessageBox.Show("Пожалуйста, заполни все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string namePattern = @"^[А-Яа-яЁё]+$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(clientLastName, namePattern))
            {
                MessageBox.Show("В поле «Фамилия» должны быть буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(clientFirstName, namePattern))
            {
                MessageBox.Show("В поле «Имя» должны быть буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(clientMiddleName, namePattern))
            {
                MessageBox.Show("В поле «Отчество» должны быть буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Data.client == null)
            {
                _db.Clients.Add(_client);
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
