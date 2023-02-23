using BP1_Kontrolna_tacka_1.DAO;
using BP1_Kontrolna_tacka_1.DAO.Impl;
using BP1_Kontrolna_tacka_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.Service
{
    public class StazaService
    {
        private static readonly IStazaDAO stazaDAO = new StazaDAOImpl();

        public List<Staza> FindAll()
        {
            return stazaDAO.FindAll().ToList();
        }

        public Staza FindById(int ids)
        {
            return stazaDAO.FindById(ids);
        }

        public int Save(Staza p)
        {
            return stazaDAO.Save(p);
        }

        public bool ExistsById(int ids)
        {
            return stazaDAO.ExistsById(ids);
        }

        public int DeleteById(int ids)
        {
            return stazaDAO.DeleteById(ids);
        }
        public int SaveAll(List<Staza> stazaList)
        {
            return stazaDAO.SaveAll(stazaList);
        }
    }
}
