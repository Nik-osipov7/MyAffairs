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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAffairs.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IStorage _storage;
        User _user;
        public MainWindow(IStorage storage, User user)
        {
            InitializeComponent();
            _storage = storage;
            _user = _storage.LogIn(user);
            RefreshTaskListBox();
        }
        private void RefreshTaskListBox()
        {
            TaskListBox.ItemsSource = null;
            TaskListBox.ItemsSource = _user.Goals;

        }
        private void NameOfTheTaskTextBlock_Initialized(object sender, EventArgs e)
        {
            

        }
        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
           

        }
        private void UrgencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void ImportanceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
