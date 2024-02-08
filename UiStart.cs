using RestorantSystemExam.Models;
using RestorantSystemExam.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestorantSystemExam
{
    internal class UiStart
    {
        public void ProjectRun()
        {
            Menu menu = new Menu();
            TableManager tableManager = new TableManager();
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Restaurant Ordering System.");
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Display Table List");
                Console.WriteLine("2. Place an Order");
                Console.WriteLine("3. Mark Table Vacant");
                Console.WriteLine("4. View Table Order");
                Console.WriteLine("5. Print Customer Voucher");
                Console.WriteLine("6. Save Restaurant Voucher");
                Console.WriteLine("0. To Exit");

                Console.WriteLine("Enter your choice");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            ExitProgram();
                            break;
                        case 1:
                            DisplayTableList(tableManager);
                            break;
                        case 2:
                            PlaceOrder(menu, tableManager);
                            break;
                        case 3:
                            MarkTableVacant(tableManager);
                            break;
                        case 4:
                            ViewTableOrder(tableManager);
                            break;
                        case 5:
                            PrintCustomerVoucher(tableManager); 
                            break;
                        case 6:
                            SaveRestaurantVoucher(tableManager);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
        private static void ExitProgram()
        {
            Console.WriteLine("Exiting the Restaurant Ordering System!");
            Environment.Exit(0);
        }
        private static void DisplayTableList(TableManager tableManager)
        {
            Console.WriteLine("Table List:");
            tableManager.DisplayTableList();
        }
        private static void PlaceOrder(Menu menu, TableManager tableManager)
        {
            Console.WriteLine("Enter the table number to place an order (0 to cancel): ");
            int tableNumber;
            if (int.TryParse(Console.ReadLine(), out tableNumber))
            {
                if (tableNumber == 0)
                {
                    Console.WriteLine("Order placement canceled");
                    return;
                }
                if (tableManager.IsTableOccupied(tableNumber))
                {
                    Console.WriteLine("Table is already occupied. Please choose another table.");
                    return;
                }
                Order tableOrder = new Order(tableManager.GetTableDetails(tableNumber), DateTime.Now);
                Table selectedTable = tableManager.GetTableDetails(tableNumber);
                Console.WriteLine("Menu:");
                menu.DisplayMenu();
                while (true)
                {
                    Console.WriteLine("Enter the item number to add to the order (0 to Finish): ");
                    int itemChoice;
                    if (int.TryParse(Console.ReadLine(), out itemChoice))
                    {
                        if (itemChoice == 0)
                        {
                            break;
                        }
                        MenuItem selectedItem = menu.GetItem(itemChoice);
                        if (selectedItem != null)
                        {
                            tableOrder.AddToOrder(selectedItem);
                            Console.WriteLine($"{selectedItem.Name} add to the order.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid item number. Plaese try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid item number.");
                    }
                }
                Console.WriteLine("Order Summary:");
                Console.WriteLine(tableOrder);
                decimal totalAmount = tableOrder.CalculateTotalAmount();
                Console.WriteLine($"Total Amount: {totalAmount} euros");
                tableManager.MarkTableOccupied(tableNumber);
                selectedTable.SetOrder(tableOrder);
                selectedTable.SetTotalAmountPaid(totalAmount);
                Console.WriteLine($"Table {tableNumber} is now occupied.");
                Console.WriteLine($"Order Timestamp: {tableOrder.Timestamp}");
                Console.WriteLine($"Total Amount Paid; {totalAmount} euros");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid table number.");
            }
        }
        private static void MarkTableVacant(TableManager tableManager)
        {
            Console.WriteLine("Enter the table number to mark as vacant");
            int vacantTableNumber;
            if (int.TryParse(Console.ReadLine(), out vacantTableNumber))
            {
                tableManager.MarkTableVacant(vacantTableNumber);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid table number");
            }
        }
        private static void ViewTableOrder(TableManager tableManager)
        {
            Console.WriteLine("Enter the table number  to view the order details:");
            int viewTableNumber = int.Parse(Console.ReadLine());
            tableManager.ViewTableOrder(viewTableNumber);
        }
        private static void SaveRestaurantVoucher(TableManager tableManager)
        {
            Console.WriteLine("Enter the table number to save restaurant voucher:");
            int tableNumber = int.Parse(Console.ReadLine());

            Table table = tableManager.GetTableDetails(tableNumber);

            if (table != null && table.GetOrder() != null)
            {
                Order order = table.GetOrder();
                RestaurantVoucherManager.SaveRestaurantVoucherToFile(order); 
            }
            else
            {
                Console.WriteLine($"No order found for Table {tableNumber} or the table is vacant.");
            }
        }

        private static void PrintCustomerVoucher(TableManager tableManager)
        {
            Console.WriteLine("Enter the table number to print customer voucher:");
            int tableNumber = int.Parse(Console.ReadLine());

            Table table = tableManager.GetTableDetails(tableNumber);

            if (table != null && table.GetOrder() != null)
            {
                Order order = table.GetOrder();
                PrintAndSaveCustomerVoucher(order);
            }
            else
            {
                Console.WriteLine($"No order found for Table {tableNumber} or the table is vacant.");
            }
        }

        private static void PrintAndSaveCustomerVoucher(Order order)
        {
            CustomerVoucherManager.PrintCustomerVoucher(order);
            Console.WriteLine("Do you want to print this voucher? (Y/N)");
            string printChoice = Console.ReadLine().ToUpper();

            if (printChoice == "Y")
            {
                Console.WriteLine("Printing.....");
                CustomerVoucherManager.SaveCustomerVoucherToFile(order);
            }
        }
    }
}
