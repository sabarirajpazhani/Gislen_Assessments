using System;
using System.Collections;
using System.Threading.Channels;

namespace Assessment2
{
    public class ExpenseAndPrice
    {
        public string ExpenseName { get; set; }
        public int ExpenseAmount { get; set; }
    }

    public class NameAndPass
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public DateOnly Date {  get; set; } 
    }

    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message) { }
    }

    public class Daily_Expense_Tracker
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
            Dictionary<int, Hashtable> Expense = new Dictionary<int, Hashtable>();          //code and expens

            Hashtable EnameAndAmount = new Hashtable();                                     // Expense name and amount

            Dictionary<int, Hashtable> CodeEuserNamePass = new Dictionary<int, Hashtable>();          // code and (User nane and pass from Hastable(userNamaePass))

            Hashtable userNamePass = new Hashtable();                                //for storing user name and pass
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("--------------------- Daily Expense Tracker ----------------------");
                Console.WriteLine("||    ||    ||   ||    ||    ||    ||    ||    ||    ||    ||   ||");
                Console.WriteLine("------------------------------------------------------------------");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("                      Choose the Operation                        ");
                Console.ResetColor();
                Console.WriteLine("                    1. Enter the Expense to Record                ");
                Console.WriteLine("                    2. View all the Expese                        ");
                Console.WriteLine("                    3. Total Amount Spent                         ");
                Console.WriteLine("                    4. Highest and Lowest Expense                 ");
                Console.WriteLine("                    5. Average Expense                            ");
                Console.WriteLine("                    6. Exist                                      ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("------------------------------------------------------------------");
                Console.ResetColor();


                int choice = 0;
                int Operation = 6;

                Choice:
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enter the Choice for the Daily Expense Tracker : ");
                    Console.ResetColor();

                    int Choice = int.Parse(Console.ReadLine());

                    if (Choice > Operation || Choice == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Choice! Please Enter the Valid Number as Given in Header");
                        goto Choice;
                    }
                    else
                    {
                        choice = Choice;
                    }
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choice! Please enter a valid Choice in Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                    Console.ResetColor();
                    goto Choice;
                }

                int userCode = 100;

                switch (choice)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("==================================================================");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("                  Enter '1' to record an expense.                 ");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("==================================================================");
                        Console.ResetColor();

                        

                        String name = "Empty";
                        Name:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Enter the Your Name : ");                      //User Name
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Red;
                            String Name = Console.ReadLine();
                            Console.ResetColor();

                            isValidString(Name);
                            name = Name;
                        }
                        catch (InvalidNameException e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{e.Message}");
                            Console.ResetColor();
                            goto Name;
                        }

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Enter the Password Name : ");                      //User Password
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        String password = Console.ReadLine();
                        Console.ResetColor();

                        userCode += 1;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Here Your User Code for Futhur Operation ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Your User Code: ");                                  //Automnatic User code
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(userCode);
                        Console.ResetColor();

                        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Please remember this code; it is used for authentication purposes.");
                        Console.ResetColor();
                        Console.WriteLine("                          ~~~~~~~~~~~~~                           ");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("                  Here You can Enter the Expense Records               ");
                        Console.ResetColor();
                        Console.WriteLine();

                        int numberOfRecords = 0;
                        Records:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write("How may Expense You want to Record : ");
                            Console.ResetColor();

                            int records = int.Parse(Console.ReadLine());

                            if (records > 5)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The maximum number of records is 5, so please enter up to 5 expense records.");
                                goto Records;
                            }
                            else
                            {
                                numberOfRecords = records;
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Choice! Please enter a valid Choice in Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                            Console.ResetColor();
                            goto Records;
                        }

                        NameAndPass nameAndPass = new NameAndPass();             // adding password and username in dictionary
                        nameAndPass.UserName = name;
                        nameAndPass.UserPassword = password;
                        nameAndPass.Date = date;
                        
                        List<NameAndPass> namePassList = new List<NameAndPass>();
                        namePassList.Add(nameAndPass);

                        userNamePass[userCode] = namePassList;
                        CodeEuserNamePass.Add(userCode, userNamePass);


                        List<ExpenseAndPrice> expenseAndPriceList = new List<ExpenseAndPrice>();   //List to store the exoense and its amount in list

                        for (int i = 0; i < numberOfRecords; i++)
                        {
                            ExpenseAndPrice NamePrice = new ExpenseAndPrice();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"Enter the name for Expense {i+1} : ");
                            Console.ResetColor();
                            NamePrice.ExpenseName = Console.ReadLine();

                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"Enter the Amount for {NamePrice.ExpenseName} : ");
                            Console.ResetColor();
                            NamePrice.ExpenseAmount = int.Parse(Console.ReadLine());
                            

                            expenseAndPriceList.Add(NamePrice);         //Adding name and Price in list
                        }


                        EnameAndAmount[userCode] = expenseAndPriceList;           //addling list with code (key) in hashtable

                        Expense.Add(userCode, EnameAndAmount);                   //adding hashtables (code, price and ammount) in dictionary





                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("                   Successfully Recorded :)                        ");
                        Console.ResetColor();

                        Console.WriteLine();
                        Console.WriteLine("----------------------Your Expense Records------------------------");
                        Console.ResetColor();
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("User Name : ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(name);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("User Code : ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(userCode);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Date : ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(date);
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------------------------------------------");
                        Console.WriteLine("|  \tExpense_Name\t\t\tAmount_Spent\t        |");
                        Console.WriteLine("------------------------------------------------------------------");
                        Console.ResetColor();

                        int totalAmount = 0;                     //Total Amount for the expense

                        foreach (KeyValuePair<int, Hashtable> i in Expense)    //For Dictionary
                        {
                            foreach(DictionaryEntry e in i.Value)             //For HashTable
                            {
                                List<ExpenseAndPrice> expenseList = new List<ExpenseAndPrice>();

                                foreach(ExpenseAndPrice k in expenseAndPriceList)
                                {
                                    totalAmount += k.ExpenseAmount;
                                    Console.WriteLine($"   \t{k.ExpenseName}\t\t\t\t{k.ExpenseAmount}");
                                }
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------------------------------------------");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"                       Total Amount : ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(totalAmount);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue ;
                        Console.WriteLine("                     ~ * Thank You * ~                           ");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();

                        Console.WriteLine("Press any Key to Continue....");
                        Console.ReadKey();
                        //foreach(KeyValuePair<int, Hashtable> i in Expense)                                  //Ecpense and amount
                        //{
                        //    Console.WriteLine("Code :"+ i.Key);

                        //    foreach(DictionaryEntry e in i.Value) { 
                        //        List<ExpenseAndPrice> expenses = (List<ExpenseAndPrice>)e.Value;

                        //        foreach(ExpenseAndPrice k in expenses)
                        //        {
                        //            Console.WriteLine(k.ExpenseName +" - " +k.ExpenseAmount);
                        //        }

                        //    }
                        //}




                        //foreach (KeyValuePair<int, Hashtable> i in CodeEuserNamePass)                                //UserName and Password
                        //{
                        //    Console.WriteLine("Code :" + i.Key);

                        //    foreach (DictionaryEntry e in i.Value)
                        //    {
                        //        List<NameAndPass> expenses = (List<NameAndPass>)e.Value;

                        //        foreach (NameAndPass k in expenses)
                        //        {
                        //            Console.WriteLine(k.UserName + " - " + k.UserPassword);
                        //        }

                        //    }
                        //}
                        break;
                }
            }
        }

    }
}