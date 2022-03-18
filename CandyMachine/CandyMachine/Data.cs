using System;
using System.IO;

namespace Project
{
    public class Data
    {
      const String FILE_NAME = "candies.data"; //mot clé const pour empêche de changer la valeur de FILE_NAME => elle devient une constante
      const String DELIMITER = "|";//variable DELEMITER => constante DELIMITER 

        //fonction qui va nous permettre de charger les données des bonbons du fichier candies.data
        public Candy[] LoadCandies() {

        Candy[] candies = new Candy[0]; //déclaration et réservation de la mémoire de notre tableau avec taille tableau 0
        String line; //variable ligne de type chaine de caractère qu'on utilisera pour récupérer une ligne de candies.data
        //la classe StreamReader permet de lire un fichier de données comme dans notre cas candies.data
        //avec StreamReader on va pouvoir lire le fichier candies.data et récupérer les données y contenant
        StreamReader streamReader = GetReader(FILE_NAME); //stremReader : variable lecteur de fichier.
        //la varibale streamReader prend comme valeur le nom du fichier à lire
        while((line = streamReader.ReadLine()) != null)  //tant que la variable stremReader arrive à lire des lignes dans le fichier
        {
          //format d'une ligne dans le fichier candies.data: 1|Wacky Monkey|2|10
          String name = line.Split(DELIMITER)[1]; //on divise la ligne courante par le délimiteur | 
         //une chaine caractères qui est "splité" avec la fonction Split(délimiteur) retourne un tableau de string
         //une fois la chaine est splitée, on affecte à la variable name la valeur de la 2eme case du tableau
          decimal price = decimal.Parse(line.Split(DELIMITER)[2]);//on splite et on affecte la valeur du tableau qui a la position 2(3eme case)
          int stock = int.Parse(line.Split(DELIMITER)[3]);//idem
          
          //on a maintenant récupérer les données d'un bonbon à partir de ligne qu'on a placé dans les variables:name , price et stock
          /*---RAPPEL----
           une variable structurée on l'appelle objet
           une case de la variable structurée on l'appelle attribut
           */
          Candy newCandy = new Candy(name,price,stock);//on déclare un objet(variable structurée) de type Candy et j'itilialise l'attribut(case) name de l'objet newCandy
          //newCandy.Price = price; //on affecte la valeur de price à l'attribut newCandy.Price de l'objet newCandy
          //newCandy.Stock = stock;//on affecte la valeur de price à l'attribut newCandy.Price de l'objet newCandy
          
          //j'ajoute mon nouvel bonbon récupéré (que j'ai maintenant dans l'objet newCandy) au tableau des candies à l'aide de la fonction AddCandy()
          candies = AddCandy(candies, newCandy);//la fonction AddCandy prend en entrée le tableau candies et l'objet qui contient le bonbon à ajouter au tableau
          //la fonction AddCandy() retourne un le tableau candies après avoir ajouter le nouvel bonbon dans ce tableau
        }  
        
        //rappellez vous toutes ces instructions sont dans la fonction LoadCandies()
        //la fonction LoadCandies() a pu lire les lignes du fichier, récupérer les données des bonbons à partir du fichier et les placer dans la tableau
        return candies;//on retourne le tableau rempli

      }
        //fonction GetReader() qui permet de créer un lecteur de fichier: la fonction prend en paramètres le nom du fichier
        //une variable de type StreamReader a besoin d'une réservation de la mémoire (avec le mot clé new)
        //elle prend le chemin du fichier en paramètres
        private StreamReader GetReader(String filename) {
            return new StreamReader($@"{Directory.GetCurrentDirectory()}/{filename}");
        }

        //fonction qui permet d'ajouter un bonbon de type Candy dans le tableau de type Candy[]
        private Candy[] AddCandy(Candy[] candies, Candy candy) {
          Candy[] newCandies = new Candy[candies.Length + 1];//création d'un nouveu tableau avec une case de plus
          for (int i = 0; i < candies.Length; i++)
          {
              newCandies[i] = candies[i];//copier les données du tableau candies dans le tableau newCandies
          }
          newCandies[candies.Length] = candy; //ajouter à la dernière case ajouté de newCandies les données de candy (nouveau bonbon)
          return newCandies; //on retourne le nouveau tableau aui contient maintenant tous le tableau de départ + le nouveau bonbon
        }
    }
}