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
        int _importantValue;
        int _urgentValue;
        bool _completedGoals;
        public MainWindow(IStorage storage, User user)
        {
            InitializeComponent();
            _storage = storage;
            _user = _storage.LogIn(user);
            _importantValue = -1;
            _urgentValue = -1;
            RefreshTaskListBox();
            ImportanceComboBox.ItemsSource = _storage.ObjectsForImportanceComboBox;
            UrgencyComboBox.ItemsSource = _storage.ObjectsForUrgencyComboBox;
        }

        private void RefreshTaskListBox()
        {
            TaskListBox.ItemsSource = null;
            List<Goal> sortedGoals = _user.Goals.Where(g => g.Completed == false).ToList();
            switch (_importantValue)
            {
                case 0:
                    sortedGoals = sortedGoals.Where(g => g.Important == false).ToList();
                    break;
                case 1:
                    sortedGoals = sortedGoals.Where(g => g.Important == true).ToList();
                    break;
                default:
                    break;
            }
            switch (_urgentValue)
            {
                case -1:
                    break;
                case 0:
                    sortedGoals = sortedGoals.Where(g => g.Urgent == false).ToList();
                    break;
                case 1:
                    sortedGoals = sortedGoals.Where(g => g.Urgent == true).ToList();
                    break;
            }
            TaskListBox.ItemsSource = sortedGoals.
                OrderByDescending(g => g.Important).ThenByDescending(g => g.Urgent);
        }

        private void NameOfTheTaskTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock NameTextBlock = sender as TextBlock;
            NameTextBlock.Text = (NameTextBlock.DataContext as Goal).Name;
            var color = SetBackgroundColor(NameTextBlock.DataContext as Goal);
            NameTextBlock.Background = color;

        }
        private void DeleteButtonBorder_Initialized(object sender, EventArgs e)
        {
            Border BorderTaskTextBlock = sender as Border;
            var color = SetBackgroundColor(BorderTaskTextBlock.DataContext as Goal);
            BorderTaskTextBlock.BorderBrush = color;
        }
        private SolidColorBrush SetBackgroundColor(Goal goal)
        {
            if (!goal.Completed)
            {
                if (goal.Important)
                    if (goal.Urgent)
                        return Brushes.Red;
                    else
                        return Brushes.Orange;
                else
                {
                    if (goal.Urgent)
                        return Brushes.Yellow;
                    else
                        return Brushes.Lime;
                }
            }
            else
            {
                return Brushes.Gray;
            }


        }

        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow window = new TaskWindow(_storage, _user) { Owner = this };
            if (window.ShowDialog() == true)
            {
                RefreshTaskListBox();
            }

        }

        private void TaskListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TaskListBox.SelectedItem is Goal goal)
            {
                TaskWindow window = new TaskWindow(_storage, _user, goal) { Owner = this };
                if (window.ShowDialog() == true)
                {
                    RefreshTaskListBox();
                }
            }
        }

        private void UrgencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectForComboBox obj = UrgencyComboBox.SelectedItem as ObjectForComboBox;
            _urgentValue = obj.Value;
            RefreshTaskListBox();
        }

        private void ImportanceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectForComboBox obj = ImportanceComboBox.SelectedItem as ObjectForComboBox;
            _importantValue = obj.Value;
            RefreshTaskListBox();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button DelButton = sender as Button;
            Goal goal = DelButton.DataContext as Goal;
            _user.Goals.Remove(goal);
            _storage.RemoveGoal(goal);
            RefreshTaskListBox();
        }

        private void CompleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            Goal goal = chb.DataContext as Goal;
            if (!_completedGoals)
                goal.Completed = true;
            else
                goal.Completed = false;
            _user.Goals.First(g => g.Id == goal.Id).Completed = goal.Completed;
            _storage.EditGoal(goal);
            TurnToCompleted(false);
            RefreshTaskListBox();
        }

        private void CompletedTasksButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_completedGoals)
            {
                TaskListBox.ItemsSource = null;
                TaskListBox.ItemsSource = _user.Goals.Where(g => g.Completed == true);
                TurnToCompleted(true);
            }
            else
            {
                RefreshTaskListBox();
                TurnToCompleted(false);
            }

        }
        private void TurnToCompleted(bool par)
        {
            if (par)
                CompletedTasksButton.Content = "Актуальные";
            else
                CompletedTasksButton.Content = "Завершенные";
            _completedGoals = par;
            ImportanceComboBox.IsEnabled = !par;
            UrgencyComboBox.IsEnabled = !par;
        }

        private void CompleteCheckBox_Initialized(object sender, EventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (_completedGoals)
                chb.ToolTip = "Восстановить";
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow window = new AuthorizationWindow(_storage);
            window.Show();
            Close();
        }
    }
}
