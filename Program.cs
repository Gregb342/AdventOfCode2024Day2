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
                        
                        // ici on vérifie ue la suite est forcément progressive ou forcément degressive
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
                    }

                    line = sr.ReadLine();
                }
            }          
            Console.WriteLine(safeNumber.ToString());
        }
    }
}
