using System.Globalization; 
// CultureInfo.CurrentCulture används för att få den aktuella kulturen (t.ex. sv-SE)
// som styr formatering av datum, siffror, etc. baserat på systemets regionala inställningar. 

//FinanceManager är en klass som hanterar alla transaktioner. Den kan lägga till och ta bort transaktioner samt beräkna aktuellt saldo på kontot.
//Denna klass kommer att kunna generera/skapa ett object, som i sin tur kommer att innehålla en lista där alla transaktioner kommer att lagras.
//genom Listan (i objectet, som genereras av denna Classen) kommer denna Class sedan att kunna behandla nyinkommen-input, men även befintlig data i Listan för att lägga till-, ta bort-, transaktioner och beräkna totalt saldo, med hjälp av inbyggda Metoder för respektive aktivitet. 
public class FinanceManager 
{
  //skapar en shorthand Property (med abstraherad kod) som lägger till transaktioner i Listan för inkomster och kostnader. Lista som håller alla transaktioner (både inkomster och kostnader). 
  public List<TransactionBase> transactionList { get; set; }

  //Constructor (utan deklarerade parametrar) - Constructorn tar inga argument för att skapa en tom Lista att starta med, som ska hålla koll på alla inlagda transaktioner senare. 
  //Listan sparas sedan i FinanceManager-objectet, som genereras som följd (som heter transactionsList). 
  public FinanceManager() 
  {
    //en ny tom lista anges som värde till Lista-attributet kopplat till Propertyn. 
    transactionList = new List<TransactionBase>(); 
  }

  //Metoder() - som jobbar med Listans innehåll
    //Metod för att lägga till en transaktion (inkomst eller utgift) till listan.
    public void AddTransactionMethod(TransactionBase transactionBase) //ClassNamn parameterNamn
    {      
      //Lista-object och nestad Method-Add(), med transactionBase = som består av transaktionsObjectet. 
      transactionList.Add(transactionBase); //Lägger till transaktionen i listan. 
    }

    //Metod för att ta bort en transaktion från listan.
    public void RemoveTransactionMethod(TransactionBase transactionBase) //ClassNamn parameterNamn
    {      
      //Lista-object och nestad Method-Remove(), med transactionBase = som består av transaktionsObjectet. 
      transactionList.Remove(transactionBase); //Tar bort transaktionen från listan. 
    }

    //Metod för att räkna ut det nuvarande totala saldot på bankkontot. 
    public decimal GetCurrentBalanceMethod() 
    {
      //börjar med att kontot har 0 kr i totalt bankkonto-värde, för att sedan bygga på med inkomster och göra avdrag för kostnader. Skapar en variabel av decimal-typ, för att ta hänsyn till ören, som bär på värdet av beräkningen. 
      decimal balanceDecimal = 0; 

      //Loopar igenom alla transaktioner (inkomster och kostnader) i Listan, en i taget. 
      foreach (TransactionBase transaction in transactionList) //(classNamn elementNamn in ListaNamn)
      {
        //kontrollerar att den aktuella iterationen i foreach-loopen (elementet i listan) är av inkomst-typ (dvs true), om den inte är det, så är det av kostnad-typ (dvs false)
        if (transaction.GetTransactionTypeStr() == TransactionType.INCOME.ToString()) 
        {
          //denna kod utförs om villkoret är sant
          //ökar det totala saldot (inkomst) med belopp-värdet för denna transaktion
          balanceDecimal += transaction.amountDecimal;
        }
        
        else 
        {
          //denna kod utförs om villkoret är falskt
          //förminskar det totala saldot (kostnad) med belopp-värdet för denna transaktion
          balanceDecimal -= transaction.amountDecimal;
        }
      }
    
      //Returnerar bankkontots totala saldo, efter alla iterationer (transaktioner) i Listan och beräkningen av de. 
      return balanceDecimal; 
    }

    //Radera transaktioner manuellt
    //Metod för att visa alla transaktioner som finns i listan, så att användaren kan välja vilken transaktions som ska tas bort. 
    public void DisplayTransactionsMethod() //Loopar genom transaktionerna i listan och skriver ut varje transaktion med ett nummer så att användaren kan välja den för att radera. 
    {
      //output: informerar användaren om vad som skall visas
      System.Console.WriteLine("Alla transaktioner: "); 

      //skapar en räknare/counter (till en numrerad punktlista), som ska ge varje transakion en siffra att listas med.
      int counter = 1; 

      //Loopar igenom alla transaktions-object i transaktions-listan, 
      //och skriver ut varje transakrion med ett nummer i en numrerad punktlista med:  
      //nummer, beskrivning, belopp, datum. 
      foreach (TransactionBase transaction in transactionList) //(classNamn elementNamn in ListaNamn) 
      {
        //output: skriver ut till användaren: 
        //Listnummer. Transaktionstyp - transaktionsbeskrivning, transaktionsbelopp kr, transakionsdatum 
        System.Console.WriteLine($"{counter}. {transaction.GetTransactionTypeStr()} - {transaction.descriptionStr}, {transaction.amountDecimal}, {transaction.dateDateTime.ToShortDateString()}"); 

        //ökar räknaren med +1, för varje iteration(element och ny rad)
        counter++; 
      }      
    }

