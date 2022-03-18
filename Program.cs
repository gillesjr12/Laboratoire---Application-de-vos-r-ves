namespace Candy_Machine
{
    class Program
    {
        //fonctions ici---------------------

        public static int GetSelection(int nombreMaximale) //Method to get user input and validate it.
        {
            int selection = 0; //Initializes variable to store user input

            do //gives instructions to run while the condition is TRUE
            {
                Console.Clear(); //clears console data
                Board.Print(); //Print the board & asks user to enter a choice

                selection = UserNumberInput("-->"); //Calls method to validate if input is number 
            } while
                ((selection <= 0) ||
                 (selection >
                  nombreMaximale)); //  Validates user selection (!numbers <01 & >25) and ask again if other input

            return selection; //returns user input if all conditions are valid and exits the method.
        }

        public static int UserNumberInput(string texte) //Method that validates if the entered value is a valid number
        {
            int chiffre; //declares variable to be returned
            bool tryparse; //declares boolean variable to validate against

            do
            {
                Console.Write(texte); //will write the text received when method was called
                int currentLineCursor = Console.CursorTop; //will record initial cursor position, to be used later on
                tryparse = int.TryParse(Console.ReadLine(),
                    out chiffre); //will return either false or TRUE. If TRUE, it will also return the numeric value. 
                Console.SetCursorPosition(0, currentLineCursor); //will place the cursor at the pre-recorded position
                Console.Write(new string(' ', Console.WindowWidth)); //will overwrite the whole line
                Console.SetCursorPosition(0,
                    currentLineCursor); //will place the cursor at the pre-recorded position, to receive the new user input
            } while (!tryparse); //will run over the whole code until tryparse is TRUE.

            return chiffre; //will return the validated number and exit the method.
        }


        // GetCandy() : cette fonction contiendra une seule ligne de code, c’est le return ! GetCandy()
        // prend en paramètres la sélection faite par l’utilisateur (qui est une variable entière entre 1 et 
        // 25) et retourne la case du tableau qui contient les données liées à la sélection de l’utilisateur. 
        //     Autrement dit, si l’utilisateur sélectionne le bonbon numéro 6 par exemple, GetCandy() doit
        // retourner la case du tableau qui contient ce bonbon là qui est dans ce cas candies[5] (le sixième 
        // bonbon du tableau est à la position 5 car on commence par la position 0 dans un tableau ).
        //
        public static Candy GetCandy(int selection, Candy[] candies)
        {
            Board.Print(candies[selection - 1].Name, selection,
                candies[selection - 1].Price); //Returns selected candy name and price to user

            return (candies[selection - 1]);
        }

        public static decimal GetCoin(decimal argentRecu)
        {
            int choix;
            decimal montant = 0;

            // donne la main à l’utilisateur d’entrer une valeur de ce menu qui est entre 0 et 5. Si 
            // l’utilisateur entre un valeur invalide on lui redonne la main d’entrer une nouvelle valeur…
            // b. Une fois le choix que l’utilisateur a fait est valide (de 0 à 5), la fonction retourne la valeur de 
            // la monnaie qui correspond à son choix (qui est une valeur décimale). exemple : l’utilisateur 
            //     entre le choix 1, GetCoin() retourne la valeur décimale 0.05m (qui vaut 5 sous).
            // 1 (5 sous) => 0.05m , 2 (10 sous) => 0.10m , 3 (25 sous) => 0.25 , 4 (1 dollar) => 1.00m , 5 (2 
            // dollars) => 2.00m


            do
            {
                choix = int.Parse(Console.ReadLine());

                switch (choix)
                {
                    case 1:
                        return montant += 0.05m;
                    case 2:
                        return montant += 0.10m;
                    case 3:
                        return montant += 0.25m;
                    case 4:
                        return montant += 1m;
                    case 5:
                        return montant += 2m;
                    case 0:
                        Board.Print("Annulle, Prenez votre argent", returned: argentRecu);
                        UnAutre();
                        break;
                }
            } while (choix < 0 || choix > 5);


            return montant;
        }

        public static void UnAutre()
        {
            Console.WriteLine("Appuyez sur une touche pour acheter d'autre bonbon...");

            Console.ReadKey();

            Main();
        }

        public static class PremierTableau
        {
            public static Data dataManager = new Data(); /* declaration et reservation de la mémoire de la
            variable structurée(objet) dataManager de type Data */

            public static Candy[] candies = dataManager.LoadCandies(); /* appel de la fonction LoadCandies() avec la 
            variable structurée dataManager vu que c’est une fonction propre à la classe Data et 
            qu’on ne peut pas l’utiliser ailleurs sauf en créant une variable de type Data */
        }

        public static void Main() //--------------------------------------
        {
            int selection = GetSelection(25);

            Candy bonbonSelectionne = GetCandy(selection, PremierTableau.candies);


            while (true)
            {
                if (bonbonSelectionne.Stock <= 0)
                {
                    Board.Print($"{bonbonSelectionne.Name} VIDE!", selection);

                    UnAutre();
                }

                else if (bonbonSelectionne.Stock > 0)
                {
                    decimal argentRecu = 0m;


                    do
                    {
                        Console.Clear();
                        Board.Print($"{bonbonSelectionne.Name}", selection, bonbonSelectionne.Price, argentRecu);

                        Console.WriteLine(
                            "[0] = Annuler \n[1] = 5c \n[2] = 10c \n[3] = 25c \n[4] = 1$ \n[5] = 2$"
                        );
                        Console.Write("-->");

                        argentRecu = (argentRecu + GetCoin(argentRecu)); //add payment to total (decimal: ArgentRecu)
                    } while (argentRecu < bonbonSelectionne.Price);

                    decimal remettre = 0m;

                    remettre = (argentRecu - bonbonSelectionne.Price);


                    Console.Clear();
                    Board.Print($"Prenez votre friandise...", selection, bonbonSelectionne.Price, argentRecu,
                        remettre, $"{bonbonSelectionne.Name}");

                    argentRecu = 0m;
                }

                bonbonSelectionne.Stock = bonbonSelectionne.Stock - 1;

                Console.WriteLine("Stock : " + bonbonSelectionne.Stock);

                UnAutre();
            }
        }


        //fin de Main()----------------------------------------
    }
}