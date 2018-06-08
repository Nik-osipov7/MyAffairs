using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAffairs.Core.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public bool Urgent { get; set; }
        public bool Completed { get; set; }

    }
}
