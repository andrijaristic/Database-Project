using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.DTO
{
    public class ShowVozacePerDrzavaDTO
    {
        public int IdV { get; set; }
        public string ImeV { get; set; }
        public string PrezV { get; set; }
        public int BrojTit { get; set; }
        public double GodRodj { get; set; }
        public int IdD { get; set; }
        public string NazivD { get; set; }
        public int Plasman { get; set; }
        public int DrzS { get; set; }

        public ShowVozacePerDrzavaDTO(int idV, string imeV, string prezV, int brojTit, double godRodj, int idD, string nazivD, int plasman, int drzS)
        {
            IdV = idV;
            ImeV = imeV;
            PrezV = prezV;
            BrojTit = brojTit;
            GodRodj = godRodj;
            IdD = idD;
            NazivD = nazivD;
            Plasman = plasman;
            DrzS = drzS;
        }

        public static string GetFormatedHeader()
        {
            return string.Format("{0,-8} {1,-12} {2, -12} {3, -15} {4, -15}", "IDV", "Ime", "Prezime", "Broj titula", "Godina rodjenja");
        }

        public override string ToString()
        {
            return string.Format("{0,-8} {1,-12} {2, -12} {3, -15} {4, -10}", IdV, ImeV, PrezV, BrojTit, GodRodj);
        }
    }
}
