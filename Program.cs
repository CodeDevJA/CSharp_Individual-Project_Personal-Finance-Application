using System;                       //Ger tillgång till grundläggande funktioner som Console och DateTime, mm.
using System.Collections.Generic;   //Ger tillgång till List och andra samlingsklasser. 
using System.IO;                    //Ger tillgång till möjligheten att läsa från fil och skriva till fil.
using System.Linq;                  //Ger tillgång till att kunna filtrera och söka i listor lättare, mm. 

namespace individuell_uppg;

class Program
{
    static void Main(string[] args)
    {
        //skapar ett object av FinanceManager-Classen för att hantera alla transaktioner. 
        FinanceManager financeManager = new FinanceManager();

        //skapar ett object av FileManager-Classen för att all data ska kunna skrivas till och läsas från fil. 
        FileManager fileManager = new FileManager();

        //börjar programmet med att ladda in (läsa in) data från filen (om det finns någon data i den). 
        financeManager.transactionList = fileManager.LoadFromFileMethod();

        //lägger till en transaktion för inkomster här
        financeManager.AddTransactionMethod(new IncomeTransaction("CSN-bidrag", 3000, DateTime.Now));

        //lägger till en transaktion för kostnader här
        financeManager.AddTransactionMethod(new ExpenseTransaction("Kollektivtrafik kort", 1800, DateTime.Now));

        //sista steg innan programmet avslutas, skriver (all data) till filen (senaste gällande datan). 
        fileManager.SaveToFileMethod(financeManager.transactionList);

        //skriver ut nuvarande saldo på bankkontot till användaren 
        System.Console.WriteLine($"Ditt nuvarande saldo på bankkontot är: {financeManager.GetCurrentBalanceMethod()} kr. "); 

        //Avslutar programmet 
    }
}

