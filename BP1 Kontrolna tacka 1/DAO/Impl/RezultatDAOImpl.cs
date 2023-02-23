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
    public class RezultatDAOImpl : IRezultatDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Rezultat entity)
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

        public IEnumerable<Rezultat> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rezultat> FindAllById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Rezultat FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ShowVozacByID_DTO> FindVozacResultsByID()
        {
            string query = "SELECT V.idv, V.imev , V.prezv, R.ids, R.sezona, R.plasman, R.bodovi, R.maksbrzina \n"
                         + "FROM vozac V, rezultat R \n"
                         + "WHERE V.idv = :idv AND V.idv = R.idv \n"
                         + "ORDER BY V.idv ASC \n";

            List<ShowVozacByID_DTO> result = new List<ShowVozacByID_DTO>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idv", DbType.Int32, 0);

                    command.Prepare();

                    Console.Write("Unesite IDV: ");
                    ParameterUtil.SetParameterValue(command, "idv", Console.ReadLine());

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // public int IdV { get; set; }
                            // public string ImeV { get; set; }
                            // public string PrezV { get; set; }
                            // public int IdS { get; set; }
                            // public double Sezona { get; set; }
                            // public int Plasman { get; set; }
                            // public int Bodovi { get; set; }
                            // public double MaksBrzina { get; set; }
                            ShowVozacByID_DTO vozac = new ShowVozacByID_DTO(reader.GetInt32(0), 
                                                                            reader.GetString(1), 
                                                                            reader.GetString(2), 
                                                                            reader.GetInt32(3), 
                                                                            reader.GetDouble(4),
                                                                            reader.GetInt32(5),
                                                                            reader.GetInt32(6),
                                                                            reader.GetDouble(7));

                            result.Add(vozac);
                        }
                    }
                }
            }

            return result;
        }

        // Metoda da proverimo da li mozemo da uopste dodamo u tabelu. (Da li su uneti IDV i IDS postojeci)
        public int CanUpdate()
        {
            string query = "SELECT R.idv, R.ids, R.idr, V.brojtit"
                        +  "FROM rezultat R, vozac V"
                        +  "WHERE R.idv = :idv AND R.ids = :ids"
                        +  "ORDER BY R.idv ASC";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                //IDbTransaction transaction = connection.BeginTransaction();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idv", DbType.Int32, 0);
                    ParameterUtil.AddParameter(command, "ids", DbType.Int32, 0);

                    command.Prepare();

                    Console.Write("Unesite IDV: ");
                    int idv = int.Parse(Console.ReadLine());
                    ParameterUtil.SetParameterValue(command, "idv", idv);


                    Console.Write("Unesite IDS: ");
                    int ids = int.Parse(Console.ReadLine());
                    ParameterUtil.SetParameterValue(command, "ids", ids);

                    int MaxId = FindMaxId(connection) + 1;
                    bool titloviUpdate = false;

                    Rezultat rezultat = CreateNewRezultat(idv, ids);
                    if (rezultat.Plasman.Equals(1)) { titloviUpdate = true; }

                    rezultat.IdR = $"R{MaxId}";
                    Save(rezultat, connection, titloviUpdate);
                }

                //transaction.Commit();
            }
            return 1;
        }

        private int FindMaxId(IDbConnection connection)
        {
            string query = "SELECT max(idr) FROM rezultat";
            ShowRezultatForUpdatingDTO rez = new ShowRezultatForUpdatingDTO();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Prepare();

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rez.IdR = reader.GetString(0);
                        break;
                    }

                    double Desetica = char.GetNumericValue(rez.IdR[1]);
                    double Jedinica = char.GetNumericValue(rez.IdR[2]);
                    int Indeks = (int)Desetica * 10 + (int)Jedinica;

                    return Indeks;
                }
            }

        }

        public Rezultat CreateNewRezultat(int idv, int ids)
        {
            Rezultat NewRezultat = new Rezultat();

            NewRezultat.VozacIdV = idv;
            NewRezultat.StazaIdS = ids;

            Console.Write("SEZONA: ");
            NewRezultat.Sezona = double.Parse(Console.ReadLine());

            Console.Write("PLASMAN: ");
            NewRezultat.Plasman = int.Parse(Console.ReadLine());

            Console.Write("BODOVI: ");
            NewRezultat.Bodovi = int.Parse(Console.ReadLine());

            Console.Write("MAKSBRZINA: ");
            NewRezultat.MaksBrzina = double.Parse(Console.ReadLine());

            return NewRezultat;
        }


        // 5. Stavka -> Deo za snimanje. Posebna funkcija za QUERY da preuzmemo informacije za prvu proveru.
        public int Save(Rezultat entity, IDbConnection connection, bool titloviUpdate)
        {
            // Ubacivanje u Rezultat tabelu.
            string insertSQL = "INSERT into rezultat (idr, idv, ids, sezona, plasman, bodovi, maksbrzina)" 
                                           + "values (:idr, :idv, :ids, :sezona, :plasman, :bodovi, :maksbrzina)";

            // Update ako je plasman 1
            IVozacDAO vozac = new VozacDAOImpl();

            using (IDbCommand command = connection.CreateCommand())
            {
                
                command.CommandText = insertSQL;
                ParameterUtil.AddParameter(command, "idr", DbType.String);
                ParameterUtil.AddParameter(command, "idv", DbType.Int32);
                ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                ParameterUtil.AddParameter(command, "sezona", DbType.String);
                ParameterUtil.AddParameter(command, "plasman", DbType.Int32);
                ParameterUtil.AddParameter(command, "bodovi", DbType.Int32);
                ParameterUtil.AddParameter(command, "maksbrzina", DbType.Double);
                command.Prepare();

                ParameterUtil.SetParameterValue(command, "idr", entity.IdR);
                ParameterUtil.SetParameterValue(command, "idv", entity.VozacIdV);
                ParameterUtil.SetParameterValue(command, "ids", entity.StazaIdS);
                ParameterUtil.SetParameterValue(command, "sezona", entity.Sezona);
                ParameterUtil.SetParameterValue(command, "plasman", entity.Plasman);
                ParameterUtil.SetParameterValue(command, "bodovi", entity.Bodovi);
                ParameterUtil.SetParameterValue(command, "maksbrzina", entity.MaksBrzina);

                vozac.UpdateBrojTitula(entity.VozacIdV, entity.Plasman, connection);
                return command.ExecuteNonQuery();
            }

        }

        public int SaveAll(IEnumerable<Rezultat> entities)
        {
            throw new NotImplementedException();
        }

        public int Save(Rezultat entity)
        {
            throw new NotImplementedException();
        }
    }
}
