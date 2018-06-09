using MyAffairs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAffairs.Core.DataStorage
{
    internal abstract class BaseRepository<T> : IRepository<T>
    {
        protected List<T> _items;
        public IEnumerable<T> Items => _items;


        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public abstract void Save();
    }
    internal class DatabaseRepository<T> : BaseRepository<T>
    {
        List<T> items;


        public DatabaseRepository(List<T> items)
        {
            this.items = items;
            Restore();
        }

        private void Restore()
        {
            _items = items;
        }

        public override void Save()
        {

        }
    }
}
