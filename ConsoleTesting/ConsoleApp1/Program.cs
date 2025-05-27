using Controller;
using Model;
using System;
using System.Linq;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            ChestionarController controller = new ChestionarController();

            while (!controller.EsteTerminat())
            {
                Intrebare intrebare = controller.GetIntrebareCurenta();

                Console.WriteLine("Întrebare:");
                Console.WriteLine(intrebare.Text);

                for (int i = 0; i < intrebare.Variante.Length; i++)
                {
                    Console.WriteLine($"{i}. {intrebare.Variante[i]}");
                }

                Console.Write("Răspuns (ex: 0 sau 0,2): ");
                string input = Console.ReadLine();

                int[] varianteAlese;
                try
                {
                    varianteAlese = input
                        .Split(',')
                        .Select(x => int.Parse(x.Trim()))
                        .ToArray();
                }
                catch
                {
                    Console.WriteLine("Răspuns invalid. Întrebarea va fi trecută automat.");
                    varianteAlese = Array.Empty<int>();
                }

                controller.TrimiteRaspuns(varianteAlese);
                Console.WriteLine();
            }

            Console.WriteLine($"Chestionar terminat! Scor final: {controller.GetScor()}");
            Console.WriteLine("Apasă o tastă pentru a ieși...");
            Console.ReadKey();
        }
    }
}
