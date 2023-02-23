using BP1_Kontrolna_tacka_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.DTO
{
    public class ShowRezultatForUpdatingDTO
    {
        public int IdV { get; set; }
        public int IdS { get; set; }
        public string IdR { get; set; }
        public int BrojTit { get; set; }

        public ShowRezultatForUpdatingDTO() {}

        public ShowRezultatForUpdatingDTO(int idV, int idS, string idR, int brojTit)
        {
            IdV = idV;
            IdS = idS;
            IdR = idR;
            BrojTit = brojTit;
        }

        public override bool Equals(object obj)
        {
            return obj is ShowRezultatForUpdatingDTO dTO &&
                   IdV == dTO.IdV &&
                   IdS == dTO.IdS &&
                   IdR == dTO.IdR &&
                   BrojTit == dTO.BrojTit;
        }

        public override int GetHashCode()
        {
            int hashCode = -316747210;
            hashCode = hashCode * -1521134295 + IdV.GetHashCode();
            hashCode = hashCode * -1521134295 + IdS.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IdR);
            hashCode = hashCode * -1521134295 + BrojTit.GetHashCode();
            return hashCode;
        }
    }
}
