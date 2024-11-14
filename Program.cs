using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_Individual_Project_Personal_Finance_Application_main;

class Program
{
  static void Main(string[] args)
  {
    // Create instances of necessary classes
    TransactionManager transactionManager = new TransactionManager();
    FileManager fileManager = new FileManager();
    Menu menu = new Menu(transactionManager, fileManager);

    // Load transactions from file
    transactionManager.LoadTransactions();

    // Display the menu
    menu.DisplayMenu();
  }
}
