using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.Model
{
    public class Drzava
    {
        public int IdD { get; set; }
        public string NazivD { get; set; }

        public Drzava(int idD, string nazivD)
        {
            IdD = idD;
            NazivD = nazivD;
        }

        public override bool Equals(object obj)
        {
            return obj is Drzava drzava &&
                   IdD == drzava.IdD &&
                   NazivD == drzava.NazivD;
        }

        public override int GetHashCode()
        {
            int hashCode = 1516402195;
            hashCode = hashCode * -1521134295 + IdD.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NazivD);
            return hashCode;
        }

        /*
        public override bool Equals(object obj)
        {
            var drzava = obj as Drzava;
            return drzava != null
                && IdD == drzava.IdD
                && NazivD == drzava.NazivD;
        }*/
    }
}
