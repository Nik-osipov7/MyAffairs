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
using MyAffairs.Core;
using MyAffairs.Core.Interfaces;
using MyAffairs.Core.Models;

namespace MyAffairs.UI
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        IStorage _storage;
        public AuthorizationWindow()
        {
            InitializeComponent();
            _storage = Factory.Instance.GetDatabaseStorage();
        }
        public AuthorizationWindow(IStorage storage)
        {
            InitializeComponent();
            _storage = storage;
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            string password = Core.Models.User.GetHash(passwordBox.Password);
            if (string.IsNullOrWhiteSpace(login))
            {
                TextBoxLogin.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                passwordBox.Focus();
                return;
            }
            User User = new User();
            MainWindow window = new MainWindow(_storage, User);
            window.Show();
            Close();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow(_storage);
            window.Show();
            Close();
        }
    }
}

