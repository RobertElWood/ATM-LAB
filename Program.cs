namespace ATM_Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Bool acts as trigger for loop, currentAccount controls account currently logged in.
            bool goOn = true;
            Account currentAccount = null;
            
            List <Account> accountList = new List <Account> ();
            accountList.Add(new Account("Jimmy", "password", 1337));
            accountList.Add(new Account("Hannah Banana", "123456", 50000));
            accountList.Add(new Account("Justin", "qwerty", 758659));
            accountList.Add(new Account("Sarah", "ILoveYou", 67559));
            accountList.Add(new Account("Hannibal", "qwertyuiop", 1000000));
            accountList.Add(new Account("Phillip", "1q2w3e4r5t", 599));
            accountList.Add(new Account("Maria", "letmein", 807359));
            accountList.Add(new Account("Abe", "heyhey13", 1000));
            accountList.Add(new Account("Curtis", "adobe123", 11111111));

            Console.WriteLine("\nGood Afternoon! Welcome to the Grand Circus Interactive ATM!\n");
            Console.WriteLine("This innovative ATM service can act both as a standard ATM and help you create a new account, if necessary!\n");

            while (goOn)
            {
                //Displays current user's name, if logged in.
                if (currentAccount != null)
                {
                    Console.WriteLine("Currently logged in as: " + currentAccount.Name + "\n");
                }

                //Displays a list of options to user, much like the buttons on a standard ATM.
                Console.WriteLine("Please select the operation you'd like to perform by typing the appropriate number:\n");

                Console.WriteLine("1-\tRegister a new account");
                Console.WriteLine("2-\tLogin to an account");
                Console.WriteLine("3-\tCheck an account balance");
                Console.WriteLine("4-\tMake a deposit");
                Console.WriteLine("5-\tMake a withdrawal");
                Console.WriteLine("6-\tLogout of an account");
                Console.WriteLine("7-\tExit and remove card");
                Console.WriteLine();

                int input = -1;

                try
                {
                    input = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    if (input <= 0 || input > 7) 
                    {
                    Console.WriteLine("\nI'm sorry, I didn't understand that. Please enter a number 1-7.\n");
                    continue;
                    }

                }
                catch (FormatException e)
                {
                    Console.WriteLine("\nI'm sorry, it doesn't seem like you entered a number. Please try again.\n");
                    continue;
                }

                //Registers the user's new account using the ATM class's method, CreateAccount.
                if (input == 1)
                {
                    Console.WriteLine("Wonderful! We're pleased that you'd like to start an account with us.\n");
                    Console.WriteLine("Please enter a username for your new account:\n");

                    string username = Console.ReadLine();
                    Console.WriteLine();

                    Console.WriteLine("Please enter a password for your new account:\n");

                    string password = Console.ReadLine();
                    Console.WriteLine();

                    Account newAccount = ATM.CreateAccount(username, password);
                    accountList.Add(newAccount);

                    Console.WriteLine($"\nCongratulations! Your new account has been created with the username: {accountList[accountList.Count - 1].Name}\n");
                    Console.WriteLine($"Your current balance is: {accountList[accountList.Count - 1].Balance}\n\n");
                    continue;
                }

                //Allows the user to log in if their username or password matches one currently on file.
                //Uses the "Login" method of the ATM class.
                else if (input == 2) 
                {
                    Console.WriteLine("Welcome back! Please enter your username:\n");
                    string enteredName = Console.ReadLine();
                    Console.WriteLine();

                    Console.WriteLine("Please enter your password:\n");
                    string enteredPass = Console.ReadLine();
                    Console.WriteLine();

                    try
                    {
                        currentAccount = ATM.Login(enteredName, enteredPass, accountList, currentAccount);

                        Console.WriteLine($"\nSuccess! You are now logged in, {currentAccount.Name}.\n\n");

                    } 
                    catch (ArgumentOutOfRangeException) 
                    {
                        Console.WriteLine("\nI'm sorry. We don't have that account on file. Please try again.\n");
                    }

                } 

                //Displays the user's current balance, if logged in.
                else if (input == 3) 
                {
                    ATM.CheckBalance(currentAccount);
                }

                //Allows the user to deposit an amount into their account using the method "Deposit"
                //Contains error handling for non-positive integers as well as invalid types such as strings.
                else if (input == 4) 
                {
                    int depositAmount = -1;

                    if (currentAccount != null)
                    {
                        Console.WriteLine("\nPlease enter the dollar amount that you'd like to deposit in your account: \n");

                        try
                        {
                            depositAmount = int.Parse(Console.ReadLine());
                            Console.WriteLine();

                            if (depositAmount < 0) 
                            {
                                Console.WriteLine("I'm sorry. You haven't entered a valid number. Please try again.");
                                continue;
                            }
                            
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("I'm sorry, it doesn't seem like you entered a number. Please try again.\n");
                        }

                        currentAccount = ATM.Deposit(depositAmount, currentAccount);

                        Console.WriteLine($"\nThank you for your deposit. Your updated balance is: ${currentAccount.Balance}\n\n");
                    } 
                    else 
                    {
                        Console.WriteLine("\nI'm sorry. It appears that you aren't logged in.\n");
                        Console.WriteLine("Please log in first, or create an account.\n\n");
                        continue;
                    }
                }

                //Allows the user to take money out of their account using the method "Withdraw"
                //Contains error handling for non-positive integers as well as invalid types such as strings.

                else if (input == 5) 
                {
                    int withdrawalAmt = -1;

                    if (currentAccount != null)
                    {
                        Console.WriteLine("\nPlease enter the dollar amount that you'd like to withdraw from your account: \n");

                        try
                        {
                            withdrawalAmt = int.Parse(Console.ReadLine());
                            Console.WriteLine();

                            if (withdrawalAmt < 0) 
                            {
                                Console.WriteLine("I'm sorry. You haven't entered a valid number. Please try again.");
                                continue;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("I'm sorry, it doesn't seem like you entered a number. Please try again.\n");
                        }

                        currentAccount = ATM.Withdraw(withdrawalAmt, currentAccount);

                        Console.WriteLine($"\nThank you! Your updated balance is: ${currentAccount.Balance}\n\n");
                    }
                } 

                //Logs the user out by setting the current account to null.
                else if (input == 6) 
                {
                    if (currentAccount != null)
                    {
                        currentAccount = ATM.Logout(currentAccount);
                        Console.WriteLine("\nYou have successfully logged out of your account.\n");
                    } 
                    else 
                    {
                        currentAccount = ATM.Logout(currentAccount);
                    }
                }
                else 
                {
                    goOn = runAgain();
                }
            }

        }

        //If the user selects the option to exit the application, the following method will be called.
        //This allows the user to exit the program, which will constantly loop and display the action menu otherwise.
        public static bool runAgain()
        {
            Console.WriteLine();
            Console.WriteLine("\nAre you sure that you'd like to exit the ATM Application? Y/N?\n");
            Console.WriteLine("Type 'Y' to exit. Type 'N' to continue.\n");
            string input = Console.ReadLine().ToLower();

            if (input == "y")
            {
                Console.WriteLine("\nThank you for banking with us here at Grand Circus!");
                return false;
            }
            else if (input == "n")
            {
                return true;
            }
            else
            {
                Console.WriteLine("I didn't understand that. Please try again!");
                return runAgain();
            }
        }
    }
}