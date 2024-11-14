using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace CSharp_Individual_Project_Personal_Finance_Application_main;

public class TransactionManager
{
  public List<Transaction> transactionsList { get; set; }

  public TransactionManager()
  {
    transactionsList = new List<Transaction>();
  }

  // Calculate the balance (income - expense)
  public decimal GetBalance()
  {
    var income = transactionsList.Where(t => t.Type == TransactionType.INCOME).Sum(t => t.Amount);
    var expenses = transactionsList.Where(t => t.Type == TransactionType.EXPENSE).Sum(t => t.Amount);
    return income - expenses;
  }

  // View transactions by YEAR
  public List<Transaction> ViewTransactionsByYear(int specificYear)
  {
    List<Transaction> TransactionsByYearList = transactionsList.Where(t => t.Date.Year == specificYear).ToList();
    return TransactionsByYearList;
  }

  // View transactions by MONTH
  public List<Transaction> ViewTransactionsByMonth(int specificMonth)
  {
    List<Transaction> TransactionsByMonthList = transactionsList.Where(t => t.Date.Month == specificMonth).ToList();
    return TransactionsByMonthList;
  }

  // View transactions by WEEK
  public List<Transaction> ViewTransactionsByWeek(int specificYear, int specificWeek)
  {
    List<Transaction> TransactionsByWeekList =
    transactionsList.Where(t => t.Date.Year == specificYear &&
    System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear
    (t.Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek,
    DayOfWeek.Monday) == specificWeek).ToList();
    return TransactionsByWeekList;
  }

  // View transactions by DAY
  public List<Transaction> ViewTransactionsByDay(int specificDay)
  {
    List<Transaction> TransactionsByDayList = transactionsList.Where(t => t.Date.Day == specificDay).ToList();
    return TransactionsByDayList;
  }

  // View transactions by StartDate and EndDate  
    public List<Transaction> ViewTransactionsByRangeOfDate(DateTime startDate, DateTime endDate)
    {
        List<Transaction> filteredTransactionsByRangeOfDateList = transactionsList
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .ToList();
        return filteredTransactionsByRangeOfDateList;
    }

  // Add a transaction
  public void AddTransaction(Transaction transaction)
  {
    transactionsList.Add(transaction);
  }

  // Remove a transaction (by name)
  public void RemoveTransaction(string name)
  {
    var transactionToRemove = transactionsList.FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    if (transactionToRemove != null)
    {
      transactionsList.Remove(transactionToRemove);
      Console.WriteLine("Transaction removed successfully!");
    }
    else
    {
      Console.WriteLine("Transaction not found.");
    }
  }

  // Exit application (executed from the Menu.cs)
  //

  // Save transactions to file (JSON format). Write transactions to file. 
  public void SaveTransactions()
  {
    string json = JsonConvert.SerializeObject(transactionsList, Formatting.Indented);
    File.WriteAllText("transactions.json", json);
  }

  // Load transactions from file (JSON format). Read transactions from file. 
  public void LoadTransactions()
  {
    if (File.Exists("transactions.json"))
    {
      string json = File.ReadAllText("transactions.json");
      transactionsList = JsonConvert.DeserializeObject<List<Transaction>>(json) ?? new List<Transaction>();
    }
  }
}
