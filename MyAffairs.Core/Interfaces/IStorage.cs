using MyAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAffairs.Core.Interfaces
{
    public interface IStorage
    {
        IRepository<User> Users { get; }
    }
}
