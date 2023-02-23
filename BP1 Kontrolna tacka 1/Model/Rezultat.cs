using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.Model
{
    public class Rezultat
    {
        public string IdR { get; set; }
        public int VozacIdV { get; set; }
        public int StazaIdS { get; set; }
        public double Sezona { get; set; }
        public int Plasman { get; set; }
        public int Bodovi { get; set; }
        public double MaksBrzina { get; set; }

        public Rezultat()
        {

        }

        public Rezultat(string idR, int vozacIdV, int stazaIdS, double sezona, int plasman, int bodovi, double maksBrzina)
        {
            IdR = idR;
            VozacIdV = vozacIdV;
            StazaIdS = stazaIdS;
            Sezona = sezona;
            Plasman = plasman;
            Bodovi = bodovi;
            MaksBrzina = maksBrzina;
        }

        public override bool Equals(object obj)
        {
            return obj is Rezultat rezultat &&
                   IdR == rezultat.IdR &&
                   VozacIdV == rezultat.VozacIdV &&
                   StazaIdS == rezultat.StazaIdS &&
                   Sezona == rezultat.Sezona &&
                   Plasman == rezultat.Plasman &&
                   Bodovi == rezultat.Bodovi &&
                   MaksBrzina == rezultat.MaksBrzina;
        }

        public override int GetHashCode()
        {
            int hashCode = 1166324898;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IdR);
            hashCode = hashCode * -1521134295 + VozacIdV.GetHashCode();
            hashCode = hashCode * -1521134295 + StazaIdS.GetHashCode();
            hashCode = hashCode * -1521134295 + Sezona.GetHashCode();
            hashCode = hashCode * -1521134295 + Plasman.GetHashCode();
            hashCode = hashCode * -1521134295 + Bodovi.GetHashCode();
            hashCode = hashCode * -1521134295 + MaksBrzina.GetHashCode();
            return hashCode;
        }
    }
}
