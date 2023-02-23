using BP1_Kontrolna_tacka_1.Connection;
using BP1_Kontrolna_tacka_1.DTO;
using BP1_Kontrolna_tacka_1.Model;
using BP1_Kontrolna_tacka_1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP1_Kontrolna_tacka_1.DAO.Impl
{
    public class VozacDAOImpl : IVozacDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Vozac entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vozac> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vozac> FindAllById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Vozac FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBrojTitula(int idv, int plasman, IDbConnection connection)
        {
            string query = "SELECT brojtit \n"
                         + "FROM vozac \n"
                         + "WHERE idv = :idv";


            ShowRezultatForUpdatingDTO rezultat = new ShowRezultatForUpdatingDTO();

            //using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            //{
                //connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    ParameterUtil.AddParameter(command, "idv", DbType.Int32);
                    ParameterUtil.SetParameterValue(command, "idv", idv);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rezultat.BrojTit = reader.GetInt32(0);
                            break;
                        }
                    }
                }
            //}
            UpdateBrojTitulaDva(idv, rezultat.BrojTit, plasman, connection, rezultat);
        }

        public int UpdateBrojTitulaDva(int idv, int brojtit, int plasman, IDbConnection connection, ShowRezultatForUpdatingDTO rezultat)
        {
            string updateSQL = "UPDATE vozac SET brojtit=:brojtit WHERE idv = :IDVSET";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = updateSQL;
                command.Prepare();
                //IDbTransaction transaction = connection.BeginTransaction();

                int BrojTitula = brojtit;

                if (plasman == 1) { BrojTitula++; }

                ParameterUtil.AddParameter(command, "brojtit", DbType.Int32);
                ParameterUtil.SetParameterValue(command, "brojtit", BrojTitula);

                ParameterUtil.AddParameter(command, "IDVSET", DbType.Int32);
                ParameterUtil.SetParameterValue(command, "IDVSET", idv);
                //command.Transaction.Commit();
                return command.ExecuteNonQuery();
            }
        }

        // 4. Stavka
        public List<ShowVozacePerDrzavaDTO> FindVozaceByDrzava()
        {
            string query = "SELECT V.idv, V.imev , V.prezv, V.brojtit, V.godrodj, D.idd, D.nazivd, R.plasman, S.drzs \n"
                         + "FROM vozac V, drzava D, rezultat R, staza S \n"
                         + "WHERE V.drzv = D.idd AND R.idv = V.idv AND R.ids = S.ids \n"
                         + "ORDER BY V.idv ASC \n";

            List<ShowVozacePerDrzavaDTO> result = new List<ShowVozacePerDrzavaDTO>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // public int IdV { get; set; }
                            // public string ImeV { get; set; }
                            // public string PrezV { get; set; }
                            // public int BrojTit { get; set; }
                            // public double GodRodj { get; set; }
                            // public int IdD { get; set; }
                            // public string NazivD { get; set; }
                            // public int Plasman { get; set; }
                            // public int DrzS { get; set; }
                            ShowVozacePerDrzavaDTO vozac = new ShowVozacePerDrzavaDTO(reader.GetInt32(0),
                                                                                      reader.GetString(1),
                                                                                      reader.GetString(2),
                                                                                      reader.GetInt32(3),
                                                                                      reader.GetDouble(4),
                                                                                      reader.GetInt32(5),
                                                                                      reader.GetString(6),
                                                                                      reader.GetInt32(7),
                                                                                      reader.GetInt32(8));

                            result.Add(vozac);
                        }
                    }
                }
            }

            return result;
        }

        public int Save(Vozac entity)
        {
            throw new NotImplementedException();
        }

        public int SaveAll(IEnumerable<Vozac> entities)
        {
            throw new NotImplementedException();
        }
    }
}
