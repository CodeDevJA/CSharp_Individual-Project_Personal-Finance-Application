//en sub-class (som ärver från) till TransactionBase, som består av de ingående detaljerna i en transaktion, tex lön eller bidrag (för inkomst)
//den ska INTE vara abstract för att Classen ska kunna generera object och anropa tillhörande Metoder och den ska vara public så att alla kan se innehållet i den. 
//är en instans/ett objekt som genereras från Base-Classen och är fördefinerad med de argument som användes för en transaktion, dvs, transaktions beskrivning, transaktionsbelopp, transaktionsdatum, när Constructorn anropades 
public class IncomeTransaction : TransactionBase
{
  //Constructor - för att skapa ett object av denna Class (för transaktioner), 
  //och tar emot information från användaren för egenskaperna, beskrivning, belopp och datum, som fördefinieras vid skapandet av transaktionsObjektet
  //istället för att definera Constractorn i detta kod-block (som lämnas tomt), 
  //anropas BaseClassen som den ärver ifrån och skickar med inkommande argument i deklarerade parametrar och återanvänder dessa i ett nytt argument som skickas till Constructorn i BaseClassen. 
  public IncomeTransaction(string descriptionStr, decimal amountDecimal, DateTime dateDateTime) : base(descriptionStr, amountDecimal, dateDateTime) // anropar Base-Constructorn 
  {    
  }

    //implementerar (återanvänder och om-/definerar) den abstracta Metoden från Base-Classen.
    //obs! Metod-namnet är samma i alla Class-nivåer, med skillnaden att "abstract"-delen blir istället "override" (som betyder skriver över/omdefinerar). 
    public override string GetTransactionTypeStr()
    {
      return "Income";  //Returnerar "Income" (Inkomst i text-form, string) som typ av transaktion
    }
}

