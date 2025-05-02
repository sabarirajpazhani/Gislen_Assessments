﻿using System;
using System.Collections;
using System.Transactions;

namespace Assessments
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message) { }
    }
    public class Mini_SuperMarket
    {
        public static void isValidString(string name)
        {
            if (char.IsDigit(name[0]) || char.IsDigit(name[1]))
            {
                throw new InvalidNameException("The Name Should not be Number make them to correct the Alphabet");
            }

            
        }
        static void Main(string[] args)
        {
            //Data
            Dictionary<int, string> itemsName = new Dictionary<int, string>()
                {
                    {101, "Milk" },
                    {102, "Oil" },
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

            Dictionary<string, (double price, int prodQuantity)> products = new Dictionary<string, (double price, int prodQuantity)>();         //Dictionary for storing pirce and quantity of items details

            Hashtable purchasedDetails = new Hashtable();        //for storing purchased details

            while (true)
            {
                 MainStart:
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("------------------------ Grocery Shop -----------------------");
                Console.WriteLine("||     ||    ||     ||    ||     ||     ||    ||     ||    ||");
                Console.WriteLine("-------------------------------------------------------------");
                Console.ResetColor();

                Console.WriteLine("                    Chooose the Operation                    ");
                Console.WriteLine("                1. Grocery Bill Calculator                   ");
                Console.WriteLine("                2. Add the Grocery Items (Only Owner)        ");
                Console.WriteLine("                3. Remove the Groceery Items (Only Owner)    ");
                Console.WriteLine("                4. View the Overall Sales (Only Owner)       ");
                Console.WriteLine("                5. View All the Items                        ");
                Console.WriteLine("                6. Exist                                     ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================================");
                Console.ResetColor();

                int Operation = 6;
 
                int choice = 0;

                Start1:
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enter the Choice for Grocery Operation : ");
                    Console.ResetColor();

                    choice = int.Parse(Console.ReadLine());

                    if(choice > Operation || choice == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Choice! Please Enter the Valid Number as Given in Table.");
                        Console.ResetColor();
                        goto Start1;
                    }
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choice! Please enter a valid Choice in Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                    Console.ResetColor();
                    goto Start1;
                }

                switch (choice)
                {
                    case 1:                        //Grocery Bill Calculator
                        //Console.Clear();

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("           You Choose the Grocery Bill Calculator           ");
                        Console.ResetColor();

                        Console.WriteLine();
                        Console.WriteLine();
                        //Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------ Grovery Bill Calculator ------------------");
                        Console.WriteLine("||     ||    ||     ||    ||     ||     ||    ||     ||    ||");
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine("              Choose the Items for purchasing                   ");

                        var itemsNameList = itemsName.ToList();
                        var itemsPriceList = itemsPrice.ToList();

                        for (int i = 0; i < itemsNameList.Count; i++)
                        {
                            Console.WriteLine($"{itemsNameList[i].Key,18}. {itemsNameList[i].Value,-10}    Rs.{itemsPriceList[i].Value,-20}      ");
                        }

                        Console.WriteLine("                    For Billing Press '1'                  ");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("============================================================");
                        Console.ResetColor();

                        String CustomerName ="Empty";

                        Custname:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Enter the Customer Name : ");                      //Customer Name
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Red;
                            String customerName = Console.ReadLine();
                            Console.ResetColor();

                            isValidString(customerName);
                            CustomerName = customerName;
                        }
                        catch (InvalidNameException e)
                        {
                            Console.WriteLine($"{e.Message}");
                            goto Custname;
                        } 

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Here You can Enter the code of Items for purchasing");
                        Console.ResetColor();
                        Console.WriteLine();

                        int itemQuantity = 0;
                        double totalPrice = 0;

                        while (true)
                        {
                            int code = 0;

                        Start2:
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
                                goto Start2;
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

                                    if (int.TryParse(input, out quantity))    //using TryParse
                                    {
                                        if (quantity <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Quantity must be greater than Zero");
                                            Console.ResetColor();
                                            goto Start2;
                                        }
                                        total = itemsPrice[code] * quantity;
                                        totalPrice += total;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Enter the Valid Quantity with using any Character or Symbols");
                                        Console.ResetColor();
                                    }



                                    //if (products.ContainsKey(itemsName[code]))
                                    //{

                                    //    products[itemsName[code]] += total;
                                    //}
                                    //else
                                    //{
                                    //    products.Add(itemsName[code], total);
                                    //    flag = true;    
                                    //}



                                    string item = itemsName[code];

                                    if (products.ContainsKey(item))
                                    {
                                        var existing = products[item];
                                        products[item] = (existing.price + total, existing.prodQuantity + quantity);
                                    }
                                    else
                                    {
                                        products[item] = (total, quantity);
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

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Name      : ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(CustomerName);
                        Console.ResetColor();

                        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Date      : ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(date);
                        Console.ResetColor();

                        Console.WriteLine("-------------------------------------------------------------");
                        Console.WriteLine("    Items           |     Quantity       | Cost Per Item    ");
                        Console.WriteLine("-------------------------------------------------------------");

                        foreach (KeyValuePair<string, (double price, int prodQuantity)> i in products)
                        {
                            Console.WriteLine($"{i.Key,-12} \t\t{i.Value.prodQuantity}\t\t\t{i.Value.price,-10}    ");
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-------------------- Total Quantity - " + itemQuantity + " ---------------------");
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("------------------- Total Amount - " + totalPrice + " -----------------------");
                        Console.ResetColor();

                        
                        Hashtable PriceAndDate = new Hashtable();
                        
                        PriceAndDate.Add("TotalPrice",totalPrice);
                        PriceAndDate.Add("Date",date);

                        purchasedDetails.Add(CustomerName,PriceAndDate);


                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("             * ~   Thank You for Purchasing :)  ~ *             ");
                        Console.ResetColor();

                        Console.WriteLine();
                        Console.WriteLine();


                        
                        //Console.ForegroundColor = ConsoleColor.Yellow;
                        //Console.Write("If you Want to Exist from Bill calculator (Y/N) : ");
                        //Console.ResetColor();
                        //char ch = char.Parse(Console.ReadLine());
                        //if(ch =='y' || ch == 'Y' )
                        //{
                        //    Console.Clear();
                        //    goto MainStart;
                        //}
                        //else
                        //{
                        //    break;
                        //}
                        //Console.ReadKey();
                        break;


                    case 2:                                                        //Add the Grocery Items (Only Owner) 

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("       You Choose to Add the Grocery Items (Only Owner)      ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();

                        int count = 0;

                        ErrorStart1:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("Enter the Number of Items to be Added in Database: ");
                            Console.ResetColor();
                            int Count = int.Parse(Console.ReadLine());
                            count = Count;

                        }
                        catch (FormatException e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input! Please enter a Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                            goto ErrorStart1;
                        }

                        for(int i =0; i<count; i++)
                        {

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Enter the Item Name to be Added: ");
                            Console.ResetColor();
                            String addItemsName = Console.ReadLine();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Enter the Code of {addItemsName}: ");
                            Console.ResetColor();
                            int addItemCode = int.Parse(Console.ReadLine());

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Enter the Price of {addItemsName}: ");
                            Console.ResetColor();
                            double addItemPrice = double.Parse(Console.ReadLine());

                            if (!itemsName.ContainsKey(addItemCode) && !itemsName.ContainsValue(addItemsName))
                            {
                                itemsName.Add(addItemCode, addItemsName);
                                itemsPrice.Add(addItemCode, addItemPrice);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Item added successfully.");
                                Console.ResetColor();
                            }
                            else
                            {
                                if(itemsName.ContainsKey(addItemCode) && itemsName.ContainsValue(addItemsName)){
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Both Item Name and Item Code is already exists. Skipping addition.");
                                    Console.ResetColor();
                                    i--;
                                }
                                else if (itemsName.ContainsKey(addItemCode))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Item code {addItemCode} already exists. Skipping addition.");
                                    Console.ResetColor();
                                    i--;
                                }

                                else if (itemsName.ContainsValue(addItemsName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Item name '{addItemsName}' already exists. Skipping addition.");
                                    Console.ResetColor();
                                    i--;
                                }
                            }


                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("                 --------------------------                  ");
                            Console.ResetColor();


                        }
                        //Console.ForegroundColor = ConsoleColor.DarkYellow;
                        //Console.Write("If you want to add somemore Items (Y/N): ");
                        //Console.ResetColor();

                        //char ch = char.Parse(Console.ReadLine());
                        //if (ch == 'y' || ch == 'Y')
                        //{
                        //    goto ErrorStart1;
                        //}
                        //else
                        //{
                        //    break;
                        //}
                        break;


                    case 3:                                                 //Removing the Grocery Items
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("       You Choose to Remove the Grocery Items (Only Owner)      ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();

                        int count1 = 0;

                        ErrorStart2:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("Enter the Number of Items to be Delete in Database: ");
                            Console.ResetColor();
                            int Count = int.Parse(Console.ReadLine());
                            count = Count;

                        }
                        catch (FormatException e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input! Please enter a Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                            goto ErrorStart2;
                        }

                        for (int i = 0; i < count; i++)
                        {

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Enter the Item Name to be Delete: ");
                            Console.ResetColor();
                            String addItemsName = Console.ReadLine();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"Enter the Code of {addItemsName}: ");
                            Console.ResetColor();
                            int addItemCode = int.Parse(Console.ReadLine());


                            if (itemsName.ContainsKey(addItemCode) && itemsName.ContainsValue(addItemsName) )
                            {
                                itemsName.Remove(addItemCode);
                                itemsPrice.Remove(addItemCode);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Item Removed successfully.");
                                Console.ResetColor();
                            }
                            else
                            {
                                if (!itemsName.ContainsKey(addItemCode) && !itemsName.ContainsValue(addItemsName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Both Item Name and Item Code is Not exists. Skipping addition.");
                                    Console.ResetColor();
                                    i--;
                                }
                                else if (!itemsName.ContainsKey(addItemCode))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Item code {addItemCode} Not exists. Skipping addition.");
                                    Console.ResetColor();
                                    i--;
                                }

                                else if (!itemsName.ContainsValue(addItemsName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Item name '{addItemsName}' Not exists. Skipping addition.");
                                    Console.ResetColor();
                                    i--;
                                }
                            }


                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("                 --------------------------                  ");
                            Console.ResetColor();
                        }

                        break;

                    case 4:                                                        //View the Overall Sales (Only Owner) 

                        StartAth:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("         You Choose to View the Overall Sales (Only Owner)        ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();


                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Enter the Owner User Name:  ");                      //Owner user name
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        String userName = Console.ReadLine();
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Enter the Owner Password:  ");                      //Owner Password
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        String pass = Console.ReadLine();
                        Console.ResetColor();

                        if (userName == "admin" && pass == "12345")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine("| Customer_Name\t\tPurchased_Cost\t\tPurchased_Date");
                            Console.WriteLine("-------------------------------------------------------------");

                            foreach (DictionaryEntry i in purchasedDetails)
                            {
                                Hashtable details = (Hashtable)i.Value;
                                Console.WriteLine($"\t{i.Key}\t\t{details["TotalPrice"]} \t\t\t{details["Date"]}");
                            }
                            Console.WriteLine("=============================================================");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Wrong User Nmae and password :(\n Re-Enter the User Name and Password");
                            Console.ResetColor();
                            goto StartAth;
                        }

                        break;


                    case 5:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("             You Choose to View all the Items :)             ");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("=============================================================");
                        Console.ResetColor();
                        Console.WriteLine("| Item_Code\t\tItem_Name\t\tItem_Price  |");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("=============================================================");
                        Console.ResetColor();

                        foreach(KeyValuePair<int, String > i in itemsName)
                        {
                            Console.WriteLine($"\t{i.Key}\t\t{i.Value} \t\t\t{itemsPrice[i.Key]}");
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"                     Total Items : ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(itemsName.Count);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.ResetColor();
                        //Console.ReadKey();
                        break;

                    case 6:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("                     You Choose to Exist :)                  ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();

                        for(int i =  5; i> 0; i--)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("                 Existing From Grocery: ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($" {i} ");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                        }

                       

                        break;
                }
                
                if(choice == 6)
                {
                    
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("                      ~ * Thank You * ~                    ");
                    Console.ResetColor();
                    break;
                }
            }
            Console.ReadKey();
        }
    }
}