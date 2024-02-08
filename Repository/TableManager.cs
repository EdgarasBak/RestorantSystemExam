using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestorantSystemExam.Models;

namespace RestorantSystemExam.Repository
{
    internal class TableManager
    {
        private List<Table> tables;

        public TableManager()
        {
            tables = new List<Table>();
            InitializeTables();
        }

        private void InitializeTables()
        {
            tables.Add(new Table(1, 4));
            tables.Add(new Table(2, 5));
            tables.Add(new Table(3, 2));
            tables.Add(new Table(4, 3));
            tables.Add(new Table(5, 6));
        }
        public void DisplayTableList()
        {
            foreach (var table in tables)
            {
                string status = IsTableOccupied(table.TableNumber) ? "Occupied" : "Vacant";
                Console.WriteLine($"{table} - Status: {status}");
            }
        }
        public bool IsTableOccupied(int tableNumber)
        {
            Table table = GetTableDetails(tableNumber);
            return table != null && table.GetOrder() != null;
        }
        public Table GetTableDetails(int tableNumber)
        {
            return tables.Find(table => table.TableNumber == tableNumber);
        }
        public void MarkTableOccupied(int tableNumber)
        {
            Table table = GetTableDetails(tableNumber);
            if (table != null)
            {
                table.SetOrder(null);
            }
        }
        public void MarkTableVacant(int tableNumber)
        {

            Table table = GetTableDetails(tableNumber);
            if (table != null)
            {
                table.SetOrder(null);
                table.SetTotalAmountPaid(0); 
                Console.WriteLine($"Table {tableNumber} is now vacant.");
            }
        }
        public void ViewTableOrder(int tableNumber)
        {
            Table table = GetTableDetails(tableNumber);

            if (table != null && table.GetOrder() != null)
            {
                Order order = table.GetOrder();

                Console.WriteLine($"Order details for Table {tableNumber} - Seats: {table.NumberOfSeats}");
                Console.WriteLine($"Order Timestamp: {order.Timestamp}");

                Console.WriteLine("Items in the order:");
                foreach (var item in order.GetItems())
                {
                    Console.WriteLine($"{item.Name} - {item.Price} euros");
                }

                Console.WriteLine($"Total Amount Paid: {order.CalculateTotalAmount()} euros");
            }
            else
            {
                Console.WriteLine($"No order found for Table {tableNumber} or the table is vacant.");
            }
        }

    }
}
