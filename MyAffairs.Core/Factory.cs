using MyAffairs.Core.DataStorage;
using MyAffairs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAffairs.Core
{
    public class Factory
    {
        private static Factory _instance;

        public static Factory Instance => _instance ?? (_instance = new Factory());

        private Factory() { }



        private IStorage _storage;
        public IStorage GetDatabaseStorage() => _storage ?? (_storage = new DatabaseStorage());

    }
}
