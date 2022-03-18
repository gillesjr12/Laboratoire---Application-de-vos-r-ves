using Project;
using static Project.Board;



namespace CandyMachine
{

    class Program
    {

        public static class candyData
        {
            public static Data dataManager = new Data();
            public static Candy[] candies = dataManager.LoadCandies();
        }
        
        private static int userNumInput() //Force user to put an integral
        {
            int input;
            bool tryParse = true;
            do
            {
                tryParse = int.TryParse(Console.ReadLine(), out input);
                Console.Write("Veuillez entrez la bonne selection : ");
            } while (tryParse != true);
            
            return input;
        }
        
        
        private static int GetSelection() // Take the user's selection
        {
            int input;

            do
            {
                Print();
                Console.Write("Veuillez faire votre selection [1-25] : ");
                input = userNumInput();
            } while (input <= 0 || input > 25);

            return input;
        }


        public static int GetCandy(int input) //Take the correct position in the array
        {
            return input - 1;
        }
        

        static decimal GetCoin(decimal Coin) //User put money in the Candy Machine
        {
            char Quit = 'N';
            int input = 0;
            Console.WriteLine("[0] = Annuler");
            Console.WriteLine("[1] = 5c");
            Console.WriteLine("[2] = 10c");
            Console.WriteLine("[3] = 25c");
            Console.WriteLine("[4] = 1$");
            Console.WriteLine("[5] = 2$");
            Console.Write("--> ");
        
                input = userNumInput(); //User take the number between 0 to 5

                if (input <= 5 || input >= 0)
                {
                    switch (input)
                    {
                        case 0:
                            if (Coin >= 0m)
                            {
                                Console.Clear();                                                                                                    // Clear the machine
                                if (Coin  > 0);
                                {
                                    Print($"Voici votre remboursement", 0,0,0, Coin, "A la prochaine fois !");  // to show the new print with refund and cancelation
                                }
                                if (Coin == 0);
                                {
                                    Print($"Vous voulez partir ?", 0,0,0, Coin, "Restez encore un peu :(");  // to show the new print with begging to stay
                                }

                                do
                                {
                                    Console.Write("Voulez-vous quittez ? [O/N]"); // User make the choice
                                    Quit = Console.ReadLine()[0]; // if he wanna stay
                                    if (Quit == 'n' || Quit == 'N')
                                    {
                                        Console.Clear();
                                        Main(); // If he stay, we take him back to the menu
                                    }
                                    else if (Quit == 'o' || Quit == 'O')
                                    {

                                        Environment.Exit(0); // If he leave, we close the console
                                    }
                                    else
                                    {
                                        Console.WriteLine("Veuillez entrez un charactere valide."); 
                                        Thread.Sleep(1000);
                                    }
                                } while (Quit != 'n' || Quit != 'N' || Quit != 'o' || Quit != 'O');  // We loop if the user's enter a bad character

                            }
                            
                            break;
                        case 1:
                            Coin += 0.05m;
                            break;
                        case 2:
                            Coin += 0.10m;
                            break;
                        case 3:
                            Coin += 0.25m;
                            break;
                        case 4:
                            Coin += 1m;
                            break;
                        case 5:
                            Coin += 2m;
                            break;
                    }
                }
                return Coin;
        }

        static decimal cashReturn(decimal Coin, int input) //Calculate the return if the user wants to leave
        {
            decimal Money;
            Money = Coin - candyData.candies[GetCandy(input)].Price;
            return Money;
        }

        static void Music()
        {
            Console.Write("R");Console.Beep(1188,500);Console.Write("é");Console.Beep(1408,250);Console.Write("g");Console.Beep(1760,500);Console.Write("a");Console.Beep(1584,250);Console.Write("l");
            Console.Beep(1408,250);Console.Write("e");Console.Beep(1320,750);Console.Write("z");Console.Beep(1056,250);Console.Write("-");Console.Beep(1320,500);Console.Write("v");
            Console.Beep(1188,250);Console.Write("o");Console.Beep(1056,250);Console.Write("u");Console.Beep(990,500);Console.Write("s");Console.Beep(990,250);Console.Write(" b");
            Console.Beep(1056,250);Console.Write("i");Console.Beep(1188,500);Console.Write("e");Console.Beep(1320,500);Console.Write("n");Console.Beep(1056,500);Console.Write(" !");
            Console.Beep(880,500);Console.Write("!");Console.Beep(880,500);
            Thread.Sleep(500);
        } // Beautiful little song if the user buy a candy

        static void videColor()
         {
             Console.ForegroundColor = ConsoleColor.Red;
         } // Red method if there's no more candy in the Machine
        static void normalColor()
         {
             Console.ForegroundColor = ConsoleColor.White;
         } // Return to white color after the user choose a candy out of stock
        
        static void Main()
        {
            int input = 0;
            decimal Coin = 0;
            decimal Money = 0;
            bool IsRunning = true;
            

            while (IsRunning == true) // Putting a true boolean to make the machine repeat
            {

                Console.Clear();
                Print();
                input = GetSelection();  // User choose a number in the candy list

                if (candyData.candies[GetCandy(input)].Stock == 0) // If there's no stock
                {                                                  // We change the front color in red
                    Console.Clear();                                // And say that this item is out of stock
                    videColor();                                     // And offer to take another one | we switch in white color as front end     
                    Print($"{candyData.candies[GetCandy(input)].Name} est vide", input);
                    Console.WriteLine($"{candyData.candies[GetCandy(input)].Name} est vide, veuillez faire un autre choix.");
                    Thread.Sleep(3000);
                    normalColor();
                }
                else if (candyData.candies[GetCandy(input)].Stock > 0) // If there's candy in stock
                {
                    do
                    {
                        Console.Clear();
                        Print($"{candyData.candies[GetCandy(input)].Name}", input,candyData.candies[GetCandy(input)].Price,Coin); // We print the Board with the candy information
                        Coin = GetCoin(Coin);  //The user can insert is money
                        if (Coin >= candyData.candies[GetCandy(input)].Price ) // If he put enough or too much money for the candy
                        {   
                                Console.Clear();
                                candyData.candies[GetCandy(input)].Stock--; // We give him a candy 
                                Print($"Prenez votre friandise", input,candyData.candies[GetCandy(input)].Price,Coin, cashReturn(Coin,input), candyData.candies[GetCandy(input)].Name); 
                                Console.WriteLine($"Vous avez acheté un {candyData.candies[GetCandy(input)].Name} à {candyData.candies[GetCandy(input)].Price}$ !");
                                Console.WriteLine($"Il vous revient {cashReturn(Coin, input)}$");
                                Console.WriteLine();
                                Music(); // Do a beautiful music to have a client's satisfaction
                                Thread.Sleep(2000);
                        }

                    } while (candyData.candies[GetCandy(input)].Price > Coin); // As far the client don't put enough money, we loop.
                    Coin = 0;
                }
            }
        } 
    }
}