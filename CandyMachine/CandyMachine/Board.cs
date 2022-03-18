using System;

namespace Project
{
    public class Board
    {
        public static void Print(string message = "", int selection = 0, decimal price = 0,  decimal received = 0, decimal returned = 0, string result = "")
        {
            string choiceString = selection.ToString().PadLeft(2, '0'); //mettre un 0 à gauche dans le cas d'un chiffre: 1 => 01
            string priceString = PadPrice(price);//formatter prix
            string receivedString = PadPrice(received);//formatter prix recu par l'utilisateur
            string returnedString = PadPrice(returned);//formatter prix retourné à l'utilisateur
            string messageString = PadBoth(message == "" ? "VOTRE CHOIX ?" : message, 33);//affichage message à l'utilisateur avant de compléter l'achat
            string resultString = PadBoth(result, 24);//message résultat après l'achat
            Console.Clear();
            Console.WriteLine($"-----------------------------------");
            Console.WriteLine($"|        Machine à bonbons        |");
            Console.WriteLine($"|----------------------------------");
            Console.WriteLine($"|{messageString}|");
            Console.WriteLine($"|----------------------------------");
            Console.WriteLine($"| 01 | 02 | 03 | 04 | 05 |-> { choiceString } <-|");
            Console.WriteLine($"|------------------------|--------|");
            Console.WriteLine($"| 06 | 07 | 08 | 09 | 10 |  PRIX  |");
            Console.WriteLine($"|------------------------|  { priceString } |");
            Console.WriteLine($"| 11 | 12 | 13 | 14 | 15 |--------|");
            Console.WriteLine($"|------------------------|  PERÇU |");
            Console.WriteLine($"| 16 | 17 | 18 | 19 | 20 |  { receivedString } |");
            Console.WriteLine($"|------------------------|--------|");
            Console.WriteLine($"| 21 | 22 | 23 | 24 | 25 |  REMIS |");
            Console.WriteLine($"|------------------------|  { returnedString } |");
            Console.WriteLine($"|{resultString}|--------|");
            Console.WriteLine($"-----------------------------------");
        }

        private static string PadPrice(decimal price)//formater prix: retourner le prix d'un bonbon avec le format "0.00$". exemple 1 => 1.00$
        {
            return price == 0 ? "-----" : price.ToString("0.00$").PadLeft(5, '0'); //format compact d'une seule instruction if-else
            
            //    if (price == 0) return "-----";
            //    else return price.ToString("0.00$").PadLeft(5, '0');          
        }

        public static string PadBoth(string source, int length)
        {
            int spaces = length - source.Length; //espace restant de 33 (espace vide)
            int padLeft = spaces / 2 + source.Length;//ajout de la moitié d'espace vide à la taille du message (variable source)
            return source.PadLeft(padLeft).PadRight(length);//ajout de padleft espace à gauche et length espace à droite:
                                                            //-->PadLeft(padleft): compter de droite à gauche padleft fois 
                                                            //-->Padright(length) :compter de gauche à droite length fois
        }
    }
}