    //Radera transaktioner manuellt - forts. 
    //Metod för att radera en transaktion baserat på användarens val (väljer ett nummer från listan). 
    public void RemoveTransactionByIndexMethod(int index) //Tar emot ett index som användaren väljer och tar bort transaktionen från listan om indexet är giltigt. 
    {
      //if-villkor som kontrollerar att index nummret finns i listan och är ett giltigt val. 
      if (0 <= index && index < transactionList.Count) //första indexet startar på 0. 
      {
        //tar bort transaktionen på en specifik index-position. 
        transactionList.RemoveAt(index); 

        //output: skriver ut till användaren att den specifika transaktionen har raderats. 
        System.Console.WriteLine("Transaktionen har raderats. ");
      } 
      else 
      {
        //output: skriver ut till användaren att ett ogiltigt val har gjorts. 
        System.Console.WriteLine("Ogiltigt val. ");
      }
    }

    //Se statistik för inkomst och utgifter utgifter årsvis, månadsvis, dagvis och veckovis - (för år)
    //Metod för att räkna ut den totala inkomsten och kostnad per år. 
    public void DisplayYearlyStatsMethod(int year) 
    {
      //skapar 2 decimal variabler som startar på 0 (som senare ska återanvändas i beräkning av nya siffror), 
      //för att räkna ut den totala inkomsten och kostnaden för ett år. 
      decimal totalIncome = 0; //för inkomst. 
      decimal totalExpense = 0; //för kostnad.  

      //Loopar igenom listan för alla transaktions-object, och kontrollerar vilka som har datum som stämmer överens med villkoret (för datum inom ett specifikt år). 
      foreach (TransactionBase transaction in transactionList) //(classNamn elementNamn in ListaNamn)
      {
        //skapar if-villkor som kontrollerar att datumet på transaktionerna har motsvarande datum för år, som inkommer som argument (int year). 
        if (transaction.dateDateTime.Year == year) //villkor för transaktioner med rätt år i datumet, skapas nytt ytterligare if-villkor 
        {
          if (transaction.GetTransactionTypeStr() == TransactionType.INCOME.ToString()) //villkor för transaktioner med rätt transaktionstyp (inkomst), och kostnader hamnar i else-kodblocket
          {
            totalIncome += transaction.amountDecimal; //beräknar (total) inkomst för given period
          }
          else 
          {
            totalExpense += transaction.amountDecimal; //beräknar (total) kostnad för given period
          }
        }        
      }

      //output: Skriver ut årsstatistik 
      System.Console.WriteLine($"Årsvis statistik för {year}: "); 
      System.Console.WriteLine($"Totala inkomster: {totalIncome} kr"); 
      System.Console.WriteLine($"Totala inkomster: {totalExpense} kr"); 
      System.Console.WriteLine($"Netto: {totalIncome - totalExpense} kr"); 
    }
    
    //Se statistik för inkomst och utgifter årsvis, månadsvis, dagvis och veckovis - forts. (för månader)   
    //Metod för att räkna ut den totala inkomsten och kostnad per år och månad. 
    public void DisplayMonthlyStatsMethod(int year, int month) 
    {
      //skapar 2 decimal variabler som startar på 0 (som senare ska återanvändas i beräkning av nya siffror), 
      //för att räkna ut den totala inkomsten och kostnaden för ett år - månad. 
      decimal totalIncome = 0; //för inkomst. 
      decimal totalExpense = 0; //för kostnad.  

      //Loopar igenom listan för alla transaktions-object, och kontrollerar vilka som har datum som stämmer överens med villkoret (för datum inom ett specifikt år och månad). 
      foreach (TransactionBase transaction in transactionList) //(classNamn elementNamn in ListaNamn)
      {
        //skapar if-villkor som kontrollerar att datumet på transaktionerna har motsvarande datum för år och månad, som inkommer som argument (int year, int month). 
        if (transaction.dateDateTime.Year == year && transaction.dateDateTime.Month == month) //villkor för transaktioner med rätt år och månad i datumet, skapas nytt ytterligare if-villkor 
        {
          if (transaction.GetTransactionTypeStr() == TransactionType.INCOME.ToString()) //villkor för transaktioner med rätt transaktionstyp (inkomst), och kostnader hamnar i else-kodblocket
          {
            totalIncome += transaction.amountDecimal; //beräknar (total) inkomst för given period
          }
          else 
          {
            totalExpense += transaction.amountDecimal; //beräknar (total) kostnad för given period
          }
        }        
      }

      //output: Skriver ut månadsstatistik 
      System.Console.WriteLine($"Månadsvis statistik för {year}-{month}: "); 
      System.Console.WriteLine($"Totala inkomster: {totalIncome} kr"); 
      System.Console.WriteLine($"Totala inkomster: {totalExpense} kr"); 
      System.Console.WriteLine($"Netto: {totalIncome - totalExpense} kr"); 
    } 

