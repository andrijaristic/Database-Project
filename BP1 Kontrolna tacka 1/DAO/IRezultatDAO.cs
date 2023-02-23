using BP1_Kontrolna_tacka_1.DTO;
using BP1_Kontrolna_tacka_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.DAO
{
    public interface IRezultatDAO : ICRUDDao<Rezultat, int> 
    {
        List<ShowVozacByID_DTO> FindVozacResultsByID();
        int CanUpdate();
    }
}
