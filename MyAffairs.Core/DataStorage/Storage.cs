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
        protected IRepository<User> _users;
        public IRepository<User> Users => _users;
        public abstract void AddUser(User user);
        public abstract User LogIn(User user);
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
    }
}