    //Se statistik för inkomst och utgifter årsvis, månadsvis, dagvis och veckovis - forts. (för dagar)   
    //Metod för att räkna ut den totala inkomsten och kostnad per år, månad och dag. 
    public void DisplayDailyStatsMethod(int year, int month, int day) 
    {
      //skapar 2 decimal variabler som startar på 0 (som senare ska återanvändas i beräkning av nya siffror), 
      //för att räkna ut den totala inkomsten och kostnaden för ett år - månad - dag. 
      decimal totalIncome = 0; //för inkomst. 
      decimal totalExpense = 0; //för kostnad.  

      //Loopar igenom listan för alla transaktions-object, och kontrollerar vilka som har datum som stämmer överens med villkoret (för datum inom ett specifikt år, månad och dag). 
      foreach (TransactionBase transaction in transactionList) //(classNamn elementNamn in ListaNamn)
      {
        //skapar if-villkor som kontrollerar att datumet på transaktionerna har motsvarande datum för år, månad och dag, som inkommer som argument (int year, int month, int day). 
        if (transaction.dateDateTime.Year == year && transaction.dateDateTime.Month == month && transaction.dateDateTime.Day == day) //villkor för transaktioner med rätt år, månad och dag i datumet, skapas nytt ytterligare if-villkor 
        {
          if (transaction.GetTransactionTypeStr() == TransactionType.INCOME.ToString()) //villkor för transaktioner med rätt transaktionstyp (inkomst), och kostnader hamnar i else-kodblocket
          {
            totalIncome += transaction.amountDecimal; //beräknar (total) inkomst för given period
          }
          else 
          {
            totalExpense += transaction.amountDecimal; //beräknar (total) kostnad för given period
          }
        }        
      }

      //output: Skriver ut dagsstatistik 
      System.Console.WriteLine($"Dagvis statistik för {year}-{month}-{day}: "); 
      System.Console.WriteLine($"Totala inkomster: {totalIncome} kr"); 
      System.Console.WriteLine($"Totala inkomster: {totalExpense} kr"); 
      System.Console.WriteLine($"Netto: {totalIncome - totalExpense} kr"); 
    } 

    //Se statistik för inkomst och utgifter årsvis, månadsvis, dagvis och veckovis - forts. (för veckor)
    //Metod för att räkna ut den totala inkomsten och kostnad per år och vecka. 
    public void DisplayWeeklyStatsMethod(int year, int week) 
    {
      //skapar 2 decimal variabler som startar på 0 (som senare ska återanvändas i beräkning av nya siffror), 
      //för att räkna ut den totala inkomsten och kostnaden för ett år och en vecka. 
      decimal totalIncome = 0; //för inkomst. 
      decimal totalExpense = 0; //för kostnad.  

      // Hämtar systemets aktuella kultur (t.ex. sv-SE) för datum/sifferformatering, för att kunna se statestik sorterat på veckor. 
      var culture = System.Globalization.CultureInfo.CurrentCulture; 

      //Loopar igenom listan för alla transaktions-object, och kontrollerar vilka som har datum som stämmer överens med villkoret (för datum inom ett specifikt år och vecka). 
      foreach (TransactionBase transaction in transactionList) //(classNamn elementNamn in ListaNamn)
      {
        //omvandlar datum-formateringen i transaktions-objecten, till ett vecko-format, som lagras i en vecko-variabel, för att kunna använda denna variabeln i if-villkoret (vid filtrering på veckor). 
        var transactionWeek = culture.Calendar.GetWeekOfYear(transaction.dateDateTime, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday); 
          //Metoden .GetWeekOfYear(), tar 3 argument:
            //Det datum vi vill undersöka (transaction.dateDateTime).
            //Regeln för hur veckor numreras (System.Globalization.CalendarWeekRule.FirstDay).
            //Vilken veckodag som räknas som den första dagen på veckan (DayOfWeek.Monday).

        //skapar if-villkor som kontrollerar att datumet på transaktionerna har motsvarande datum för år och vecka, som inkommer som argument (int year, int week). 
        if (transaction.dateDateTime.Year == year && transactionWeek == week) //villkor för transaktioner med rätt år och vecka i datumet, skapas nytt ytterligare if-villkor
        {
          if (transaction.GetTransactionTypeStr() == TransactionType.INCOME.ToString()) //villkor för transaktioner med rätt transaktionstyp (inkomst), och kostnader hamnar i else-kodblocket
          {
            //beräknar (total) inkomst för given period 
            totalIncome += transaction.amountDecimal;
          } 
          else 
          {
            //beräknar (total) kostnad för given period 
            totalExpense += transaction.amountDecimal;
          }
        }
      }

      //output: Skriver ut veckostatistik 
      System.Console.WriteLine($"Veckovis statistik för år {year}, vecka {week}: "); 
      System.Console.WriteLine($"Totala inkomster: {totalIncome} kr"); 
      System.Console.WriteLine($"Totala kostnader: {totalExpense} kr"); 
      System.Console.WriteLine($"Netto: {totalIncome - totalExpense} kr"); 
    }
}

