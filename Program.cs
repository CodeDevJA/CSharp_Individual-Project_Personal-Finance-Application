using System; 
//Ger tillgång till grundläggande funktioner som Console och DateTime, mm.

using System.Collections.Generic; 
//Ger tillgång till List och andra samlingsklasser. 

using System.IO; 
//Ger tillgång till möjligheten att läsa från fil och skriva till fil.

using System.Linq; 
//Ger tillgång till att kunna filtrera och söka i listor lättare, mm. 

using System.Globalization; 
// CultureInfo.CurrentCulture används för att få den aktuella kulturen (t.ex. sv-SE)
// som styr formatering av datum, siffror, etc. baserat på systemets regionala inställningar. 

namespace individuell_uppg;

//Huvudprogrammet där användaren iteragerar med FinanceManager (hantering av Transaktioner) 
// och FileManager (Läsa från och spara till fil)
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

        //skapar en bool variabel som får värdet true, som ska användas i en while-Loop, för att användaren hela tiden ska få möjligheten att göra nya val, vid interaktion med FinanceManager-appen. Denna variabel får värdet false, när användaren vill avsluta appen, senare. 
        bool keepRunning = true;

        //while-Loop, som håller igång menyvalen aktiva tills användaren vill lämna appen. 
        while (keepRunning) 
        {
            //Huvudmeny - output: visar användaren tillgängliga menyval.
            System.Console.WriteLine("Välkommen till budgethanteringssystemet! ");
            System.Console.WriteLine("Välj ett alternativ (svara med en av dessa siffror). ");
            System.Console.WriteLine("1. Lägg till en transaktion. ");
            System.Console.WriteLine("2. Visa alla transaktioner. ");
            System.Console.WriteLine("3. Ta bort en transaktion. ");
            System.Console.WriteLine("4. Visa nuvarande saldo på bankkontot. ");
            System.Console.WriteLine("5. Visa statestik. ");
            System.Console.WriteLine("6. Spara och avsluta. "); 

            //spara userInput i en string variabel. 
            string userChoice = Console.ReadLine()!;

            //återanvänd userInput variabeln för att anropa ett menyval. 
            switch (userChoice) 
            {
                //Lägg till en transaktion
                case "1":
                //transaktions typ
                System.Console.WriteLine("Ange en transaktionstyp (1 för inkomst, 2 för kostnad): ");
                string typeChoice = Console.ReadLine()!;
                
                //transaktions beskrivning
                System.Console.WriteLine("Ange en beskrivning: ");
                string description = Console.ReadLine()!;
                
                //transaktions belopp
                System.Console.WriteLine("Ange ett belopp: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine()!);
                
                //transaktions datum
                System.Console.WriteLine("Ange ett datum (yyyy-mm-dd): ");
                DateTime date = DateTime.Parse(Console.ReadLine()!);
                
                //menyval för transaktions typ: inkomst eller kostnad
                if (typeChoice == "1") 
                {
                    //lägger till en transaktion av typen inkomst
                    financeManager.AddTransactionMethod(new IncomeTransaction(description, amount, date)); 
                } 
                else 
                {
                    //lägger till en transaktion av typen kostnad
                    financeManager.AddTransactionMethod(new ExpenseTransaction(description, amount, date)); 
                }

                break; 
                
                //Visa alla transaktioner
                case "2":
                financeManager.DisplayTransactionsMethod();
                break;

                //Ta bort en transaktion
                case "3":
                //visar alla transaktioner
                financeManager.DisplayTransactionsMethod(); 
                //output: användaren får information om att välja en transaktions att ta bort
                System.Console.WriteLine("Ange numret på transaktionen du vill radera: "); 
                //användaren väljer en transaktions att ta bort och valet omvandlas till motsvarande indexNummer
                int indexToDelete = Convert.ToInt32(Console.ReadLine()) - 1;
                //Metod som tar bort transaktion, anropas
                financeManager.RemoveTransactionByIndexMethod(indexToDelete);
                break;

                //Visa nuvarande saldo på bankkontot
                case "4":                
                //output: skriver ut nuvarande saldo på bankkontot till användaren 
                System.Console.WriteLine($"Ditt nuvarande saldo på bankkontot är: {financeManager.GetCurrentBalanceMethod()} kr. ");                 
                break;

                //Visa statistikmeny
                case "5":
                //output: användaren får information om att välja ett meyval för hur transaktionerna ska filtreras
                System.Console.WriteLine("Välj statistik: 1. Årsvis, 2. Månadsvis, 3. Veckovis, 4. Dagvis "); 
                //spara statsChoice i en string variabel. 
                string statsChoice = Console.ReadLine()!;

                //återanvänd statsChoice variabeln för att anropa ett menyval, via ett switch-villkor. 
                switch (statsChoice) 
                {
                    //sortera transaktions statestik årsvis
                    case "1":
                    //output: användaren får information om att skriva in vilket år som skall filtreras fram
                    System.Console.WriteLine("Ange år: ");
                    //användaren får skriva in vilket år som skall filtreras fram och detta omvandlas till rätt format
                    int year = Convert.ToInt32(Console.ReadLine()!); 
                    //Metod i FinanceManager anropas för att visa användaren resultatet
                    financeManager.DisplayYearlyStatsMethod(year);
                    break;

                    //sortera transaktions statestik månadsvis
                    case "2":
                    //output: användaren får information om att skriva in vilket år som skall filtreras fram
                    System.Console.WriteLine("Ange år: ");
                    //användaren får skriva in vilket år som skall filtreras fram och detta omvandlas till rätt format
                    year = Convert.ToInt32(Console.ReadLine()!); 
                    //output: användaren får information om att skriva in vilken månad som skall filtreras fram
                    System.Console.WriteLine("Ange månad (1-12): ");
                    //användaren får skriva in vilket månad som skall filtreras fram och detta omvandlas till rätt format
                    int month = Convert.ToInt32(Console.ReadLine()!); 
                    //Metod i FinanceManager anropas för att visa användaren resultatet
                    financeManager.DisplayMonthlyStatsMethod(year, month);
                    break;

                    //sortera transaktions statestik veckovis
                    case "3":
                    //output: användaren får information om att skriva in vilket år som skall filtreras fram
                    System.Console.WriteLine("Ange år: ");
                    //användaren får skriva in vilket år som skall filtreras fram och detta omvandlas till rätt format
                    year = Convert.ToInt32(Console.ReadLine()!); 
                    //output: användaren får information om att skriva in vilken vecka som skall filtreras fram
                    System.Console.WriteLine("Ange vecka (1-52): ");
                    //användaren får skriva in vilket månad som skall filtreras fram och detta omvandlas till rätt format
                    int week = Convert.ToInt32(Console.ReadLine()!); 
                    //Metod i FinanceManager anropas för att visa användaren resultatet
                    financeManager.DisplayWeeklyStatsMethod(year, week);
                    break;


                    //sortera transaktions statestik månadsvis
                    case "4":
                    //output: användaren får information om att skriva in vilken år som skall filtreras fram
                    System.Console.WriteLine("Ange år: ");
                    //användaren får skriva in vilket år som skall filtreras fram och detta omvandlas till rätt format
                    year = Convert.ToInt32(Console.ReadLine()!); 
                    //output: användaren får information om att skriva in vilken månad som skall filtreras fram
                    System.Console.WriteLine("Ange månad (1-12): ");
                    //användaren får skriva in vilket månad som skall filtreras fram och detta omvandlas till rätt format
                    month = Convert.ToInt32(Console.ReadLine()!); 
                    //output: användaren får information om att skriva in vilken dag som skall filtreras fram
                    System.Console.WriteLine("Ange dag (1-31): ");
                    //användaren får skriva in vilket dag som skall filtreras fram och detta omvandlas till rätt format
                    int day = Convert.ToInt32(Console.ReadLine()!); 
                    //Metod i FinanceManager anropas för att visa användaren resultatet
                    financeManager.DisplayDailyStatsMethod(year, month, day);
                    break;

                    //skapa ett standard värde som gäller ifall inget annat val är aktuellt i switch-villkorssatsen
                    default:
                    System.Console.WriteLine("Ogiltigt val. ");
                    break;
                }
                break;

                //Spara och avsluta
                case "6":
                //spara data till fil
                fileManager.SaveToFileMethod(financeManager.transactionList); 
                //ändra bool variabeln till false, för att while-Loopen ska som håller igång menyn ska upphöra/avbrytas
                keepRunning = false;
                break;

                //skapa ett standard värde som gäller ifall inget annat val är aktuellt i switch-villkorssatsen
                default:
                System.Console.WriteLine("Ogiltigt val. Försök igen. "); 
                break;
            }
        } 
    }
}

