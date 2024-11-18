using CourseWorkBuses.Models;
using Microsoft.Extensions.Primitives;
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
    public partial class AddEditWorker : Window
    {
        public AddEditWorker()
        {
            InitializeComponent();
        }

        CourseWorkBusesContext _db = new CourseWorkBusesContext();
        Worker _worker;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.worker == null)
            {
                Title = "Новый сотрудник";
                Save.Content = "Добавить";
                _worker = new Worker();
                Save.Style = (Style)FindResource("ButtonAdd");
            }
            else
            {
                Title = "Изменить сотрудника";
                Save.Content = "Изменить";
                Save.Style = (Style)FindResource("ButtonEdit");
                Icon = new BitmapImage(new Uri("pack://application:,,,/AddEditPages/Edit.png"));
                _worker = _db.Workers.Find(Data.worker.WorkerId);
            }
            DataContext = _worker;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string workerLastName = WorkerLastName.Text;
            string workerFirstName = WorkerFirstName.Text;
            string workerMiddleName = WorkerMiddleName.Text;
            string workerPosition = WorkerPosition.Text;
            string workerContacts = WorkerContacts.Text;
            string workerHireDate = WorkerHireDate.Text;

            if (string.IsNullOrEmpty(workerLastName) || string.IsNullOrEmpty(workerFirstName) ||
                string.IsNullOrEmpty(workerContacts) || string.IsNullOrEmpty(workerPosition) || string.IsNullOrEmpty(workerHireDate))
            {
                MessageBox.Show("Пожалуйста, заполни все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string namePattern = @"^[А-Яа-яЁё]+$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(workerLastName, namePattern))
            {
                MessageBox.Show("В поле «Фамилия» должны быть буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(workerFirstName, namePattern))
            {
                MessageBox.Show("В поле «Имя» должны быть буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(workerMiddleName, namePattern))
            {
                MessageBox.Show("В поле «Отчество» должны быть буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string datePattern = @"^\d{2}/\d{2}/\d{4}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(workerHireDate, datePattern))
            {
                MessageBox.Show("Неверный формат даты в поле «Принят на работу»!\n\nПравильный: MM/DD/YYYY", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Data.worker == null)
            {
                _db.Workers.Add(_worker);
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
