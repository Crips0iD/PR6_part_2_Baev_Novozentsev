using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfAppBaev
{
    /// <summary>
    /// Логика взаимодействия для SingUpPage.xaml
    /// </summary>
    public partial class SingUpPage : Page
    {
        public SingUpPage()
        {
            InitializeComponent();
        }

        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        public bool Register(string firstName, string lastName, string username, string email, string phone, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Заполните поле имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Заполните поле фамилия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Укажите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Укажите email", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Укажите номер телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Укажите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!IsValidEmail(email.Trim()))
            {
                MessageBox.Show("Некорректный email", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            var phoneRegex = new Regex(@"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$");
            if (!phoneRegex.IsMatch(phone))
            {
                MessageBox.Show("Введите номер телефона в формате +7(XXX)XXX-XX-XX", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен быть больше 6 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну цифру", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            using (var db = new EducationSystemKyrsachEntities())
            {
                if (db.Users.AsNoTracking().Any(u => u.Users1 == username))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (db.Users.AsNoTracking().Any(u => u.Email == email.Trim()))
                {
                    MessageBox.Show("Email уже зарегистрирован", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                var newUser = new Users()
                {
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    Users1 = username.Trim(),
                    PasswordHash = GetHash(password),
                    Role = "Teacher",
                    Email = email.Trim(),
                    Phone = phone.Trim()
                };

                db.Users.Add(newUser);
                db.SaveChanges();

               

                MessageBox.Show("Вы успешно зарегистрировались!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Register(
                nameText.Text,
                surnameText.Text,
                loginText.Text,
                emailText.Text,
                phoneText.Text,
                passwordText.Password))
            {
                NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            }
        }
    }
}