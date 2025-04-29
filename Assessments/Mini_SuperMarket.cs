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

            Console.WriteLine("              Choose the Items for purchasing                 ");
            Console.WriteLine("              101. Milk              - Rs.20                  ");
            Console.WriteLine("              102. Coconut Oil       - Rs.50                  ");
            Console.WriteLine("              103. Sugar             - Rs.15                  ");
            Console.WriteLine("              104. Salt              - Rs.20                  ");
            Console.WriteLine("              105. Rice              - Rs.50                  ");
            Console.WriteLine("              106. Butter            - Rs.30                  ");

            Console.WriteLine("                    For Billing Press '1'                  ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("____________________________________________________________");
            Console.WriteLine("-------------------------------------------------------------");
            Console.ResetColor();

            Dictionary<int, int> itemsPrice = new Dictionary<int, int>();
            itemsPrice.Add(101, 20);
            itemsPrice.Add(102, 50);
            itemsPrice.Add(103, 15);
            itemsPrice.Add(104, 20);
            itemsPrice.Add(105, 50);
            itemsPrice.Add(106, 30);

            Dictionary<int, string> itemsName = new Dictionary<int, string>()
            {
                {101, "Milk" },
                {102, "Coconut Oil" },
                {103, "Sugar" },
                {104, "Salt" },
                {105, "Rice" },
                {106,"Butter" }
            };

            Dictionary<string, int> produts = new Dictionary<string, int>();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Here You can Enter the code of Items for purchasing");
            Console.ResetColor();

            int quantity = 0;
            int total = 0;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter the Item Code / Proceed Bill Enter '1': ");
                Console.ResetColor();

                int code = int.Parse(Console.ReadLine());

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
                        quantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity1 = int.Parse(Console.ReadLine());
                        int totalPrice1 = itemsPrice[code] * quantity1;
                        total += totalPrice1;
                        produts.Add(itemsName[code], totalPrice1);
                        break;

                    case 102:
                        quantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity2 = int.Parse(Console.ReadLine());
                        int totalPrice2 = itemsPrice[code] * quantity2;
                        total += totalPrice2;
                        produts.Add(itemsName[code], totalPrice2);
                        break;

                    case 103:
                        quantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity3 = int.Parse(Console.ReadLine());
                        int totalPrice3 = itemsPrice[code] * quantity3;
                        total += totalPrice3;
                        produts.Add(itemsName[code], totalPrice3);
                        break;

                    case 104:
                        quantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity4 = int.Parse(Console.ReadLine());
                        int totalPrice4 = itemsPrice[code] * quantity4;
                        total += totalPrice4;
                        produts.Add(itemsName[code], totalPrice4);
                        break;

                    case 105:
                        quantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity5 = int.Parse(Console.ReadLine());
                        int totalPrice5 = itemsPrice[code] * quantity5;
                        total += totalPrice5;
                        produts.Add(itemsName[code], totalPrice5);
                        break;

                    case 106:
                        quantity += 1;
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        int quantity6 = int.Parse(Console.ReadLine());
                        int totalPrice6 = itemsPrice[code] * quantity6;
                        total += totalPrice6;
                        produts.Add(itemsName[code], totalPrice6);
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
            Console.WriteLine("\tItems\t\tQuantity Cost");
            foreach (KeyValuePair<string, int> i in produts)
            {
                Console.WriteLine($"\t{i.Key}\t\t{i.Value}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------Total Quantity - " + quantity + "---------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------- Total Amount - " + (decimal)total + " -----------------------");
            Console.ResetColor();
        }
    }
}