using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace CSharp_Individual_Project_Personal_Finance_Application_main;

public class Menu
{
  private readonly TransactionManager _transactionManager;
  private readonly FileManager _fileManager;

  public Menu(TransactionManager transactionManager, FileManager fileManager)
  {
    _transactionManager = transactionManager;
    _fileManager = fileManager;
  }

  public void DisplayMenu()
  {
    while (true)
    {
      Console.Clear();

      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("Personal Finance Application");
      Console.ResetColor();
      Console.WriteLine(" ");

      Console.WriteLine("Select an option (by writing the number):");
      Console.WriteLine(" ");

      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("1. View Balance");
      Console.ResetColor();
      Console.WriteLine(" ");

      Console.WriteLine("2. View transactions by YEAR");
      Console.WriteLine("3. View transactions by MONTH");
      Console.WriteLine("4. View transactions by WEEK");
      Console.WriteLine("5. View transactions by DAY");
      Console.WriteLine(" ");

      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("6. View transactions by RANGE OF DATE");
      Console.ResetColor();
      Console.WriteLine(" ");

      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("7. Add Transaction");
      Console.ResetColor();
      Console.WriteLine(" ");

      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("8. Remove Transaction");
      Console.ResetColor();
      Console.WriteLine(" ");

      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("9. Exit");
      Console.ResetColor();
      Console.WriteLine(" ");

      Console.Write("Enter your choice by typing a number: ");
      Console.WriteLine(" ");

      if (Enum.TryParse<MenuOptions>(Console.ReadLine()!, out var choice))
      {
        switch (choice)
        {
          case MenuOptions.ViewBalance:
            ViewBalance();
            break;
          case MenuOptions.ViewTransactionsByYear:
            ViewTransactionsByYear();
            break;
          case MenuOptions.ViewTransactionsByMonth:
            ViewTransactionsByMonth();
            break;
          case MenuOptions.ViewTransactionsByWeek:
            ViewTransactionsByWeek();
            break;
          case MenuOptions.ViewTransactionsByDay:
            ViewTransactionsByDay();
            break;
          case MenuOptions.ViewTransactionsByRangeOfDate:
            ViewTransactionsByRangeOfDate();
            break;
          case MenuOptions.AddTransaction:
            AddTransaction();
            _transactionManager.SaveTransactions();
            break;
          case MenuOptions.RemoveTransaction:
            RemoveTransaction();
            _transactionManager.SaveTransactions();
            break;
          case MenuOptions.Exit:
            _transactionManager.SaveTransactions();
            Environment.Exit(0);
            break;
        }
      }
    }
  }

  private void ViewBalance()
  {
    decimal balance = _transactionManager.GetBalance();
    Console.ForegroundColor = balance >= 0 ? ConsoleColor.Yellow : ConsoleColor.Red;
    Console.WriteLine($"Current balance: {balance:C}");
    Console.ResetColor();
    Console.ReadKey();
  }

  // View transactions by YEAR
  private void ViewTransactionsByYear()
  {
    Console.WriteLine("Write the YEAR number (yyyy): ");
    int specificYear = int.Parse(Console.ReadLine()!);

    List<Transaction> TransactionsByYearList = _transactionManager.ViewTransactionsByYear(specificYear);

    Console.WriteLine($"Transactions By Year {specificYear}: ");
    foreach (Transaction transaction in TransactionsByYearList)
    {
      Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Type}: {transaction.Name}, {transaction.Description}, {transaction.Amount:C}");
    }

