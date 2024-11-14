using System;

namespace CSharp_Individual_Project_Personal_Finance_Application_main;

public class Transaction
{
  public TransactionType Type { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public decimal Amount { get; set; }
  public DateTime Date { get; set; }
}

public enum TransactionType
{
  INCOME = 1,   // Green foreground
  EXPENSE = 2   // Red foreground
}

public enum TransactionPeriod
{
  YEAR = 1,     // Filter transactions by year
  MONTH,    // Filter transactions by month
  WEEK,     // Filter transactions by week
  DAY,      // Filter transactions by day
}
