using System.Runtime.CompilerServices;

namespace AdventOfCode2024Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // recupérer la liste des éléments depuis le fichier input
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "input.txt");
            int safeNumber = 0;            
            int safeDampenedNumber = 0;

            // placer chaque ligne dans un tableau, ou chaque élément du niveau et une case du tableau
            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();
                while (line != null) // boucler pour vérifier le safe ou unsafe de la ligne
                {
                    int[] numbers = line.Split(' ')
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(s => int.Parse(s))
                        .ToArray();                    

                    bool isSafe = true;
                    bool isIncreasing = numbers[1] > numbers[0];

                    for (int i = 1; i < numbers.Length; i++)
                    {
                        int diff = numbers[i] - numbers[i - 1];

                        // rappel : les éléments doivent soit augmenter soit diminuer, avec un écart de valeur de minimum 1 et maximum 3
                        if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                        {  
                            isSafe = false;
                            break;
                        }
                        
                        // ici on vérifie que la suite est forcément progressive ou forcément degressive
                        if ((isIncreasing && diff < 0) || (!isIncreasing && diff > 0))
                        {
                            isSafe = false;
                            break;
                        }                        
                    }


                    // a chaque fois que isSafe sera true, le nombre final sera incrémenté.
                    if (isSafe)
                    {
                        safeNumber++;
                        safeDampenedNumber++; // les niveau safe seront toujours ajouté au resultat dampened, de base
                    }
                    else // puis on rajoute les autre niveaux qui deviennent safe si ils sont dampened
                    {
                        bool isDampenedSafe = false;

                        for (int i = 0; i < numbers.Length; i++)
                        {
                            List<int> subList = new List<int>();
                            for (int j =  0; j < numbers.Length; j++)
                            {
                                if (j != i)
                                {
                                    subList.Add(numbers[j]);
                                }
                            }

                            bool isSubIncreasing = subList[1] > subList[0];
                            bool isSubSafe = true; // on retrouve la meme logique qu'au début car les vérifications sont les memes

                            for (int k = 1; k < subList.Count; k++) // on boucle sur la nouvelle liste qui contient les résultats restants
                            {
                                int subDiff = subList[k] - subList[k - 1];
                                
                                if (Math.Abs(subDiff) < 1 || Math.Abs(subDiff) > 3)
                                {
                                    isSubSafe = false;
                                    break;
                                }

                                if ((isSubIncreasing && subDiff < 0) || (!isSubIncreasing && subDiff > 0))
                                {
                                    isSubSafe = false;
                                    break;
                                }
                            }

                            if (isSubSafe) // si une sous liste est ok, elle passe le isDampenedSafe à true, qui permettra d'incrémenter le resultat final
                            {
                                isDampenedSafe = true;
                                break;
                            }
                        }

                        if (isDampenedSafe)
                        {
                            safeDampenedNumber++;
                        }
                    }


                    line = sr.ReadLine();
                }
            }          
            Console.WriteLine(safeNumber.ToString());
            Console.WriteLine(safeDampenedNumber.ToString());
        }
    }
}
