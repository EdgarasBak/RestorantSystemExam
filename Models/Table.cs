using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestorantSystemExam.Models
{
    internal class Table
    {
        public int TableNumber { get; }
        public int NumberOfSeats { get; }
        private Order order;
        private decimal totalAmountPaid;
        public Table(int tableNumber, int numberOfSeats)
        {
            TableNumber = tableNumber;
            NumberOfSeats = numberOfSeats;
        }
        public void SetOrder(Order order)
        {
            this.order = order;
        }
        public Order GetOrder()
        {
            return order;
        }
        public void SetTotalAmountPaid(decimal totalAmountPaid)
        {
            this.totalAmountPaid = totalAmountPaid;
        }
        public decimal GetTotalAmountPaid()
        {
            return totalAmountPaid;
        }
        public override string ToString()
        {
            return $"Table: {TableNumber} - Seats: {NumberOfSeats}";
        }
    }
}
