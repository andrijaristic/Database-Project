using BP1_Kontrolna_tacka_1.Model;
using BP1_Kontrolna_tacka_1.Service;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.UIHandler
{
    public class StazaUIHandler
    {
        private static readonly StazaService stazaService = new StazaService();

        public void HandleStazaMenu()
        {
            string answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju za rad nad stazama:");
                Console.WriteLine("1 - Prikaz svih");
                Console.WriteLine("2 - Prikaz po identifikatoru");
                Console.WriteLine("3 - Unos jedne staze");
                Console.WriteLine("4 - Unos vise staza");
                Console.WriteLine("5 - Izmena po identifikatoru");
                Console.WriteLine("6 - Brisanje po identifikatoru");
                Console.WriteLine("X - Izlazak iz rukovanja stazama");

                Console.Write("\nOdabir opcije: ");
                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        ShowById();
                        break;
                    case "3":
                        HandleSingleInsert();
                        break;
                    case "4":
                        HandleMultipleInserts();
                        break;
                    case "5":
                        HandleUpdate();
                        break;
                    case "6":
                        HandleDelete();
                        break;

                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void ShowAll()
        {
            Console.WriteLine(Staza.GetFormattedHeader());

            try
            {
                foreach (Staza staza in stazaService.FindAll())
                {
                    Console.WriteLine(staza);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowById()
        {
            Console.Write("IDS: ");
            int ids = int.Parse(Console.ReadLine());

            try
            {
                Staza staza = stazaService.FindById(ids);

                Console.WriteLine(Staza.GetFormattedHeader());
                Console.WriteLine(staza);
            } catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void HandleSingleInsert()
        {
            Console.Write("\nIDS: ");
            int ids = int.Parse(Console.ReadLine());

            Console.Write("Naziv staze: ");
            string NazivS = Console.ReadLine();

            Console.Write("Broj krugova: "); //int
            int BrojKrugova = int.Parse(Console.ReadLine());

            Console.Write("Duz kruga: "); //float
            float DuzKruga = float.Parse(Console.ReadLine());

            Console.Write("ID Drzave: "); //int
            int IDDrzave = int.Parse(Console.ReadLine());

            try
            {
                int insert = stazaService.Save(new Staza(ids, NazivS, BrojKrugova, DuzKruga, IDDrzave));
                if (insert != 0)
                {
                    Console.WriteLine("Staza \"{0}\" je uspesno uneta.", NazivS);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void HandleUpdate()
        {
            Console.Write("\nIDS: ");
            int ids = int.Parse(Console.ReadLine());

            try
            {
                if (!stazaService.ExistsById(ids))
                {
                    Console.WriteLine("Uneta vrednost ne postoji!");
                    return;
                }

                Console.Write("Naziv staze: ");
                string NazivS = Console.ReadLine();

                Console.Write("Broj krugova: "); //int
                int BrojKrugova = int.Parse(Console.ReadLine());

                Console.Write("Duz kruga: "); //float
                float DuzKruga = float.Parse(Console.ReadLine());

                Console.Write("ID Drzave: "); //int
                int IDDrzave = int.Parse(Console.ReadLine());

                int update = stazaService.Save(new Staza(ids, NazivS, BrojKrugova, DuzKruga, IDDrzave));
                if (update != 0)
                {
                    Console.WriteLine("Staza \"{0}\" je uspesno izmenjena.", ids);
                }
            } 
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void HandleDelete()
        {
            Console.Write("\nIDS: ");
            int ids = int.Parse(Console.ReadLine());

            try
            {
                int delete = stazaService.DeleteById(ids);
                if (delete != 0)
                {
                    Console.WriteLine("Staza sa sifrom \"{0}\" je uspesno izbrisana.", ids);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void HandleMultipleInserts()
        {
            List<Staza> stazaList = new List<Staza>();
            string answer;

            do
            {
                Console.Write("\nIDS: ");
                int ids = int.Parse(Console.ReadLine());

                Console.Write("Naziv staze: ");
                string NazivS = Console.ReadLine();

                Console.Write("Broj krugova: "); //int
                int BrojKrugova = int.Parse(Console.ReadLine());

                Console.Write("Duz kruga: "); //float
                float DuzKruga = float.Parse(Console.ReadLine());

                Console.Write("ID Drzave: "); //int
                int IDDrzave = int.Parse(Console.ReadLine());

                stazaList.Add(new Staza(ids, NazivS, BrojKrugova, DuzKruga, IDDrzave));

                Console.WriteLine("Unesi jos jednu stazu? (ENTER za potvrdu, X za odustajanje)");
                answer = Console.ReadLine();
            } while (!answer.ToUpper().Equals("X"));

            try
            {
                int numInserted = stazaService.SaveAll(stazaList);
                Console.WriteLine("Uspesno uneto {0} staza!", numInserted);
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
