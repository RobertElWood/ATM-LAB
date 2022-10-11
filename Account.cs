using System;

public class Account
{

	public string Name { get; set; }

	public string Password { get; set; }

	public int Balance { get; set; }

	public Account (string name, string password, int balance)
	{
		Name = name;
		Password = password;
		Balance = balance;
	}
}
