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
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        IStorage _storage;
        User _user;
        Goal _goal;
        bool _edit;
        bool _importance;
        bool _urgency;
        public TaskWindow(IStorage storage, User user)
        {
            InitializeComponent();
            _storage = storage;
            _user = user;
        }
        public TaskWindow(IStorage storage, User user, Goal goal)
        {
            _storage = storage;
            _user = user;
            _goal = goal;
            _importance = goal.Important;
            _urgency = goal.Urgent;
            InitializeComponent();
            NameTextBox.Text = _goal.Name;
            DescriptionTextBox.Text = _goal.Text;
            ConfirmButton.Content = "Сохранить";
            Title = "Задача";
            ConfirmButton.IsEnabled = false;
            _edit = true;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string description = DescriptionTextBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                NameTextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                DescriptionTextBox.Focus();
                return;
            }
            if (!_edit)
            {
                Goal goal = new Goal
                {
                    Name = name,
                    Text = description,
                    Important = _importance,
                    Urgent = _urgency,
                    Completed = false
                };
                _storage.AddGoal(_user, goal);
                _user.Goals.Add(goal);
                DialogResult = true;
            }
            else
            {
                _goal.Name = name;
                _goal.Text = description;
                _goal.Important = _importance;
                _goal.Urgent = _urgency;
                _storage.EditGoal(_goal);
                DialogResult = true;
            }
        }

        private void ImportanceCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (_edit)
                ConfirmButton.IsEnabled = true;
            _importance = true;

        }

        private void UrgencyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (_edit)
                ConfirmButton.IsEnabled = true;
            _urgency = true;
        }

        private void ImportanceCheckBox_Initialized(object sender, EventArgs e)
        {
            CheckBox chb = (CheckBox)sender;
            chb.IsChecked = _importance;
        }

        private void UrgencyCheckBox_Initialized(object sender, EventArgs e)
        {
            CheckBox chb = (CheckBox)sender;
            chb.IsChecked = _urgency;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_edit)
                ConfirmButton.IsEnabled = true;
        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_edit)
                ConfirmButton.IsEnabled = true;
        }


        private void ImportanceCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_edit)
                ConfirmButton.IsEnabled = true;
            _importance = false;
        }

        private void UrgencyCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_edit)
                ConfirmButton.IsEnabled = true;
            _urgency = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
