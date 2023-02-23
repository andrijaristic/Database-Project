using BP1_Kontrolna_tacka_1.Connection;
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
    public class StazaDAOImpl : IStazaDAO
    {
        public int Count()
        {
            string query = "SELECT count(*) FROM staza";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            
        }

        public int Delete(Staza entity)
        {
            return DeleteById(entity.IdS);
        }

        public int DeleteAll()
        {
            string query = "DELETE FROM staza";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int DeleteById(int ids)
        {
            string query = "DELETE FROM staza WHERE ids=:ids";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "ids", ids);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public bool ExistsById(int ids)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return ExistsById(ids, connection);
            }
        }

        private bool ExistsById(int ids, IDbConnection connection)
        {
            string query = "SELECT * FROM staza WHERE ids=:ids";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "ids", ids);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Staza> FindAll()
        {
            string query = "SELECT ids, nazivs, brojkrug, duzkrug, drzs FROM staza";
            List<Staza> stazaList = new List<Staza>();

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
                            /*
                                public int IdS { get; set; }            // ID Staze.
                                public string NazivS { get; set; }      // Naziv staze.
                                public int BrojKruga { get; set; }      // Broj krugova na stazi.
                                public float DuzKruga { get; set; }     // Duzina kruga.
                                public int DrzS { get; set; }           // Drzava.
                             
                             */

                            Staza staza = new Staza(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetFloat(3), reader.GetInt32(4));
                            stazaList.Add(staza);
                        }
                    }
                }
            }

            return stazaList;
        }

        public IEnumerable<Staza> FindAllById(IEnumerable<int> ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ids, nazivs, brojkrug, duzkrug, drzs FROM staza WHERE ids in (");
            foreach (int id in ids)
            {
                sb.Append(":id" + id + ",");
            }
            sb.Remove(sb.Length - 1, 1); // delete last ','
            sb.Append(")");

            List<Staza> stazaList = new List<Staza>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sb.ToString();
                    foreach (int id in ids)
                    {
                        ParameterUtil.AddParameter(command, "id" + id, DbType.Int32);
                    }
                    command.Prepare();

                    foreach (int id in ids)
                    {
                        ParameterUtil.SetParameterValue(command, "id" + id, id);
                    }
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Staza staza = new Staza(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetFloat(3), reader.GetInt32(4));
                            stazaList.Add(staza);
                        }
                    }
                }
            }

            return stazaList;
        }

        public Staza FindById(int ids)
        {
            string query = "select ids, nazivs, brojkrug, duzkrug, drzs " +
                        "FROM staza WHERE ids = :ids";
            Staza staza = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "ids", ids);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staza = new Staza(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetFloat(3), reader.GetInt32(4));
                        }
                    }
                }
            }

            return staza;
        }

        public int Save(Staza entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(Staza staza, IDbConnection connection)
        {
            // id_th intentionally in the last place, so that the order between commands remains the same
            string insertSql = "INSERT INTO staza (nazivs, brojkrug, duzkrug, drzs, ids) " +
                "VALUES (:nazivs, :brojkrug, :duzkrug, :drzs, :ids)";

            string updateSql = "UPDATE staza SET nazivs=:nazivs, brojkrug=:brojkrug, " +
                "duzkrug=:duzkrug, drzs=:drzs WHERE ids=:ids";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(staza.IdS, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "nazivs", DbType.String, 50);
                ParameterUtil.AddParameter(command, "brojkrug", DbType.Int32, 50);
                ParameterUtil.AddParameter(command, "duzkrug", DbType.Double, 50);
                ParameterUtil.AddParameter(command, "drzs", DbType.Int32, 50);
                ParameterUtil.AddParameter(command, "ids", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "ids", staza.IdS);
                ParameterUtil.SetParameterValue(command, "nazivs", staza.NazivS);
                ParameterUtil.SetParameterValue(command, "brojkrug", staza.BrojKruga);
                ParameterUtil.SetParameterValue(command, "duzkrug", staza.DuzKruga);
                ParameterUtil.SetParameterValue(command, "drzs", staza.DrzS);
                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Staza> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction(); // transaction start

                int numSaved = 0;

                // insert or update every theatre
                foreach (Staza entity in entities)
                {
                    // changes are visible only to current connection
                    numSaved += Save(entity, connection);
                }

                // transaction ends successfully, changes are now visible to other connections as well
                transaction.Commit();

                return numSaved;
            }
        }
    }
}
