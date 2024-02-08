using RestorantSystemExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestorantSystemExam.Repository
{
    internal class RestaurantVoucherManager
    {
        public static void SaveRestaurantVoucherToFile(Order order)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("Voucher.txt"))
                {
                    writer.WriteLine("Restaurant Voucher:");
                    writer.WriteLine($"Order Timestamp: {order.Timestamp}");
                    writer.WriteLine("Items in the order:");
                    foreach (var item in order.GetItems())
                    {
                        writer.WriteLine($"{item.Name} - {item.Price} euros");
                    }
                    writer.WriteLine($"Total Amount Paid: {order.CalculateTotalAmount()} euros");
                }
                Console.WriteLine("Restaurant voucher saved to file: Voucher.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving restaurant voucher: {ex.Message}");
            }
        }
    }
}
