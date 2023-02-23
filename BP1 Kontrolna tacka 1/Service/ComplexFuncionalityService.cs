using BP1_Kontrolna_tacka_1.DAO;
using BP1_Kontrolna_tacka_1.DAO.Impl;
using BP1_Kontrolna_tacka_1.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.Service
{
    public class ComplexFuncionalityService
    {
        private static readonly IDrzavaDAO drzavaDAO = new DrzavaDAOImpl();
        private static readonly IRezultatDAO rezultatDAO = new RezultatDAOImpl();
        private static readonly IStazaDAO stazaDAO = new StazaDAOImpl();
        private static readonly IVozacDAO vozacDAO = new VozacDAOImpl();

        // Stavka 3.
        public List<ShowVozacByID_DTO> GetVozacResultsByID()
        {
            List<ShowVozacByID_DTO> result = new List<ShowVozacByID_DTO>();

            foreach (ShowVozacByID_DTO resultDTO in rezultatDAO.FindVozacResultsByID())
            {
                result.Add(resultDTO);  
            }

            return result;
        }

        // Stavka 4.
        public List<ShowVozacePerDrzavaDTO> GetVozacePerDrzava()
        {
            List<ShowVozacePerDrzavaDTO> result = new List<ShowVozacePerDrzavaDTO>();

            foreach (ShowVozacePerDrzavaDTO resultDTO in vozacDAO.FindVozaceByDrzava())
            {
                result.Add(resultDTO);
            }

            return result;
        }

        // Stavka 5.
        public void UpdateRezultat()
        {
            rezultatDAO.CanUpdate();
        }
    }
}
