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
}

