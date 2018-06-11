using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAffairs.Core.Models
{
    public class ObjectForComboBox
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public ObjectForComboBox(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
