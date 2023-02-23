using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.Model
{
    public class Vozac
    {
        public int IdV { get; set; }            // ID Vozaca.
        public string ImeV { get; set; }        // Ime vozaca.
        public string PrezV { get; set; }       // Prezime vozaca.
        public double GodRodj { get; set; }     // Godina rodjenja vozaca.
        public int BrojTit { get; set; }        // Broj titule vozaca. (Koja po redu.)
        public int DrzV { get; set; }           // Drzava vozaca. (Odakle je.)

        public Vozac(int idV, string imeV, string prezV, double godRodj, int brojTit, int drzV)
        {
            IdV = idV;
            ImeV = imeV;
            PrezV = prezV;
            GodRodj = godRodj;
            BrojTit = brojTit;
            DrzV = drzV;
        }

        public override bool Equals(object obj)
        {
            return obj is Vozac vozac &&
                   IdV == vozac.IdV &&
                   ImeV == vozac.ImeV &&
                   PrezV == vozac.PrezV &&
                   GodRodj == vozac.GodRodj &&
                   BrojTit == vozac.BrojTit &&
                   DrzV == vozac.DrzV;
        }

        public override int GetHashCode()
        {
            int hashCode = 594837380;
            hashCode = hashCode * -1521134295 + IdV.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ImeV);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PrezV);
            hashCode = hashCode * -1521134295 + GodRodj.GetHashCode();
            hashCode = hashCode * -1521134295 + BrojTit.GetHashCode();
            hashCode = hashCode * -1521134295 + DrzV.GetHashCode();
            return hashCode;
        }

        /*
        public override bool Equals(object obj)
        {
            var vozac = obj as Vozac;
            return vozac != null
                && IdV == vozac.IdV
                && ImeV == vozac.ImeV
                && PrezV == vozac.PrezV
                && GodRodj == vozac.GodRodj
                && BrojTit == vozac.BrojTit
                && DrzV == vozac.DrzV;
        }*/
    }
}
