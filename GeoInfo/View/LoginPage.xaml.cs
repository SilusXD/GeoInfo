using GeoInfo.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeoInfo.Models
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private Users user;
        public LoginPage()
        {
            InitializeComponent();
        }

        public bool Auth(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            user = Model.GeoInfoEE.GetContext().Users.AsNoTracking().FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user == null)
            {
                MessageBox.Show("Пользователь с такими данными не найден!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            MessageBox.Show("Пользователь успешно найден!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            
            return true;
        }

        public bool Reg(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            var user = Model.GeoInfoEE.GetContext().Users.AsNoTracking().FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                MessageBox.Show("Пользователь с такими данными уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            Model.Users newUsers = new Model.Users();
            newUsers.Login = login;
            newUsers.Password = password;

            Model.GeoInfoEE.GetContext().Users.Add(newUsers);
            Model.GeoInfoEE.GetContext().SaveChanges();
            MessageBox.Show("Пользователь успешно создан!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if(Auth(txtBoxLogin.Text, passwordBox.Password))
            {
                NavigationService.Navigate(new View.UserPage(user));
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Reg(txtBoxLogin.Text, passwordBox.Password);
        }
    }
}
