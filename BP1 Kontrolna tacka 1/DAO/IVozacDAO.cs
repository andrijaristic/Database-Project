using BP1_Kontrolna_tacka_1.DTO;
using BP1_Kontrolna_tacka_1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.DAO
{
    public interface IVozacDAO : ICRUDDao<Vozac, int>
    {
        // Metoda nabavljanja vozaca gde pamtimo i drzavu odakle su.
        List<ShowVozacePerDrzavaDTO> FindVozaceByDrzava();
        void UpdateBrojTitula(int idv, int plasman, IDbConnection connection);
        int UpdateBrojTitulaDva(int idv, int brojtit, int plasman, IDbConnection connection, ShowRezultatForUpdatingDTO rezultat);
    }
}
