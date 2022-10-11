using System;

public class ATM
{
	public static Account CreateAccount (string username, string password) 
	{
		Account newAccount = new Account(username, password, 0);
		return newAccount;
	}

	public static Account Login (string username, string password, List <Account> accountList, Account currentAccount) 
	{
		Account  accountInfo = new Account( "", "", -1 );

		if (currentAccount != null)
		{
			Console.WriteLine("\nI'm sorry. It appears that there is already an account logged in.\n");
			Console.WriteLine("Please log out if you'd like to view a different account.\n");
				
		}
		else
		{

			IEnumerable <Account> foundAccount = accountList.Where(acc => acc.Name == username && acc.Password == password);
			accountInfo = foundAccount.ElementAt(0);
		}

        return accountInfo;
    }

	public static Account Logout (Account currentAccount) 
	{
		if (currentAccount == null)
		{
			Console.WriteLine("\nI'm sorry. It appears that you aren't logged in.\n");
			Console.WriteLine("Please log in first, or create an account.\n\n");
			return currentAccount;
		}
		else
		{
			currentAccount = null;
			return currentAccount;
		}
	}

	public static void CheckBalance (Account currentAccount) 
	{
		if (currentAccount == null)
		{
			Console.WriteLine("\nI'm sorry. It appears that you aren't logged in.\n");
			Console.WriteLine("Please log in first, or create an account.\n\n");
		}
		else
		{
			Console.WriteLine($"\nYour current balance is: ${currentAccount.Balance}\n\n");
		}
	}

	public static Account Deposit (int depositAmount, Account currentAccount)
	{
		if (currentAccount == null)
		{
			Console.WriteLine("\nI'm sorry. It appears that you aren't logged in.\n");
			Console.WriteLine("Please log in first, or create an account.\n\n");
		}
		else
		{
			currentAccount.Balance = currentAccount.Balance + depositAmount;
		}

		return currentAccount;

	}

    public static Account Withdraw (int withdrawAmount, Account currentAccount)
    {
        if (currentAccount == null)
        {
            Console.WriteLine("\nI'm sorry. It appears that you aren't logged in.\n");
            Console.WriteLine("Please log in first, or create an account.\n\n");
        }
        else
        {
			if (currentAccount.Balance - withdrawAmount < 0)
			{
				Console.WriteLine("\nI'm sorry. It appears you've tried to withdraw too much. Please try again.");
				return currentAccount;
			}
			else
			{
				currentAccount.Balance = currentAccount.Balance - withdrawAmount;
			}

        }

        return currentAccount;

    }
}
