using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.Model
{
    public class Staza
    {
        public int IdS { get; set; }            // ID Staze.
        public string NazivS { get; set; }      // Naziv staze.
        public int BrojKruga { get; set; }      // Broj krugova na stazi.
        public double DuzKruga { get; set; }     // Duzina kruga.
        public int DrzS { get; set; }           // Drzava.

        public Staza(int idS, string nazivS, int brojKruga, double duzKruga, int drzS)
        {
            IdS = idS;
            NazivS = nazivS;
            BrojKruga = brojKruga;
            DuzKruga = duzKruga;
            DrzS = drzS;
        }


        public static string GetFormattedHeader()
        {
            return string.Format("{0,-6} {1,-35} {2,-20} {3,-35} {4,-30}",
                "IDS", "NAZIV STAZE", "BROJ KRUGA", "DUZINA KRUGA", "DRZAVA STAZE");
        }

        public override string ToString()
        {
            return string.Format("{0,-6} {1,-35} {2,-20} {3,-35} {4,-30}",
                IdS, NazivS, BrojKruga, DuzKruga, DrzS);
        }

        public override bool Equals(object obj)
        {
            return obj is Staza staza &&
                   IdS == staza.IdS &&
                   NazivS == staza.NazivS &&
                   BrojKruga == staza.BrojKruga &&
                   DuzKruga == staza.DuzKruga &&
                   DrzS == staza.DrzS;
        }

        public override int GetHashCode()
        {
            int hashCode = 885922283;
            hashCode = hashCode * -1521134295 + IdS.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NazivS);
            hashCode = hashCode * -1521134295 + BrojKruga.GetHashCode();
            hashCode = hashCode * -1521134295 + DuzKruga.GetHashCode();
            hashCode = hashCode * -1521134295 + DrzS.GetHashCode();
            return hashCode;
        }

        /* Moramo override metode kako bi mogli da svaki atribut uporedjivali sa ovom metodom.
        public override bool Equals(object obj)
        {
            var staza = obj as Staza;
            return staza != null
                && IdS == staza.IdS
                && NazivS == staza.NazivS
                && BrojKruga == staza.BrojKruga
                && DuzKruga == staza.DuzKruga
                && DrzS == staza.DrzS;
        }*/
    }
}
