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
    public class StraatRepositoryADO : IStraatRepository
    {
        private string connectionString;

        public StraatRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        //public bool HeeftStraat(int gemeenteId, string straatnaam)
        //{
        //    string query = "SELECT count(*) FROM dbo.straat WHERE NIScode=@NIScode AND straatnaam=@straatnaam";
        //    SqlConnection conn = getConnection();
        //    using (SqlCommand command = new SqlCommand(query, conn))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            command.Parameters.AddWithValue("@NIScode", gemeenteId);
        //            command.Parameters.AddWithValue("@straatnaam", straatnaam);
        //            int n = (int)command.ExecuteScalar();
        //            if (n > 0) return true;
        //            return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            StraatRepositoryException dbex = new StraatRepositoryException("HeeftStraat niet gelukt", ex);
        //            dbex.Data.Add("gemeenteid", gemeenteId);
        //            dbex.Data.Add("straatnaam", straatnaam);
        //            throw dbex;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}
        public bool HeeftStraat(int id)
        {
            string query = "SELECT count(*) FROM dbo.straat WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", id);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    StraatRepositoryException dbex = new StraatRepositoryException("HeeftStraat niet gelukt", ex);
                    dbex.Data.Add("id", id);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public Straat GeefStraat(int id)
        {
            string query = "SELECT t1.*,t2.gemeentenaam FROM dbo.straat t1 "
                +" INNER JOIN dbo.gemeente t2 on t1.NIScode=t2.NIScode WHERE t1.id=@id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@id", id);
                    IDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    Gemeente g = new Gemeente((int)dataReader["NIScode"], (string)dataReader["gemeentenaam"]);
                    Straat s = new Straat(id, (string)dataReader["straatnaam"], g);
                    dataReader.Close();
                    return s;
                }
                catch (Exception ex)
                {
                    throw new StraatRepositoryException("GeefStraat niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public List<Straat> GeefStratenGemeente(int gemeenteId)
        {
            string query = "SELECT t1.*,t2.gemeentenaam FROM dbo.straat t1 "
                + " INNER JOIN dbo.gemeente t2 on t1.NIScode=t2.NIScode WHERE t1.NIScode=@NIScode";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    List<Straat> straten = new List<Straat>();
                    conn.Open();
                    command.Parameters.AddWithValue("@NIScode",gemeenteId);
                    IDataReader dataReader = command.ExecuteReader();
                    Gemeente g = null;
                    while (dataReader.Read())
                    {
                        if (g == null) g = new Gemeente((int)dataReader["NIScode"], (string)dataReader["gemeentenaam"]);
                        Straat s = new Straat((int)dataReader["id"], (string)dataReader["straatnaam"], g);
                        straten.Add(s);
                    }
                    dataReader.Close();
                    return straten;
                }
                catch (Exception ex)
                {
                    throw new StraatRepositoryException("GeefStraten niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftAdressen(int id)
        {
            string query = "SELECT count(*) FROM dbo.adres WHERE straatId=@straatId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@straatId", id);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    StraatRepositoryException dbex = new StraatRepositoryException("HeeftAdressen niet gelukt", ex);
                    dbex.Data.Add("id", id);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateStraat(Straat straat)
        {
            string query = "UPDATE dbo.straat SET straatnaam=@straatnaam WHERE id=@id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@straatnaam", SqlDbType.NVarChar));
                    command.Parameters["@id"].Value = straat.ID;
                    command.Parameters["@straatnaam"].Value = straat.Straatnaam;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    GemeenteRepositoryException dbex = new GemeenteRepositoryException("UpdateStraat niet gelukt", ex);
                    dbex.Data.Add("straat", straat);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VerwijderStraat(int id)
        {
            string query = "DELETE FROM dbo.straat WHERE id=@id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.Parameters["@id"].Value = id;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    StraatRepositoryException dbex = new StraatRepositoryException("VerwijderStraat niet gelukt", ex);
                    dbex.Data.Add("id", id);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public Straat VoegStraatToe(Straat straat)
        {
            string query = "INSERT INTO dbo.straat (NIScode,straatnaam) output INSERTED.ID VALUES(@NIScode,@straatnaam)";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@NIScode", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@straatnaam", SqlDbType.NVarChar));
                    command.Parameters["@NIScode"].Value = straat.Gemeente.NIScode;
                    command.Parameters["@straatnaam"].Value = straat.Straatnaam;
                    int newId=(int)command.ExecuteScalar();
                    straat.ZetID(newId);
                    return straat;
                }
                catch (Exception ex)
                {
                    StraatRepositoryException dbex = new StraatRepositoryException("VoegStraatToe niet gelukt", ex);
                    dbex.Data.Add("straat", straat);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftStraat(string straatnaam, int gemeenteid)
        {
            string query = "SELECT count(*) FROM dbo.straat WHERE NIScode=@gemeenteId AND straatnaam=@straatnaam";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@gemeenteId", gemeenteid);
                    command.Parameters.AddWithValue("@straatnaam", straatnaam);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    StraatRepositoryException dbex = new StraatRepositoryException("HeeftStraat niet gelukt", ex);
                    dbex.Data.Add("gemeenteid", gemeenteid);
                    dbex.Data.Add("straatnaam", straatnaam);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
