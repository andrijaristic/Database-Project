using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.UIHandler
{
    public class MainUIHandler
    {
        private readonly StazaUIHandler stazaUIHandler = new StazaUIHandler();
        private readonly ComplexQueryUIHandler complexQueryUIHandler = new ComplexQueryUIHandler();

        public void HandleMainMenu()
        {
            string answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju:");
                Console.WriteLine("1 - Rukovanje stazama");
                Console.WriteLine("2 - Kompleksni upiti");
                Console.WriteLine("X - Izlazak iz programa");

                Console.Write("\nOdaberite opciju: ");
                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        stazaUIHandler.HandleStazaMenu();
                        break;
                    case "2":
                        complexQueryUIHandler.HandleComplexQueryMenu();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }
    }
}
