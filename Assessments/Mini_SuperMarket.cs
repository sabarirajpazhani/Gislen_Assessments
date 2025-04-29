using System;

namespace Assessments
{
    public class Mini_SuperMarket
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------ Grovery Bill Calculator ------------------");
            Console.WriteLine("||     ||    ||     ||    ||     ||     ||    ||     ||    ||");
            Console.WriteLine("-------------------------------------------------------------");
            Console.ResetColor();

            Console.WriteLine("              Choose the Items for purchasing                   ");
            Console.WriteLine("              101. Milk              - Rs.20.5                  ");
            Console.WriteLine("              102. Coconut Oil       - Rs.50.0                  ");
            Console.WriteLine("              103. Sugar             - Rs.15.5                  ");
            Console.WriteLine("              104. Salt              - Rs.20.7                  ");
            Console.WriteLine("              105. Rice              - Rs.50.0                  ");
            Console.WriteLine("              106. Butter            - Rs.30.5                  ");

            Console.WriteLine("                    For Billing Press '1'                  ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("____________________________________________________________");
            Console.WriteLine("-------------------------------------------------------------");
            Console.ResetColor();

            Dictionary<int, double> itemsPrice = new Dictionary<int, double>();
            itemsPrice.Add(101, 20.5);
            itemsPrice.Add(102, 50.0);
            itemsPrice.Add(103, 15.5);
            itemsPrice.Add(104, 20.7);
            itemsPrice.Add(105, 50.0);
            itemsPrice.Add(106, 30.5);

            Dictionary<int, string> itemsName = new Dictionary<int, string>()
            {
                {101, "Milk" },
                {102, "Coconut Oil" },
                {103, "Sugar" },
                {104, "Salt" },
                {105, "Rice" },
                {106,"Butter" }
            };

            Dictionary<string, double> produts = new Dictionary<string, double>();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Here You can Enter the code of Items for purchasing");
            Console.ResetColor();

            int itemQuantity = 0;
            double total = 0;

            while (true)
            {
                //int code = int.Parse(Console.ReadLine());
                int code = 0;

                Start:
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enter the Item Code / Proceed Bill Enter '1': ");
                    Console.ResetColor();

                    code = int.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a valid Code in Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                    Console.ResetColor();
                    goto Start;
                }

                if (code == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Thank You For Purchasing");
                    Console.ResetColor();
                    break;
                }

                switch (code)
                {
                    case 101:

                        itemQuantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity1 = int.Parse(Console.ReadLine());
                        double totalPrice1 = itemsPrice[code] * quantity1;
                        total += totalPrice1;

                        if (produts.ContainsKey(itemsName[code]))
                        {
                            produts[itemsName[code]] += totalPrice1;
                        }
                        else
                        {
                            produts.Add(itemsName[code], totalPrice1);
                        }
                        break;

                    case 102:
                        itemQuantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity2 = int.Parse(Console.ReadLine());
                        double totalPrice2 = itemsPrice[code] * quantity2;
                        total += totalPrice2;

                        if (produts.ContainsKey(itemsName[code]))
                        {
                            produts[itemsName[code]] += totalPrice2;
                        }
                        else
                        {
                            produts.Add(itemsName[code], totalPrice2);
                        }
                        break;

                    case 103:
                        itemQuantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity3 = int.Parse(Console.ReadLine());
                        double totalPrice3 = itemsPrice[code] * quantity3;
                        total += totalPrice3;

                        if (produts.ContainsKey(itemsName[code]))
                        {
                            produts[itemsName[code]] += totalPrice3;
                        }
                        else
                        {
                            produts.Add(itemsName[code], totalPrice3);
                        }
                        break;

                    case 104:
                        itemQuantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity4 = int.Parse(Console.ReadLine());
                        double totalPrice4 = itemsPrice[code] * quantity4;
                        total += totalPrice4;
                        if (produts.ContainsKey(itemsName[code]))
                        {
                            produts[itemsName[code]] += totalPrice4;
                        }
                        else
                        {
                            produts.Add(itemsName[code], totalPrice4);
                        }
                        break;

                    case 105:
                        itemQuantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity5 = int.Parse(Console.ReadLine());
                        double totalPrice5 = itemsPrice[code] * quantity5;
                        total += totalPrice5;
                        if (produts.ContainsKey(itemsName[code]))
                        {
                            produts[itemsName[code]] += totalPrice5;
                        }
                        else
                        {
                            produts.Add(itemsName[code], totalPrice5);
                        }
                        break;

                    case 106:
                        itemQuantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity6 = int.Parse(Console.ReadLine());
                        double totalPrice6 = itemsPrice[code] * quantity6;
                        total += totalPrice6;
                        if (produts.ContainsKey(itemsName[code]))
                        {
                            produts[itemsName[code]] += totalPrice6;
                        }
                        else
                        {
                            produts.Add(itemsName[code], totalPrice6);
                        }
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You Entered Worng Item Code");
                        Console.ResetColor();
                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("Purchased Items");
            Console.WriteLine("       Items                             Quantity Cost       ");
            foreach (KeyValuePair<string, double> i in produts)
            {
                Console.WriteLine($"  {i.Key}                               {i.Value}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------Total Quantity - " + itemQuantity + "---------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------- Total Amount - " + total + " -----------------------");
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}