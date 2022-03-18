
    using System;
    using System.Runtime.InteropServices;
    
    namespace Machinebonbon
    {
        public class Program
        {
            private static Candy[] t_candy;//déclare un tableau d'objet Candy
            private static decimal mDepAccu;
            private static int choix = 0;
            
           // private static Data data_use = new Data();//creer un objet de type data
           // t_candy = data_use.LoadCandies();//Se sert d'une fonction de l'objet data_use qui retourne un tableau d'objet Candy

            public static void Main()
            {

                Data dataUse = new Data();//creer un objet de type data
                 t_candy = dataUse.LoadCandies();//Se sert d'une fonction de l'objet data_use qui retourne un tableau d'objet Candy
                 Console.BackgroundColor = ConsoleColor.Black;//ajouter d'un peu de color black is default
                 Console.ForegroundColor = ConsoleColor.Blue;//change le texte de la console en bleu
                 Menu_Bonbon();
                 
            }


                public static void Menu_Bonbon()
                {
                    bool laVieEstBelle = true;//variable affectue aved une valeur true
                

                        while (laVieEstBelle)//boucle infini pour executer le corps du programme
                        {
                            
                            Board.Print("Votre Bad Choice");//affiche le board avec u message
                            Console.Beep();
                            Console.Beep();//beep ;)
                            
                       
                        do
                        {
                            
                            int intstring;
                            bool intResultTryparse = int.TryParse(Console.ReadLine(), out intstring);
                            if (intResultTryparse == true)//execute si le tryparse a fonctionner
                            {
                                
                                choix = intstring;//stock la donner entrer converti par Tryparse
                                choix -= 1;//adjust choix a la valeur d'index d'un tableau
                            }
                            else Menu_Bonbon();//dans le cas ou l'entrer est inadequate un apelle cette fonction
                        } while (choix > 24 || choix < 0);//execute la fonction tant qu'on a pas entrer un numbre valide
                    
                        Candy unCandy = GetCandy(choix);//apelle une fonction qui va chercher le bonbon dans le tableau d'objet Candy et retourne l'objet de l'indez correspondant
                    
                        if (unCandy.Stock > 0)//s'execute si le le bonbon est en stock
                        {
                            Board.Print(unCandy.Name, choix + 1, unCandy.Price);//affiche le bonbon selectionner et son prix d'achat
                            mDepAccu = 0m;
                    
                            do
                            {
                                mDepAccu += GetCoin(unCandy.Price);//compteur de monnaie deposer
                                Board.Print(unCandy.Name, choix, unCandy.Price, mDepAccu);//affichage du depot de monnaie
                                
                            } while (mDepAccu < unCandy.Price);//continue la boucle tant que il n'y a pas asez d'argent acccumuler
                    
                            decimal argARemettre = mDepAccu - unCandy.Price;
                            Board.Print("Prenez votre Friandise Enjoy!!", choix, unCandy.Price, mDepAccu, argARemettre,
                                unCandy.Name);//affichage lors de l'achat du bonbon
                            t_candy[choix].Stock -= 1;//diminue le stock de 1
                           for(int i = 0; i < 3;i++) Console.Beep();//on s'amuse
                            Console.Write("\nVoulez vous un acheter un autre bonbon [y/n]");

                            char char_Rep;
                           
                            bool charResultTryparse = char.TryParse(Console.ReadLine(), out char_Rep);
                            if (charResultTryparse == true)//execute si le tryparse a fonctionner
                            {
                                if (char_Rep == 'Y' || char_Rep == 'y') Menu_Bonbon();//si la reponse est Y/y execute la fonction a nouveau
                                else Environment.Exit(1);//sortie du programme
                                
                            }
                            

                        }
                        else
                        {
                            Board.Print("VIDE");//affiche Vide sur le board vu que le stock est a 0
                            Console.Beep(5000, 200);
                            Console.Write("\nAppuyez sur une touche pour choisir un autre bonbon...");
                            var onsenfou = Console.ReadLine();//pause qui demande un entrer
                            
                    
                        }
                        }
                
                
                }
            
                public static Candy GetCandy(int choix)//fonction qui recois le choix de bonbon selectionner par l'utilisateur
                {
                    Candy oneCandy = t_candy[choix];//stock l'objet de l'indez du tableau d'objet Candy dasn L,objet One_candy
                    return oneCandy;//returne l'objet One_candy
                }

                public static decimal GetCoin(decimal price)//affichage du menu pour entrer la monnaie et la fonction retourne le montant choisis
                {

                    Console.WriteLine("[0] = Annuler ");//Menu du depot de piece
                    Console.WriteLine("[1] = 5c");
                    Console.WriteLine("[2] = 10c");
                    Console.WriteLine("[3] = 25c");
                    Console.WriteLine("[4] = 1$");
                    Console.WriteLine("[5] = 2$");
                   
                    
                    int intStr;
                    bool intResultTryParse = int.TryParse(Console.ReadLine(), out intStr);
                    int pMonnaie = 0;
                    if (intResultTryParse == true) pMonnaie = intStr;//assigne a P_monnaie le nombre entrer le TryParse est concluant
                    else GetCoin(price);//sinon rapelle la fonction pour refaire le choix
                    //Averifier bug quand on entre une vlaeur autre que numerique le if n'est pas executer donc il laisse la veleur 0 par defaut ce qui crrer le bug et pk le else n'entre pas en jeu
                    
                    decimal mEntrer = 0m;
                    switch (pMonnaie)//switch
                    {
                        case 0://affiche le Board avec le message annuler et la remise change
                            Console.Beep(5000, 1000);
                            Board.Print("ANNULÉ",0 , 0 , 0 , mDepAccu, "reprenez votre change" );//affiche le board avec Annuler et le montant remis
                            Thread.Sleep(1500);
                            Menu_Bonbon();//apelle de la fonction Menu_Bonbon(corps du code de menu)
                            
                            break;
                        case 1:
                            mEntrer = 0.05m;//associe entrer et monnaie auquel cela correspond
                            break;
                        case 2:
                            mEntrer = 0.10m;
                            break;
                        case 3:
                            mEntrer = 0.25m;
                            break;
                        case 4:
                            mEntrer = 1m;
                            break;
                        case 5:
                            mEntrer = 2m;
                            break;
                    }
                    
                    return mEntrer;//retour de la fonction GetCoin() retourne le montant entrer en decimal
                }
                
            }

        }
    