using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace InventoryManager
{
	public class Product
	{
		public required string Name { get; set; }
		public required decimal Price { get; set; }
		public required int Stock { get; set; }

		[SetsRequiredMembers]
		public Product(string name, decimal price, int stock)
		{
			Name = name;
			Price = price;
			Stock = stock;
		}
	}

	public class Inventory
	{
		private List<Product> products = new List<Product>();
		public int GetProductsCount() { return products.Count; }
		public void AddProduct()
		{
			Console.WriteLine("Add a product");
			Console.WriteLine(("").PadRight(13, '-'));
			Console.WriteLine("Enter product details:");
			string name = GetValidInput("Name");
			decimal price = decimal.Parse(GetValidInput("Price"));
			int stock = int.Parse(GetValidInput("Stock level"));

			var product = new Product(name, price, stock);
			if (product == null)
			{
				Console.WriteLine("Invalid product attribute(s).");
			}
			else
			{
				products.Add(product);
				Console.WriteLine();
				Console.Write("Product added");
				Console.ReadLine();
			}
		}

		public void ViewProducts()
		{
			Console.WriteLine("View all products");
			Console.WriteLine(("").PadRight(17, '-'));
			Console.WriteLine("{0,-5} {1,-20} {2,10} {3,10}", "N", "Name", "Price", "Stock");
			Console.WriteLine(("").PadRight(5, '-') + " " + ("").PadRight(20, '-') + " " + ("").PadRight(10, '-') + " " + ("").PadRight(10, '-'));
			for (int i = 0; i < products.Count; i++)
			{
				Console.WriteLine("{0,-5} {1,-20} {2,10:C} {3,10:N0}", i + 1, products[i].Name, products[i].Price, products[i].Stock);
			}
			Console.WriteLine();
			Console.Write("End of list");
			Console.ReadLine();
			Console.WriteLine();
		}

		public void SellProduct()
		{
			Console.WriteLine("Sell a product");
			Console.WriteLine(("").PadRight(14, '-'));
			int productNumber = SelectProduct();
			Console.Write("Quantity: ");
			int quantity = int.Parse(Console.ReadLine());
			if (quantity > products[productNumber].Stock)
			{
				Console.WriteLine("Quantity exceeds current stock.");
			}
			else
			{
				products[productNumber].Stock -= quantity;
				Console.WriteLine();
				Console.WriteLine("Total value: {0:C}", products[productNumber].Price * quantity);
				Console.Write("Product sold.");
				Console.ReadLine();
			}
		}

		public void RestockProduct()
		{
			Console.WriteLine("Restock a product");
			Console.WriteLine(("").PadRight(17, '-'));
			int productNumber = SelectProduct();
			Console.Write("Amount: ");
			int productAmount = int.Parse(Console.ReadLine());
			if (productAmount < 0)
			{
				Console.WriteLine("Product amount cannot be less than 0.");
			}
			else
			{
				products[productNumber].Stock += productAmount;
				Console.WriteLine();
				Console.Write("Product restocked.");
				Console.ReadLine();
			}
		}

		public void RemoveProduct()
		{
			Console.WriteLine("Remove a product");
			Console.WriteLine(("").PadRight(16, '-'));
			int productNumber = SelectProduct();
			products.RemoveAt(productNumber);
			Console.WriteLine();
			Console.Write("Product removed.");
			Console.ReadLine();
		}

		private string GetValidInput(string property)
		{
			string? inputStr = null;
			while (inputStr == null)
			{
				Console.Write("{0}: ", property);
				inputStr = Console.ReadLine();
				if (inputStr == null)
				{
					Console.WriteLine("Invalid input");
				}
			}

			return inputStr;
		}

		private int SelectProduct()
		{
			Console.WriteLine("{0,-5} {1,-20} {2,10}", "N", "Name", "Stock");
			Console.WriteLine(("").PadRight(5, '-') + " " + ("").PadRight(20, '-') + " " + ("").PadRight(10, '-'));
			for (int i = 0; i < products.Count; i++)
			{
				Console.WriteLine("{0,-5} {1,-20} {2,10:N0}", i + 1, products[i].Name, products[i].Stock);
			}
			Console.WriteLine();
			Console.Write("Enter product number: ");
			int selection = int.Parse(Console.ReadLine()) - 1;

			return selection;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var inventory = new Inventory();

			Console.WriteLine("Inventory Management System");
			Console.WriteLine(("").PadRight(27,'-'));
			Console.WriteLine();
			bool exit = false;
			while (!exit)
			{
				int selection = DisplayMainMenu(inventory.GetProductsCount());
				Console.WriteLine();
				if (inventory.GetProductsCount() > 0)
				{
					switch (selection)
					{
						case 0: // View all products
							inventory.ViewProducts();
							break;
						case 1: // Add a product
							inventory.AddProduct();
							break;
						case 2: // Sell a product
							inventory.SellProduct();
							break;
						case 3: // Restock a product
							inventory.RestockProduct();
							break;
						case 4: // Remove a product
							inventory.RemoveProduct();
							break;
						case 5: // Exit
							Console.WriteLine("Exiting the application.");
							exit = true;
							break;
					}
				}
				else
				{
					switch (selection)
					{
						case 0: // Add a product
							inventory.AddProduct();
							break;
						case 1: // Exit
							Console.WriteLine("Exiting the application");
							exit = true;
							break;
					}
				}
				Console.WriteLine("");
			}
		}

		public static int DisplayMainMenu(int inventoryProductsCount)
		{
			int selection = -1;
			bool isValidOption = false;

			while (!isValidOption)
			{
				Console.WriteLine("Main Menu:");
				Console.WriteLine(("").PadRight(21,'-'));
				string[] menuOptions = GetMainMenuOptions(inventoryProductsCount);
				for (int i = 0; i < menuOptions.Length; i++)
				{
					Console.WriteLine($"{i + 1} - {menuOptions[i]}");
				}
				Console.WriteLine(("").PadRight(21,'-'));
				Console.Write("Selection: ");
				selection = int.Parse(Console.ReadLine());

				if (ValidateMenuSelection(menuOptions, selection))
				{
					isValidOption = true;
				}
				else
				{
					Console.WriteLine("Invalid option.\n");
				}
			}

			return selection - 1;
		}

		public static string[] GetMainMenuOptions(int inventoryProductsCount)
		{
			string[] fullMainMenu =
			{
				"View all products",
				"Add a product",
				"Sell a product",
				"Restock a product",
				"Remove a product",
				"Exit"
			};

			string[] noProductMainMenu =
			{
				"Add a product",
				"Exit"
			};

			return inventoryProductsCount > 0 ? fullMainMenu : noProductMainMenu;
		}

		public static bool ValidateMenuSelection(string[] menuOptions, int selection)
		{
			return selection > 0 && selection <= menuOptions.Length;
		}
	}
}



