//en abstract class som består av de ingående detaljerna i en transaktion, tex lön eller bidrag (för inkomst) och tex räkningar (för kostnader/utgifter)
//den ska vara abstract för att det ska vara en mall som andra klasser ärver från, men den ska vara public så att alla kan se innehållet i den. 
//en instans/ett objekt som genereras från denna mall skall bestå av de uppgifter en transaktion ska ha, dvs, transaktions beskrivning, transaktionsbelopp, transaktionsdatum
public abstract class TransactionBase 
{
  //shorthand Properties - är mer kortfattad syntax, delar av kod blir abstraherade så här, som annars hade synts vid manuell kodning
  //(istället för att manuellt skriva ut alla attribut och dess kopplingar till Properties i resp. kod-block)
  public string? descriptionStr { get; set; } //beskrivning (string) av transaktionen (tex "lön")
  public decimal amountDecimal { get; set; }  //belopp (decimal) för transaktionen (tex 2500.50)
  public DateTime dateDateTime { get; set; }  //datum (DateTime) för transaktionen (tex 2024-10-05)

  //Constructor - för att skapa ett object av denna Class (för transaktioner), 
  //och tar emot information från användaren för egenskaperna, beskrivning, belopp och datum, som fördefinieras vid skapandet av transaktionsObjektet
  public TransactionBase(string descriptionStr, decimal amountDecimal, DateTime dateTime) 
  {
    //tar emot inkommande argument (via deklarerad parameter), och tilldelar inkommande värde till det genererade objektets egenskaper (när objectet skapas), dvs dold {attribut-namn}
    //this.{attribut-namn} hänvisar till {attribut-namn} inom samma Class (dvs det dolda abstraherade attributet som tilldelas sitt värde genom shorthandPropertyn)
    this.descriptionStr = descriptionStr;     //Ger {attribut-namn} beskrivningen till det angivna värdet 
    this.amountDecimal = amountDecimal;       //Ger {attribut-namn} beloppet till det angivna värdet 
    this.dateDateTime = dateDateTime;         //Ger {attribut-namn} datumet till det angivna värdet 
  } 

  //abstract Metod som ska ärvas och implementeras av Sub-Classer, för att specificera transaktionstypen, (tex "inkomst" eller "kostnad")
  //obs! denna metod måste implementeras av alla Classer som ärver från Classen TransactionBase 
  public abstract string GetTransactionTypeStr(); 

}

