﻿using System;

namespace Assessments
{
    public class Mini_SuperMarket
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> itemsName = new Dictionary<int, string>()
            {
                {101, "Milk" },
                {102, "CoconutOil" },
                {103, "Sugar" },
                {104, "Salt" },
                {105, "Rice" },
                {106,"Butter" }
            };

            Dictionary<int, double> itemsPrice = new Dictionary<int, double>()
            {
                {101, 20.5},
                {102, 50.0},
                {103, 15.5},
                {104, 20.7},
                {105, 50.0},
                {106, 30.5}
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------ Grovery Bill Calculator ------------------");
            Console.WriteLine("||     ||    ||     ||    ||     ||     ||    ||     ||    ||");
            Console.WriteLine("-------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("              Choose the Items for purchasing                   ");

            var itemsNameList = itemsName.ToList();
            var itemsPriceList = itemsPrice.ToList();
            
            for (int i = 0; i< itemsNameList.Count; i++)
            {
                Console.WriteLine($"{itemsNameList[i].Key, 18}. {itemsNameList[i].Value, -10}    Rs.{itemsPriceList[i].Value,-20}      ");
            }
            
            Console.WriteLine("                    For Billing Press '1'                  ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================================");
            Console.ResetColor();

            Dictionary<string, double> produts = new Dictionary<string, double>();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Here You can Enter the code of Items for purchasing");
            Console.ResetColor();

            int itemQuantity = 0;
            double totalPrice = 0;

            while (true)
            {
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

                bool flag = false;
                int quantity = 0;
                double total = 0;

                while (true)
                {
                    if (itemsPrice.ContainsKey(code))
                    {
                        Console.Write($"Enter the Quantity of {itemsName[code]} : ");
                        string input = Console.ReadLine();

                        if(int.TryParse(input, out quantity))    //using TryParse
                        {
                            total = itemsPrice[code] * quantity;
                            totalPrice += total;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Enter the Valid Quantity with using any Character or Symbols");
                            Console.ResetColor();
                        }

                        if (produts.ContainsKey(itemsName[code]))
                        {
                            produts[itemsName[code]] += total;
                        }
                        else
                        {
                            produts.Add(itemsName[code], total);
                            flag = true;
                        }

                        if (flag)
                        {
                            itemQuantity += 1;
                            flag = false;
                        }
                        quantity = 0;
                        total = 0;
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Code. Choose from Items Table");
                        Console.ResetColor();
                        break;
                    }
                    
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-------------------------------------------------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("                 Bill for Purchased Items                    ");
            Console.ResetColor();

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("       Items                |              Quantity Cost      ");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (KeyValuePair<string, double> i in produts)
            {
                Console.WriteLine($"\t{i.Key,-12} \t\t\t\t{i.Value,-10}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------------- Total Quantity - " + itemQuantity + " ---------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------- Total Amount - " + totalPrice + " -----------------------");
            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("             * ~   Thank You for Purchasing   ~ *             ");
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}