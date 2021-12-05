using AdresbeheerADOlayer.Exceptions;
using AdresbeheerDomain.Interfaces;
using AdresbeheerDomain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerADOlayer.Repositories
{
    public class GemeenteRepositoryADO : IGemeenteRepository
    {
        private string connectionString;

        public GemeenteRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public Gemeente GeefGemeente(int NIScode)
        {
            string query = "SELECT * FROM dbo.gemeente WHERE NIScode=@NIScode";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@NIScode", NIScode);
                    IDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    Gemeente g = new Gemeente((int)dataReader["NIScode"], (string)dataReader["gemeentenaam"]);
                    dataReader.Close();
                    return g;
                }
                catch (Exception ex)
                {
                    throw new GemeenteRepositoryException("GeefGemeente niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftGemeente(int NIScode)
        {
            string query = "SELECT count(*) FROM dbo.gemeente WHERE NIScode=@NIScode";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@NIScode", NIScode);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    GemeenteRepositoryException dbex = new GemeenteRepositoryException("BestaatGemeente niet gelukt", ex);
                    dbex.Data.Add("NIScode", NIScode);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftStraten(int NIScode)
        {
            string query = "SELECT count(*) FROM dbo.straat WHERE NIScode=@NIScode";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@NIScode", NIScode);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    GemeenteRepositoryException dbex = new GemeenteRepositoryException("HeeftStraten niet gelukt", ex);
                    dbex.Data.Add("NIScode", NIScode);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateGemeente(Gemeente gemeente)
        {
            string query = "UPDATE dbo.gemeente SET gemeentenaam=@gemeentenaam WHERE NIScode=@NIScode";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@NIScode", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@gemeentenaam", SqlDbType.NVarChar));
                    command.Parameters["@NIScode"].Value = gemeente.NIScode;
                    command.Parameters["@gemeentenaam"].Value = gemeente.Gemeentenaam;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    GemeenteRepositoryException dbex = new GemeenteRepositoryException("UpdateGemeente niet gelukt", ex);
                    dbex.Data.Add("Gemeente", gemeente);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VerwijderGemeente(int NIScode)
        {
            string query = "DELETE FROM dbo.gemeente WHERE NIScode=@NIScode";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@NIScode", SqlDbType.Int));
                    command.Parameters["@NIScode"].Value = NIScode;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    GemeenteRepositoryException dbex = new GemeenteRepositoryException("VerwijderGemeente niet gelukt", ex);
                    dbex.Data.Add("NIScode", NIScode);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VoegGemeenteToe(Gemeente gemeente)
        {
            string query = "INSERT INTO dbo.gemeente (NIScode,gemeentenaam) VALUES(@NIScode,@gemeentenaam)";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@NIScode", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@gemeentenaam", SqlDbType.NVarChar));
                    command.Parameters["@NIScode"].Value = gemeente.NIScode;
                    command.Parameters["@gemeentenaam"].Value = gemeente.Gemeentenaam;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    GemeenteRepositoryException dbex = new GemeenteRepositoryException("VoegGemeenteToe niet gelukt", ex);
                    dbex.Data.Add("Gemeente", gemeente);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public List<Gemeente> GeefGemeenten()
        {
            List<Gemeente> lg = new List<Gemeente>();
            string query = "SELECT * FROM dbo.gemeente order by gemeentenaam";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int NIScode = (int)dataReader["NIScode"];
                        string gemeentenaam = (string)dataReader["gemeentenaam"];
                        lg.Add(new Gemeente(NIScode, gemeentenaam));
                    }
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    throw new GemeenteRepositoryException("LeesGemeenten niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            return lg;
        }
    }
}
