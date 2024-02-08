using RestorantSystemExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestorantSystemExam.Repository
{
    internal class CustomerVoucherManager
    {
        public static void PrintAndSaveCustomerVoucher(Order order)
        {
            PrintCustomerVoucher(order);
            SaveCustomerVoucherToFile(order);
        }

        public static void PrintCustomerVoucher(Order order)
        {

            Console.WriteLine("Customer Voucher:");
            Console.WriteLine($"Order Timestamp: {order.Timestamp}");
            Console.WriteLine("Items in the order:");
            foreach (var item in order.GetItems())
            {
                Console.WriteLine($"{item.Name} - {item.Price} euros");
            }
            Console.WriteLine($"Total Amount Paid: {order.CalculateTotalAmount()} euros");
        }

        public static void SaveCustomerVoucherToFile(Order order)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("CustomerVoucher.txt"))
                {
                    writer.WriteLine("Customer Voucher:");
                    writer.WriteLine($"Order Timestamp: {order.Timestamp}");
                    writer.WriteLine("Items in the order:");
                    foreach (var item in order.GetItems())
                    {
                        writer.WriteLine($"{item.Name} - {item.Price} euros");
                    }
                    writer.WriteLine($"Total Amount Paid: {order.CalculateTotalAmount()} euros");
                }
                Console.WriteLine("Customer voucher saved to file: CustomerVoucher.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving customer voucher: {ex.Message}");
            }
        }
    }
}
