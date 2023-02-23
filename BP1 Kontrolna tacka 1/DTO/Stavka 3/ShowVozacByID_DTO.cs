using BP1_Kontrolna_tacka_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.DTO
{
    public class ShowVozacByID_DTO
    {
        public int IdV { get; set; }
        public string ImeV { get; set; }
        public string PrezV { get; set; }
        public int IdS { get; set; }
        public double Sezona { get; set; }
        public int Plasman { get; set; }
        public int Bodovi { get; set; }
        public double MaksBrzina { get; set; }

        // Za cuvanje rezultata i ispis. DORADI (Mozda cak i ne mora, posto kad kazu spisak mozda ne misle na pravu pravcijatu listu nego samo da ispis bude u vidu spiska.)
        public List<Rezultat> rezultati = new List<Rezultat>();

        public ShowVozacByID_DTO(int idV, string imeV, string prezV, int idS, double sezona, int plasman, int bodovi, double maksBrzina)
        {
            IdV = idV;
            ImeV = imeV;
            PrezV = prezV;
            IdS = idS;
            Sezona = sezona;
            Plasman = plasman;
            Bodovi = bodovi;
            MaksBrzina = maksBrzina;
        }

        public static string GetFormatedHeader()
        {
            //return string.Format("{0,-8} {1,-20} {2, -20} {3, -4} {4, -6} {5, -4} {6, -10}", "IDV", "IME", "PREZIME", "IDS", "SEZONA", "PLASMAN", "BODOVI");
            return string.Format("{0,-8} {1,-20} {2, -20} {3, -10} {4, -15}", "IDS", "SEZONA", "PLASMAN", "BODOVI", "MAKS BRZINA");
        }

        public override string ToString()
        {
            //return string.Format("{0,-8} {1,-20} {2, -20} {3, -4} {4, -12} {5, -5} {6, -10}", IdV, ImeV, PrezV, IdS, Sezona, Plasman, Bodovi);
            return string.Format("{0,-8} {1,-20} {2, -20} {3, -10} {4, -10}", IdS, Sezona, Plasman, Bodovi, MaksBrzina);
        }
    }
}
