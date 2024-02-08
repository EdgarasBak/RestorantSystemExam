using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestorantSystemExam.Models;

namespace RestorantSystemExam.Repository
{
    internal class Menu
    {
        private List<MenuItem> items;

        public Menu()
        {
            items = new List<MenuItem>();
            LoadMenuFromFiles();

        }
        public void LoadMenuFromFiles()
        {
            LoadMenuFromFile("food.txt");
            LoadMenuFromFile("drinks.txt");
        }
        private void LoadMenuFromFile(string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal price))
                    {
                        items.Add(new MenuItem(parts[0], price));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading menu from {fileName}: {ex.Message}");
            }
        }
        public void DisplayMenu()
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Name} - {items[i].Price} euros");
            }
        }

        public MenuItem GetItem(int index)
        {
            if (index >= 1 && index <= items.Count)
            {
                return items[index - 1];
            }
            return null;
        }

    }
}
