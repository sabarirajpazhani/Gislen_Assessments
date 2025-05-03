using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Xml.Linq;

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
        public string UserEmail { get; set; }
        public DateOnly Date { get; set; }
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
                throw new InvalidNameException("The name should not contain numbers, must consist only of alphabets, and must be at least 3 characters long");
            }
        }

        public static bool isValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";

            return Regex.IsMatch(email, pattern);
        }

        public static bool verifyNameEmail(Dictionary<int, Hashtable> CodeEuserNamePass, int UserCodeAuth)   //Verfiy the user Name and Email
        {
            string UserNameAuth = "Empty";
            string UserEmailAuth = "Empty";

            bool verifyName = false;
            bool verifyEmail = false;   

            UserName:
            try
            {

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter the User Name: ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                String userNameAuth = Console.ReadLine();
                Console.ResetColor();
                if (userNameAuth.Length <3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please Enter the Name Properly");
                    Console.ResetColor();
                    goto UserName;
                }
                isValidString(userNameAuth);
                Hashtable userName = CodeEuserNamePass[UserCodeAuth];
                List<NameAndPass> NameList = (List<NameAndPass>)userName[UserCodeAuth];
                String storingName = NameList[0].UserName;
                if (storingName == userNameAuth)
                {
                    UserNameAuth = userNameAuth;
                    verifyName = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The name is not valid for this code");
                    Console.ResetColor();
                    goto UserName;
                }

            UserEmail:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Enter the User Email: ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                String userEmailAuth = Console.ReadLine();
                Console.ResetColor();

                if (UserEmailAuth.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please Enter the Email");
                    Console.ResetColor();
                    goto UserEmail;
                }
                else if (!isValidEmail(userEmailAuth))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please Enter valid Email");
                    Console.ResetColor();
                    goto UserEmail;
                }
                else
                {
                    Hashtable userEmail = CodeEuserNamePass[UserCodeAuth];
                    List<NameAndPass> EmailList = (List<NameAndPass>)userEmail[UserCodeAuth];
                    String StoredUser = EmailList[0].UserEmail;
                    if (StoredUser == userEmailAuth)
                    {
                        UserEmailAuth = userEmailAuth;
                        verifyEmail = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("User Email not Match for this Code");
                        goto UserEmail;
                    }
                }
            }
            catch (InvalidNameException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                goto UserName;
            }

            return (verifyEmail && verifyName);
        }

        public static void displayExpensePrice(Dictionary<int, Hashtable> CodeEuserNamePass, int UserCodeAuth, Dictionary<int, Hashtable> Expense)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("User Name : ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Hashtable userName = CodeEuserNamePass[UserCodeAuth];
            List<NameAndPass> nameList = (List<NameAndPass>)userName[UserCodeAuth];
            Console.WriteLine(nameList[0].UserName);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("User Code : ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(UserCodeAuth);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Date : ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Hashtable dateData = CodeEuserNamePass[UserCodeAuth];
            List<NameAndPass> dateList = (List<NameAndPass>)dateData[UserCodeAuth];
            Console.WriteLine(dateList[0].Date);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("| S.No  \tExpense_Name\t\t\tAmount_Spent\t        |");
            Console.WriteLine("------------------------------------------------------------------");
            Console.ResetColor();

            int sn = 1;
            Hashtable userExpanseTable = Expense[UserCodeAuth];
            foreach (DictionaryEntry i in userExpanseTable)
            {
                List<ExpenseAndPrice> expenseList = i.Value as List<ExpenseAndPrice>;
                foreach (ExpenseAndPrice k in expenseList)
                {
                    Console.WriteLine($" {sn}   \t{k.ExpenseName}\t\t\t\t{k.ExpenseAmount}");
                    sn++;
                }
                
            }
            sn = 0;
        }

        public static bool VerifyEmailOnly(Dictionary<int, Hashtable> CodeEuserNamePass, String email)
        {
            bool result = false;

            foreach(KeyValuePair<int, Hashtable> i in CodeEuserNamePass)
            {
                foreach(DictionaryEntry e in i.Value)
                {
                    List<NameAndPass> emailList = (List<NameAndPass>)e.Value;
                    foreach(NameAndPass k in emailList)
                    {
                        if(k.UserEmail == email)
                        {
                            result = true;
                            break;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        static int userCode = 100;

        static void Main(string[] args)
        {
            Dictionary<int, Hashtable> Expense = new Dictionary<int, Hashtable>();          //code and expens



            Dictionary<int, Hashtable> CodeEuserNamePass = new Dictionary<int, Hashtable>();          // code and (User nane and pass from Hastable(userNamaePass))



            Hashtable Total = new Hashtable();         //use code and total amount



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
                Console.WriteLine("                    3. Expense Summary                            ");
                Console.WriteLine("                         - Total Amount Spent                     ");
                Console.WriteLine("                         - Highest and Lowest Expense             ");
                Console.WriteLine("                         - Average Expense                        ");
                Console.WriteLine("                    4. Update the Expense (Both Expense and Amount");
                Console.WriteLine("                    5. Exist                                      ");

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

                //int userCode = 100;
                String name = "Empty";
                String password = "Empty";
                String email = "Empty";

                int totalAmount = 0;                     //Total Amount for the expense
                int UserCodeAuth = 0;
                //DateOnly date;
                switch (choice)
                {
                    case 1:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("                  Enter '1' to record an expense.                 ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("------------------------------------------------------------------");
                        Console.ResetColor();



                        Name:
                        try
                        {
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
                            catch(IndexOutOfRangeException e)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Name must be at least 3 characters long and it must not contins any Special Character");
                                Console.ResetColor();
                                goto Name;
                            }
                        }
                        catch (InvalidNameException e)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{e.Message}");
                            Console.ResetColor();
                            goto Name;
                        }


                        Password:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Enter the Password : ");                      //User Password
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        String pass = Console.ReadLine();
                        Console.ResetColor();
                        if (pass.Length >= 5)
                        {
                            password = pass;
                        }
                        else if (pass.Length == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please enter a Password ");
                            Console.ResetColor();
                            goto Password;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please enter a password of at least 5 characters.");
                            Console.ResetColor();
                            goto Password;
                        }

                        Email:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Enter the Email : ");                      //User Email
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        String UserEmail = Console.ReadLine();
                        Console.ResetColor();

                        if(VerifyEmailOnly(CodeEuserNamePass, UserEmail))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Already Email Exist");
                            Console.ResetColor();
                            goto Email;
                        }
                        if(UserEmail.Length == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please Enter the Email");
                            Console.ResetColor();
                            goto Email;
                        }
                        else if (!isValidEmail(UserEmail))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please Enter valid Email");
                            Console.ResetColor();
                            goto Email;
                        }
                        else
                        {
                            email = UserEmail;
                        }

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

                        //date = DateOnly.FromDateTime(DateTime.Now);

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
                        nameAndPass.UserEmail = email;
                        nameAndPass.Date = DateOnly.FromDateTime(DateTime.Now);

                        List<NameAndPass> namePassList = new List<NameAndPass>();
                        namePassList.Add(nameAndPass);

                        Hashtable userNamePass = new Hashtable();                                //for storing user name and pass and date and email

                        userNamePass[userCode] = namePassList;
                        CodeEuserNamePass.Add(userCode, userNamePass);

                        List<ExpenseAndPrice> expenseAndPriceList = new List<ExpenseAndPrice>();   //List to store the exoense and its amount in list
                        
                        for (int i = 0; i < numberOfRecords; i++)
                        {
                            ExpenseAndPrice NamePrice = new ExpenseAndPrice();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"Enter the name for Expense {i + 1} : ");
                            Console.ResetColor();
                            NamePrice.ExpenseName = Console.ReadLine();

                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"Enter the Amount for {NamePrice.ExpenseName} : ");
                            Console.ResetColor();
                            int total = int.Parse(Console.ReadLine());

                            NamePrice.ExpenseAmount = total;
                            totalAmount += total;
                            expenseAndPriceList.Add(NamePrice);         //Adding name and Price in list
                        }

                        Total.Add(userCode, totalAmount);        //Adding total to Hashtable

                        Hashtable EnameAndAmount = new Hashtable();                                     // Expense name and amount

                        EnameAndAmount[userCode] = expenseAndPriceList;           //addling list with code (key) in hashtable

                        Expense.Add(userCode, EnameAndAmount);                   //adding hashtables (code, price and ammount) in dictionary

                        



                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("                   Successfully Recorded :)                        ");
                        Console.ResetColor();

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
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
                        Console.Write("User Email : ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(email);
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
                        Console.WriteLine(DateOnly.FromDateTime(DateTime.Now));
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("------------------------------------------------------------------");
                        Console.WriteLine("|  \tExpense_Name\t\t\tAmount_Spent\t        |");
                        Console.WriteLine("------------------------------------------------------------------");
                        Console.ResetColor();


                        Hashtable ExpanseTable = Expense[userCode];
                        foreach (DictionaryEntry i in ExpanseTable)
                        {
                            List<ExpenseAndPrice> expenseList = i.Value as List<ExpenseAndPrice>;
                            foreach (ExpenseAndPrice k in expenseList)
                            {
                                Console.WriteLine($"   \t{k.ExpenseName}\t\t\t\t{k.ExpenseAmount}");
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
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("                     ~ * Thank You * ~                           ");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        totalAmount = 0;
                        //Console.WriteLine("Press any Key to Continue....");
                        //Console.ReadKey();
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

                    case 2:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("                 Enter '2' to View all the Expese                 ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("------------------------------------------------------------------");
                        Console.ResetColor();

                        
                        

                        UserCode:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Enter the User Code: ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            int userCodeAuth = int.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (CodeEuserNamePass.ContainsKey(userCodeAuth))
                            {
                                UserCodeAuth = userCodeAuth;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invail Code! Code Not Found");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("If you want to add the Expense? (Y) / If you want to Re-Enter the Code? (S)");
                                char ch = char.Parse(Console.ReadLine());
                                if(ch == 'y' || ch == 'Y')
                                {
                                    goto Name;
                                }
                                if(ch == 'S' || ch == 's')
                                {
                                    goto UserCode;
                                }
                            }

                        }
                        catch (FormatException e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid code! Please enter a valid Code in Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                            Console.ResetColor();
                            goto UserCode;
                        }

                        if (verifyNameEmail(CodeEuserNamePass,UserCodeAuth))
                        {
                        UserPassword:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Enter the Password: ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            String userPassAuth = Console.ReadLine();
                            Console.ResetColor();

                            Hashtable passData = CodeEuserNamePass[UserCodeAuth];
                            if (passData.ContainsKey(UserCodeAuth))
                            {
                                List<NameAndPass> PassList = (List<NameAndPass>)passData[UserCodeAuth];

                                string storedPass = PassList[0].UserPassword;

                                if (storedPass == userPassAuth)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine();
                                    Console.WriteLine("-------------------------Your Expense---------------------------");
                                    Console.ResetColor();
                                    Console.WriteLine();

                                    displayExpensePrice(CodeEuserNamePass, UserCodeAuth,Expense);


                                    //Console.ResetColor();
                                    //Console.ForegroundColor = ConsoleColor.DarkGray;
                                    //Console.Write("User Name : ");
                                    //Console.ResetColor();
                                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                                    //Hashtable userName = CodeEuserNamePass[UserCodeAuth];
                                    //List<NameAndPass> nameList = (List<NameAndPass>)userName[UserCodeAuth];
                                    //Console.WriteLine(nameList[0].UserName);
                                    //Console.ResetColor();

                                    //Console.ForegroundColor = ConsoleColor.DarkGray;
                                    //Console.Write("User Code : ");
                                    //Console.ResetColor();
                                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                                    //Console.WriteLine(UserCodeAuth);
                                    //Console.ResetColor();

                                    //Console.ForegroundColor = ConsoleColor.DarkGray;
                                    //Console.Write("Date : ");
                                    //Console.ResetColor();
                                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                                    //Hashtable dateData = CodeEuserNamePass[UserCodeAuth];
                                    //List<NameAndPass> dateList = (List<NameAndPass>)dateData[UserCodeAuth];
                                    //Console.WriteLine(dateList[0].Date);
                                    //Console.ResetColor();

                                    //Console.ForegroundColor = ConsoleColor.Cyan;
                                    //Console.WriteLine("------------------------------------------------------------------");
                                    //Console.WriteLine("|  \tExpense_Name\t\t\tAmount_Spent\t        |");
                                    //Console.WriteLine("------------------------------------------------------------------");
                                    //Console.ResetColor();

                                    //Hashtable userExpanseTable = Expense[UserCodeAuth];
                                    //foreach (DictionaryEntry i in userExpanseTable)
                                    //{
                                    //    List<ExpenseAndPrice> expenseList = i.Value as List<ExpenseAndPrice>;
                                    //    foreach (ExpenseAndPrice k in expenseList)
                                    //    {
                                    //        Console.WriteLine($"   \t{k.ExpenseName}\t\t\t\t{k.ExpenseAmount}");
                                    //    }
                                    //}




                                    //foreach (KeyValuePair<int, Hashtable> i in Expense)    //For Dictionary
                                    //{
                                    //    if(i.)
                                    //    foreach (DictionaryEntry e in i.Value)             //For HashTable
                                    //    {
                                    //        List<ExpenseAndPrice> expenseList = new List<ExpenseAndPrice>();

                                    //        foreach (ExpenseAndPrice k in expenseAndPriceList)
                                    //        {
                                    //            Console.WriteLine($"   \t{k.ExpenseName}\t\t\t\t{k.ExpenseAmount}");
                                    //        }
                                    //    }
                                    //}

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("------------------------------------------------------------------");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write($"                       Total Amount : ");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(Total[userCode]);
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("------------------------------------------------------------------");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("                     ~ * Thank You * ~                           ");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.WriteLine();

                                    //Console.WriteLine("Press any Key to Continue....");
                                    //Console.ReadKey();
                                }

                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Incorrect Password :(");
                                    Console.ResetColor();
                                    goto UserPassword;
                                }
                            }
                        }

                        UserCodeAuth = 0;

                    //UserName:
                    //    try
                    //    {

                    //        Console.ForegroundColor = ConsoleColor.DarkGray;
                    //        Console.Write("Enter the User Name: ");
                    //        Console.ResetColor();
                    //        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    //        String userNameAuth = Console.ReadLine();
                    //        Console.ResetColor();
                    //        if (userNameAuth.Length == 0)
                    //        {
                    //            Console.ForegroundColor = ConsoleColor.Red;
                    //            Console.WriteLine("Please Enter the Name");
                    //            Console.ResetColor();
                    //            goto UserName;
                    //        }
                    //        isValidString(userNameAuth);
                    //        Hashtable userName = CodeEuserNamePass[UserCodeAuth];
                    //        List<NameAndPass> NameList = (List<NameAndPass>)userName[UserCodeAuth];
                    //        String storingName = NameList[0].UserName;
                    //        if (storingName == userNameAuth)
                    //        {
                    //            UserNameAuth = userNameAuth;
                    //        }
                    //        else
                    //        {
                    //            Console.ForegroundColor = ConsoleColor.Red;
                    //            Console.WriteLine("The name is not valid for this code");
                    //            Console.ResetColor();
                    //            goto UserName;
                    //        }

                    //    UserEmail:
                    //        Console.ForegroundColor = ConsoleColor.DarkGray;
                    //        Console.Write("Enter the User Email: ");
                    //        Console.ResetColor();
                    //        Console.ForegroundColor = ConsoleColor.Red;
                    //        String userEmailAuth = Console.ReadLine();
                    //        Console.ResetColor();

                    //        if (UserEmailAuth.Length == 0)
                    //        {
                    //            Console.ForegroundColor = ConsoleColor.Yellow;
                    //            Console.WriteLine("Please Enter the Email");
                    //            Console.ResetColor();
                    //            goto UserEmail;
                    //        }
                    //        else if (!isValidEmail(userEmailAuth))
                    //        {
                    //            Console.ForegroundColor = ConsoleColor.Red;
                    //            Console.WriteLine("Please Enter valid Email");
                    //            Console.ResetColor();
                    //            goto UserEmail;
                    //        }
                    //        else
                    //        {
                    //            Hashtable userEmail = CodeEuserNamePass[UserCodeAuth];
                    //            List<NameAndPass> EmailList = (List<NameAndPass>)userEmail[UserCodeAuth];
                    //            String StoredUser = EmailList[0].UserEmail;
                    //            if (StoredUser == userEmailAuth)
                    //            {
                    //                UserEmailAuth = userEmailAuth;
                    //            }
                    //            else
                    //            {
                    //                Console.ForegroundColor = ConsoleColor.Red;
                    //                Console.WriteLine("User Email not Match for this Code");
                    //                goto UserEmail;
                    //            }
                    //        }
                    //    }
                    //    catch (InvalidNameException e)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Red;
                    //        Console.WriteLine(e.Message);
                    //        Console.ResetColor();
                    //        goto UserName;
                    //    }

                        
                        break;

                    case 3:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("             Enter '3' to View all the Expese Summary             ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("-------------------------Expense Summary--------------------------");
                        Console.ResetColor();
                        Console.WriteLine();

                        userCode:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Enter the User Code: ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            int userCodeAuth = int.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (CodeEuserNamePass.ContainsKey(userCodeAuth))
                            {
                                UserCodeAuth = userCodeAuth;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invail Code! Code Not Found");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("If you want to add the Expense? (Y) / If you want to Re-Enter the Code? (S)");
                                char ch = char.Parse(Console.ReadLine());
                                if (ch == 'y' || ch == 'Y')
                                {
                                    goto Name;
                                }
                                if (ch == 'S' || ch == 's')
                                {
                                    goto userCode;
                                }
                            }

                        }
                        catch (FormatException e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid code! Please enter a valid Code in Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                            Console.ResetColor();
                            goto userCode;
                        }

                        if (verifyNameEmail(CodeEuserNamePass, UserCodeAuth))
                        {
                        UserPassword:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Enter the Password: ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            String userPassAuth = Console.ReadLine();
                            Console.ResetColor();

                            Hashtable passData = CodeEuserNamePass[UserCodeAuth];
                            if (passData.ContainsKey(UserCodeAuth))
                            {
                                List<NameAndPass> PassList = (List<NameAndPass>)passData[UserCodeAuth];

                                string storedPass = PassList[0].UserPassword;

                                if (storedPass == userPassAuth)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine();
                                    Console.WriteLine("-------------------------Your Expense---------------------------");
                                    Console.ResetColor();
                                    Console.WriteLine();

                                    displayExpensePrice(CodeEuserNamePass, UserCodeAuth, Expense);

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("------------------------------------------------------------------");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write($"Total Amount         : ");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(Total[UserCodeAuth]);
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write($"Average Expense      : ");
                                    Console.ResetColor();
                                    Hashtable ExpensePrice = Expense[UserCodeAuth];
                                    List<ExpenseAndPrice> expenseList = (List<ExpenseAndPrice>)ExpensePrice[UserCodeAuth];
                                    int lengthOfExpense = expenseList.Count;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    int total =(int) Total[UserCodeAuth];
                                    Console.WriteLine(total/lengthOfExpense);
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write($"Highest Expense      : ");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(expenseList.Max(e => e.ExpenseAmount));
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write($"\t===>\t");
                                    Console.ResetColor();
                                    string expenseName1 = "Empty";
                                    for (int i = 0; i < expenseList.Count; i++)
                                    {
                                        if (expenseList[i].ExpenseAmount == expenseList.Max(e =>e.ExpenseAmount))
                                        {
                                            expenseName1 = expenseList[i].ExpenseName;
                                            break;
                                        }
                                    }
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write($"Highest Expense Name  : ");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(expenseName1);
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write($"Lowest Expense       : ");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(expenseList.Min(e => e.ExpenseAmount));
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write($"\t===>\t");
                                    Console.ResetColor();
                                    string expenseName2 = "Empty";
                                    for (int i = 0; i < expenseList.Count; i++)
                                    {
                                        if (expenseList[i].ExpenseAmount == expenseList.Min(e => e.ExpenseAmount))
                                        {
                                            expenseName2 = expenseList[i].ExpenseName;
                                            break;
                                        }
                                    }
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(expenseName2);
                                    Console.ResetColor();
                                    Console.Write($"Lowest Expense Name   : ");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("------------------------------------------------------------------");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                }

                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Incorrect Password :(");
                                    Console.ResetColor();
                                    goto UserPassword;
                                }

                            }
                        }
                        UserCodeAuth = 0;
                        break;

                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("              You Choose to Update The Expense :)            ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();

                        Code:
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Enter the User Code: ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            int userCodeAuth = int.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (CodeEuserNamePass.ContainsKey(userCodeAuth))
                            {
                                UserCodeAuth = userCodeAuth;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invail Code! Code Not Found");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("If you want to add the Expense? (Y) / If you want to Re-Enter the Code? (S)");
                                char ch = char.Parse(Console.ReadLine());
                                if (ch == 'y' || ch == 'Y')
                                {
                                    goto Name;
                                }
                                if (ch == 'S' || ch == 's')
                                {
                                    goto Code;
                                }
                            }

                        }
                        catch (FormatException e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid code! Please enter a valid Code in Number digits only. \nAlphabets or symbols or Whitespace are not allowed.");
                            Console.ResetColor();
                            goto Code;
                        }

                        if (verifyNameEmail(CodeEuserNamePass, UserCodeAuth))
                        {
                            UserPassword:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Enter the Password: ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            String userPassAuth = Console.ReadLine();
                            Console.ResetColor();

                            Hashtable passData = CodeEuserNamePass[UserCodeAuth];
                            if (passData.ContainsKey(UserCodeAuth))
                            {
                                List<NameAndPass> PassList = (List<NameAndPass>)passData[UserCodeAuth];

                                string storedPass = PassList[0].UserPassword;

                                if (storedPass == userPassAuth)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine();
                                    Console.WriteLine("---------------------------Your Expense---------------------------");
                                    Console.ResetColor();
                                    Console.WriteLine();

                                    displayExpensePrice(CodeEuserNamePass, UserCodeAuth, Expense);

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("------------------------------------------------------------------");
                                    Console.ResetColor();
                                    Update:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("Enter the S.No to Edit the Expense Record");
                                    Console.ResetColor();
                                    int sno = int.Parse(Console.ReadLine());

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Press 'e' button to update the Expanse Name (e)");
                                    Console.WriteLine("Press 'v' button to update the Expanse Amount (v)");
                                    Console.ResetColor();
                                    char ch = char.Parse(Console.ReadLine());

                                    int snoCheck = 1;
                                    if(ch == 'e')
                                    {
                                        
                                        string updatedExpense = Console.ReadLine();

                                        Hashtable updateExpanseName = Expense[UserCodeAuth];
                                        foreach (DictionaryEntry i in updateExpanseName)
                                        {
                                            List<ExpenseAndPrice> expenseList = i.Value as List<ExpenseAndPrice>;
                                            for(int j = 0; j<expenseList.Count; j++)
                                            {
                                                if(snoCheck == sno)
                                                {
                                                    expenseList[j].ExpenseName = updatedExpense;
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine("Expense Name is Successfully Updated");
                                                    Console.ResetColor();
                                                    break;
                                                }
                                                snoCheck++;
                                            }
                                        }
                                    }
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("If you Want to Do update (Y/N): ");
                                    Console.ResetColor();
                                    char check = char.Parse(Console.ReadLine());
                                    if(check == 'Y' || check == 'y')
                                    {
                                        goto Update;
                                    }
                                    
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("------------------------------------------------------------------");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                }

                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Incorrect Password :(");
                                    Console.ResetColor();
                                    goto UserPassword;
                                }

                            }
                        }

                        break;



                    case 5:
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

                        for (int i = 5; i > 0; i--)
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

                if(choice == 5)
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
