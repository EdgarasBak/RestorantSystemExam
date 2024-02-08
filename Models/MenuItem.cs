using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestorantSystemExam.Models
{
    internal class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public MenuItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
