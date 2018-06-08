using MyAffairs.Core.Interfaces;
using MyAffairs.Core.Models;
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

namespace MyAffairs.UI
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        IStorage _storage;
        public RegisterWindow(IStorage storage)
        {
            InitializeComponent();
            _storage = storage;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = Core.Models.User.GetHash(PasswordTextBox.Password);
            string passwordConf = Core.Models.User.GetHash(ConfirmPasswordTextBox.Password);
            if (string.IsNullOrWhiteSpace(fullName))
            {
                FullNameTextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                EmailTextBox.Focus();
                return;
            }
            if (_storage.Users.Items.FirstOrDefault(u => u.Email.ToLower() == email.ToLower()) != null)
            {
                MessageBox.Show("Пользователь с таким Email уже существует!");
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                PasswordTextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(ConfirmPasswordTextBox.Password))
            {
                ConfirmPasswordTextBox.Focus();
                return;
            }
            if (password != passwordConf)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка");
                return;
            }
            User NewUser = new User
            {
                FullName = fullName,
                Password = password,
                Email = email,
                Goals = new List<Goal>()
            };
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AuthorizationWindow window = new AuthorizationWindow(_storage);
            window.Show();
        }
    }
}
