using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using WpfAppBaev;
using System.IO;

namespace WpfAppBaev
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class Login : Page
    {
        private MainWindow _mainWindow;
        public Login(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

        }

        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        public bool Auth(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return false;
            }
            password = GetHash(password);
            using (var db = new EducationSystemKyrsachEntities())
            {
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Users1 == login && u.PasswordHash == password);
                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return false;
                }
                if (user.PasswordHash != password)
                {
                    MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (user.Users1 != login)
                {
                    MessageBox.Show("Неверный логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                MessageBox.Show("Пользователь успешно найден");
                loginTextBox.Clear();
                passwordText.Clear();
                return true;
            }
        }



        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Auth(loginTextBox.Text, passwordText.Password);
        }

        private void singupButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SingUpPage.xaml", UriKind.Relative));
        }
    }
}
