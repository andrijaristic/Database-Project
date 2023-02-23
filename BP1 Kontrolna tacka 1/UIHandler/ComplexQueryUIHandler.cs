using BP1_Kontrolna_tacka_1.DTO;
using BP1_Kontrolna_tacka_1.Service;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.UIHandler
{
    public class ComplexQueryUIHandler
    {
        private static readonly ComplexFuncionalityService complexQueryService = new ComplexFuncionalityService();

        public void HandleComplexQueryMenu()
        {
            String answer;
            do
            {
                Console.WriteLine("\nOdaberite funkcionalnost:");
                Console.WriteLine(
                        "\n3  - Implementirati izveštaj koji će za uneti identifikacioni broj vozača formule"
                             + "\n1(IDV) prikazati sve rezultate koji pripadaju tom vozaču.Nakon liste rezultata prikazati"
                             + "\nprosečnu maksimalnu brzinu tog vozača.");

                Console.WriteLine(
                        "\n4  - Implementirati izveštaj koji će za svaku državu prikazati vozače koji su iz te"
                             + "\n države; potom prikazati prosečno godište vozača te države i ukupan broj titula vozača"
                             + "\n te države. Zatim je potrebno prikazati koliko titula su vozači uzeli na stazama svoje države.");

                Console.WriteLine(
                        "\n5  - Implementirati funkciju koja će omogućiti dodavanje novog rezultata za"
                             +  "postojećeg vozača na postojećoj stazi.U slučaju da je prilikom dodavanja rezultata za"
                             +  "plasman uneto „1“ (vozač je prvi završio trku) potrebno je uvećati broj titula za jedan"
                             +  "kod vozača na kog se novouneseni rezultat odnosi.");

                Console.WriteLine("\nX  - Izlazak iz kompleksnih upita");

                Console.Write("\n\nOdabir opcije: ");
                answer = Console.ReadLine();

                switch (answer)
                {
                    case "3":
                        // 3. Stavka
                        ShowVozacResultsByID();
                        break;
                    case "4":
                        // 4. Stavka
                        ShowVozacePerDrzava();
                        break;
                    case "5":
                        // 5. Stavka
                        UpdateRezultatUIMenu();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }

        // 3. Stavka
        public void ShowVozacResultsByID()
        {
            try
            {
                List<ShowVozacByID_DTO> dtos = complexQueryService.GetVozacResultsByID();
                if (dtos.Count != 0)
                {
                    List<int> IDovi = new List<int>();

                    foreach (ShowVozacByID_DTO dto in dtos)
                    {
                        if (IDovi.Contains(dto.IdV)) { continue; }
                        else { IDovi.Add(dto.IdV); }
                    }

                    foreach (ShowVozacByID_DTO dto in dtos)
                    {
                        if (IDovi.Contains(dto.IdV))
                        {
                            Console.WriteLine($"\n{dto.ImeV} {dto.PrezV}");
                            Console.WriteLine("========================================================================");
                            IDovi.Remove(dto.IdV);

                            //double AvgMaxSpeed = 0;
                            int ResultNum = 1;
                            double TotalMaxSpeed = dto.MaksBrzina;
                            Console.WriteLine(ShowVozacByID_DTO.GetFormatedHeader());
                            Console.WriteLine(dto);

                            foreach (ShowVozacByID_DTO vozaci in dtos)
                            {
                                // IF -> Gledaj da li je ID isti a Staza razlicita.
                                if (vozaci.IdV.Equals(dto.IdV) && !vozaci.IdS.Equals(dto.IdS))
                                {
                                    ResultNum++;
                                    TotalMaxSpeed += vozaci.MaksBrzina;
                                    Console.WriteLine(ShowVozacByID_DTO.GetFormatedHeader());
                                    Console.WriteLine(vozaci);
                                } // ELSE IF -> Ako je ID i Staza ista, gledaj Sezonu trke.
                                else if (vozaci.IdV.Equals(dto.IdV) && vozaci.IdS.Equals(dto.IdS) && !vozaci.Sezona.Equals(dto.Sezona))
                                {
                                    ResultNum++;
                                    TotalMaxSpeed += vozaci.MaksBrzina;
                                    Console.WriteLine(ShowVozacByID_DTO.GetFormatedHeader());
                                    Console.WriteLine(vozaci);
                                }
                                Console.WriteLine("------------------------------------------------------------------------");
                            }

                            Console.WriteLine("\nPROSECNA MAKSIMALNA BRZINA VOZACA: {0, -8:F3}\n", TotalMaxSpeed / ResultNum);
                            Console.WriteLine();
                        }

                        /*
                        Console.WriteLine(ShowVozacByID_DTO.GetFormatedHeader());
                        Console.WriteLine(dto);
                        //double AvgMaxSpeed = 0;
                        int ResultNum = 1;
                        double TotalMaxSpeed = dto.MaksBrzina;

                        foreach (ShowVozacByID_DTO vozaci in dtos)
                        {
                            // IF -> Gledaj da li je ID isti a Staza razlicita.
                            if (vozaci.IdV.Equals(dto.IdV) && !vozaci.IdS.Equals(dto.IdS))
                            {
                                ResultNum++;
                                TotalMaxSpeed += vozaci.MaksBrzina;
                            } // ELSE IF -> Ako je ID i Staza ista, gledaj Sezonu trke.
                            else if (vozaci.IdV.Equals(dto.IdV) && vozaci.IdS.Equals(dto.IdS) && !vozaci.Sezona.Equals(dto.Sezona))
                            {
                                ResultNum++;
                                TotalMaxSpeed += vozaci.MaksBrzina;
                            }
                        }

                        Console.WriteLine("\nPROSECNA MAKSIMALNA BRZINA VOZACA: {0, -8:F3}\n", TotalMaxSpeed / ResultNum);
                        Console.WriteLine();
                        */
                    }
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 4. Stavka
        public void ShowVozacePerDrzava()
        {
            try
            {
                List<ShowVozacePerDrzavaDTO> dtos = complexQueryService.GetVozacePerDrzava();
                if (dtos.Count != 0)
                {
                    List<string> NaziviDrzava = new List<string>();
                    List<int> VozaciLista = new List<int>();

                    foreach (ShowVozacePerDrzavaDTO dto in dtos)
                    {
                        if (NaziviDrzava.Contains(dto.NazivD)) { continue; }
                        else { NaziviDrzava.Add(dto.NazivD); }
                    }


                    foreach (ShowVozacePerDrzavaDTO dto in dtos)
                    {
                        int VozacNum = 0;
                        double GodinaUkupna = 0;
                        int SumTitula = 0;
                        int TituleSvojeDrzave = 0;

                        if (NaziviDrzava.Contains(dto.NazivD))
                        {
                            Console.WriteLine($"{dto.NazivD.ToUpper()}:");
                            Console.WriteLine("========================================================================");
                            NaziviDrzava.Remove(dto.NazivD);
                            Console.WriteLine(ShowVozacePerDrzavaDTO.GetFormatedHeader());
                            Console.WriteLine("------------------------------------------------------------------------");

                            foreach (ShowVozacePerDrzavaDTO vozaci in dtos)
                            {
                                if (vozaci.IdD.Equals(dto.IdD))
                                {
                                    if (!VozaciLista.Contains(vozaci.IdV))
                                    {
                                        VozaciLista.Add(vozaci.IdV);
                                        Console.WriteLine(vozaci);

                                        SumTitula += vozaci.BrojTit;
                                        GodinaUkupna += vozaci.GodRodj;
                                        VozacNum++;

                                        foreach (ShowVozacePerDrzavaDTO vozaci2 in dtos)
                                        {
                                            if (vozaci2.IdV.Equals(vozaci.IdV))
                                            {
                                                if (vozaci2.DrzS.Equals(vozaci.IdD) && vozaci2.Plasman.Equals(1))
                                                {
                                                    TituleSvojeDrzave++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("------------------------------------------------------------------------");
                            Console.WriteLine("Procesno godiste vozaca: {0, -4:F0}; Ukupno titula: {1, -4}", GodinaUkupna/VozacNum, SumTitula);
                            Console.WriteLine($"Vozaci su uzeli {TituleSvojeDrzave} titule na stazama svoje drzave.");

                            Console.WriteLine("========================================================================\n");
                        }
                    }
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateRezultatUIMenu()
        {
            complexQueryService.UpdateRezultat();
        }
    }
}

/*
    Console.WriteLine(ShowVozacePerDrzavaDTO.GetFormatedHeader());
    Console.WriteLine(dto);
 */