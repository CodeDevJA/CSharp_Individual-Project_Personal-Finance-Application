//FileManager sköter filhantering. Den sparar och laddar transaktioner från en fil. 
//Den här klassen kommer att hantera filinläsning och filskrivning 
//(dvs läsa från fil och spara till fil). 

//FileManager är en Class som genrerar ett object, som i sin tur kan anropa Metoder från FileManager-Classen. Dessa Metoder har olika uppgifter. 
//En av metoderna ska läsa in alla transaktioner från en fil och omvandla dessa så att informationen kan lagras i Listan igen (i objectet från FinanceManager-Classen). 
//En annan av metoderna ska skriva ner alla transaktioner från Listan (i objectet från FinanceManager-Classen) och omvandla dessa så att informationen kan sparas/lagras i filen. 
//Alla metoder kommer man åt genom FileManager-Classen. 
public class FileManager 
{
  //sökvägen till filen där alla transaktioner sparas ner och laddas upp igen. 
  private string filePathStr = "finance_data_accont_transactions-file";

  //Metod() - som sparar alla transaktioner till en fil. 
  public void SaveToFileMethod(List<TransactionBase> transactionList) //ClassNamn parameterNamn
  {
    //using () - möjliggör en abstraktion, vid frigörandet av resurser med en automatisk metod. 
    //med using slipper man det manuella arbetet, och koden blir mer läsbar och träffsäker. 
    //När man är klar med att skriva till fil, måste man antingen stänga eller låta systemet frigöra resurserna, vilket sker automatiskt när man använder using-satsen här. 
      //skapar ett object från Classen StreamWriter (med hjälp av en Constructor, som tar filen (där datan ska sparas), som parameter) och lagrar Streamwriter-objectet i variabeln writer.
      using (StreamWriter writer = new StreamWriter(filePathStr)) //Öppnar filen för skrivning. 
      {
        //Loopar/itererar igenom Listan med alla transaktionerna (en i taget) 
        foreach (TransactionBase transaction in transactionList) //(classNamn elementNamn in ListaNamn) 
        {
          //skriver ner ett element i taget, påvarsin rad, från listan till filen där det ska sparas. Skriver typ av transaktion, beskrivning, summa och datum till filen med ";" mellan varje del. 
          writer.WriteLine($"{transaction.GetTransactionTypeStr()};{transaction.descriptionStr};{transaction.amountDecimal};{transaction.dateDateTime}");
        }
      }
  }

  //Metod() - som laddar alla transaktioner från en fil. 
  public List<TransactionBase> LoadFromFileMethod() 
  {
    //skapar en ny tom lista för bankkonto transaktionerna.
    List<TransactionBase> transactionList = new List<TransactionBase>();

    //GuardClaus - om filen inte finns, returnera en tom lista 
    //(det finns inget att läsa från).
    if (!File.Exists(filePathStr)) 
    {
      return transactionList;
    }

    //using () {} - using kommer att stänga filen igen när {kod-blocket} är klart och frigör sedan resurser. 
    //StreamReader öppnar filen för att läsa transaktionerna en rad i taget. 
    using (StreamReader reader = new StreamReader(filePathStr)) 
    {
      //skapar en tom variabel lineStr, för att sedan i efterhand lagra (läsa in) varje rad med data från filen.
      string lineStr; 
      //while-Loop, som itererar(loopar) tills det inte längre finns några nya rader i filen att hämta (läsa in) data ifån. .ReadLine() används för att läsa in en rad i taget och lagra dessa i string variabeln lineStr. 
      while ((lineStr = reader.ReadLine()!) != null) 
      {
        //delar upp varje iteration (loop) som en separat del i en transaktion och letar efter ";" för att skilja på delarna (typ; beskrivning; belopp; datum). Varje del lagras sedan i en strArray partsStrArr. 
        string[] partsStrArr = lineStr.Split(';');

        //villkorsats som avgör om typen är en inkomst eller en kostnad och utför respektive kod-block (skapar rätt typ av transaktion) för respektive villkors resultat. 
          //kontrollerar om första elementet i strArrayen är "Income"-värde.
          if (partsStrArr[0] == "Income") 
          {
            //lägger till en ny transaktion (nytt object från FinanceManager) till Listan, med data som var sparat i filen, en rad i taget. 
            //tar en strArr itaget, delar upp de olika elementen, till respektive del av argumenten som skickas in till FinanceManagerConstructorn, som tas emot som parametrar, för att skapa ett nytt object med dessa värden och lägger sedan till detta object som en transaktion i Listan igen, för inkomster. 
            transactionList.Add(new IncomeTransaction(partsStrArr[1], decimal.Parse(partsStrArr[2]), DateTime.Parse(partsStrArr[3]))); 
          }
          
          //kontrollerar om första elementet i strArrayen är "Expense"-värde. 
          else if (partsStrArr[0] == "Expense") 
          {
            //lägger till en ny transaktion (nytt object från FinanceManager) till Listan, med data som var sparat i filen, en rad i taget. 
            //tar en strArr itaget, delar upp de olika elementen, till respektive del av argumenten som skickas in till FinanceManagerConstructorn, som tas emot som parametrar, för att skapa ett nytt object med dessa värden och lägger sedan till detta object som en transaktion i Listan igen, för kostnader. 
            transactionList.Add(new ExpenseTransaction(partsStrArr[1], decimal.Parse(partsStrArr[2]), DateTime.Parse(partsStrArr[3])));
          }
      }
    }

    //Returnerar Listan med laddade transaktioner, från datan som var sparad till fil. 
    return transactionList; 
  }
}

