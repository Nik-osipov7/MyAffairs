using MyAffairs.Core.Interfaces;
using MyAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAffairs.Core.DataStorage
{
    internal abstract class BaseStorage : IStorage
    {
        public List<ObjectForComboBox> ObjectsForImportanceComboBox { get; set; }
        public List<ObjectForComboBox> ObjectsForUrgencyComboBox { get; set; }

        protected IRepository<User> _users;
        public IRepository<User> Users => _users;
        public abstract void AddUser(User user);
        public abstract User LogIn(User user);
        public abstract void AddGoal(User user, Goal goal);
        public abstract void RemoveGoal(Goal goal);
        public abstract void EditGoal(Goal goal);
    }
    internal class DatabaseStorage : BaseStorage
    {
        Context _context;

        public DatabaseStorage()
        {
            Restore();
        }
        private void Restore()
        {
            using (_context = new Context())
            {
                _users = new DatabaseRepository<User>(_context.Users.ToList());
            }
            ObjectsForImportanceComboBox = new List<ObjectForComboBox>()
            {
                new ObjectForComboBox("Все", -1),
                new ObjectForComboBox("Важные", 1),
                new ObjectForComboBox("Неважные", 0)
            };
            ObjectsForUrgencyComboBox = new List<ObjectForComboBox>()
            {
                new ObjectForComboBox("Все", -1),
                new ObjectForComboBox("Срочные", 1),
                new ObjectForComboBox("Несрочные", 0)
            };
        }

        public override void AddUser(User user)
        {
            using (_context = new Context())
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }
        public override User LogIn(User user)
        {
            using (_context = new Context())
            {
                User User = _context.Users.First(u => u.Email == user.Email);
                _context.Entry(User).Collection(u => u.Goals).Load();
                return User;
            }

        }
        public override void RemoveGoal(Goal goal)
        {
            using (_context = new Context())
            {
                Goal Goal = _context.Goals.First(g => g.Id == goal.Id);
                _context.Goals.Remove(Goal);
                _context.SaveChanges();
            }
        }
        public override void AddGoal(User user, Goal goal)
        {
            using (_context = new Context())
            {
                _context.Users.First(u => u.Email == user.Email).Goals.Add(goal);
                _context.SaveChanges();
            }
        }
        public override void EditGoal(Goal goal)
        {
            using (_context = new Context())
            {
                Goal _goal = _context.Goals.First(g => g.Id == goal.Id);
                _goal.Name = goal.Name;
                _goal.Text = goal.Text;
                _goal.Important = goal.Important;
                _goal.Urgent = goal.Urgent;
                _goal.Completed = goal.Completed;
                _context.SaveChanges();
            }
        }
    }
}
