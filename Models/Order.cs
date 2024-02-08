using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestorantSystemExam.Models
{
    internal class Order
    {
        private List<MenuItem> items;
        public Table Table { get; }
        public DateTime Timestamp { get; }
        public bool PrintCustomerVoucher { get; set; }

        public Order(Table table, DateTime timestamp)
        {
            Table = table;
            items = new List<MenuItem>();
            Timestamp = timestamp;
        }
        public void AddToOrder(MenuItem item)
        {
            items.Add(item);
        }

        public decimal CalculateTotalAmount()
        {
            return items.Sum(item => item.Price);
        }
        public override string ToString()
        {
            string orderDetails = $"Table {Table.TableNumber} - Seats: {Table.NumberOfSeats}\n";
            foreach (var item in items)
            {
                orderDetails += $"{item.Name} - {item.Price} euros\n";
            }
            return orderDetails;
        }
        public IEnumerable<MenuItem> GetItems()
        {
            return items;
        }
    }
}