    Console.ReadKey();
  }

  // View transactions by MONTH
  private void ViewTransactionsByMonth()
  {
    Console.WriteLine("Write the MONTH number (MM): ");
    int specificMonth = int.Parse(Console.ReadLine()!);

    List<Transaction> TransactionsByMonthList = _transactionManager.ViewTransactionsByMonth(specificMonth);

    Console.WriteLine($"Transactions By Month {specificMonth}: ");
    foreach (Transaction transaction in TransactionsByMonthList)
    {
      Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Type}: {transaction.Name}, {transaction.Description}, {transaction.Amount:C}");
    }

    Console.ReadKey();
  }

  // View transactions by WEEK
  private void ViewTransactionsByWeek()
  {
    Console.WriteLine("Write the YEAR number (yyyy): ");
    int specificYear = int.Parse(Console.ReadLine()!);

    Console.WriteLine("Write the WEEK number (ww): ");
    int specificWeek = int.Parse(Console.ReadLine()!);

    List<Transaction> TransactionsByWeekList = _transactionManager.ViewTransactionsByWeek(specificYear, specificWeek);

    Console.WriteLine($"Transactions By Week {specificWeek} and YEAR {specificYear}: ");
    foreach (Transaction transaction in TransactionsByWeekList)
    {
      Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Type}: {transaction.Name}, {transaction.Description}, {transaction.Amount:C}");
    }

    Console.ReadKey();
  }

  // View transactions by DAY
  private void ViewTransactionsByDay()
  {
    Console.WriteLine("Write the DAY number (dd): ");
    int specificDay = int.Parse(Console.ReadLine()!);

    List<Transaction> TransactionsByDayList = _transactionManager.ViewTransactionsByDay(specificDay);

    Console.WriteLine($"Transactions By Day {specificDay}: ");
    foreach (Transaction transaction in TransactionsByDayList)
    {
      Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Type}: {transaction.Name}, {transaction.Description}, {transaction.Amount:C}");
    }

    Console.ReadKey();
  }

  // View transactions by StartDate and EndDate  
  private void ViewTransactionsByRangeOfDate()
  {
    Console.Write("Enter the start date (YYYY-MM-DD): ");
    DateTime startDate = DateTime.Parse(Console.ReadLine()!);

    Console.Write("Enter the end date (YYYY-MM-DD): ");
    DateTime endDate = DateTime.Parse(Console.ReadLine()!);

    List<Transaction> filteredTransactionsByRangeOfDateList = _transactionManager.ViewTransactionsByRangeOfDate(startDate, endDate);

    if (filteredTransactionsByRangeOfDateList.Any())
    {
      Console.WriteLine($"Transactions from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}: ");
      foreach (Transaction transaction in filteredTransactionsByRangeOfDateList)
      {
        Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Type}: {transaction.Name}, {transaction.Description}, {transaction.Amount:C} ");
      }
    }
    else
    {
      Console.WriteLine("No transactions found in the given date range.");
    }

    Console.ReadKey();
  }

  private void AddTransaction()
  {
    Console.WriteLine("Enter transaction type: ");
    Console.WriteLine($"1. {TransactionType.INCOME} ");
    Console.WriteLine($"2. {TransactionType.EXPENSE} ");
    string typeInput = Console.ReadLine()!;
    var type = typeInput.ToLower() == "1" ? TransactionType.INCOME : TransactionType.EXPENSE;

    Console.Write("Enter the name: ");
    string name = Console.ReadLine()!;

    Console.Write("Enter the description: ");
    string description = Console.ReadLine()!;

    Console.Write("Enter the amount: ");
    decimal amount = Convert.ToDecimal(Console.ReadLine()!);

    Console.Write("Enter the date (YYYY-MM-DD): ");
    DateTime date = DateTime.Parse(Console.ReadLine()!);

    Transaction transaction = new Transaction
    {
      Type = type,
      Amount = amount,
      Name = name,
      Description = description,
      Date = date
    };

    _transactionManager.AddTransaction(transaction);
  }

  // Remove a transaction by name
  private void RemoveTransaction()
  {
    Console.WriteLine("Enter the transaction name to remove: ");
    string name = Console.ReadLine()!;
    _transactionManager.RemoveTransaction(name);
  }
}

public enum MenuOptions
{
  ViewBalance = 1,
  ViewTransactionsByYear = 2,
  ViewTransactionsByMonth = 3,
  ViewTransactionsByWeek = 4,
  ViewTransactionsByDay = 5,
  ViewTransactionsByRangeOfDate = 6,
  AddTransaction = 7,
  RemoveTransaction = 8,
  Exit = 9
}
