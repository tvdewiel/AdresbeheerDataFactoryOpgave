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
    public class AdresRepositoryADO : IAdresRepository
    {
        private string connectionString;

        public AdresRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool HeeftAdres(int straatId,string huisnummer,string busnummer,string appartementnummer)
        {
            string query = "SELECT count(*) FROM dbo.adres WHERE straatId=@straatId AND "
                +"huisnummer=@huisnummer AND busnummer=@busnummer AND appartementnummer=@appartementnummer";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@straatId",straatId);
                    command.Parameters.AddWithValue("@huisnummer",huisnummer);
                    command.Parameters.AddWithValue("@busnummer",busnummer);
                    command.Parameters.AddWithValue("@appartementnummer",appartementnummer);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    AdresRepositoryException dbex = new AdresRepositoryException("HeeftAdres niet gelukt", ex);
                    dbex.Data.Add("straatid",straatId);
                    dbex.Data.Add("huisnummer", huisnummer);
                    dbex.Data.Add("busnummer", busnummer);
                    dbex.Data.Add("appartementnummer", appartementnummer);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftAdres(int id)
        {
            string query = "SELECT count(*) FROM dbo.adres WHERE Id=@Id";
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
                    AdresRepositoryException dbex = new AdresRepositoryException("HeeftAdres niet gelukt", ex);
                    dbex.Data.Add("id", id);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public Adres GeefAdres(int id)
        {
            string query = "SELECT t1.*,t2.straatnaam,t2.NIScode,t3.gemeentenaam FROM dbo.adres t1 "
                + " INNER JOIN dbo.straat t2 on t1.straatid=t2.id "
                + " INNER JOIN dbo.gemeente t3 on t3.NIScode=t2.NIScode WHERE t1.id=@id";
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
                    Straat s = new Straat((int)dataReader["straatID"], (string)dataReader["straatnaam"], g);
                    Adreslocatie l = new Adreslocatie((double)dataReader["xcoord"], (double)dataReader["ycoord"]);
                    Adres a = new Adres(id, s, (string)dataReader["huisnummer"], (string)dataReader["appartementnummer"]
                        , (string)dataReader["busnummer"], (int)dataReader["postcode"],l);
                    dataReader.Close();
                    return a;
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
        public IEnumerable<Adres> GeefAdressenStraat(int straatid)
        {
            string query = "SELECT t1.*,t2.straatnaam,t2.NIScode,t3.gemeentenaam FROM dbo.adres t1 "
                + " INNER JOIN dbo.straat t2 on t1.straatid=t2.id "
                + " INNER JOIN dbo.gemeente t3 on t3.NIScode=t2.NIScode WHERE t1.straatid=@straatid";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    List<Adres> adressen = new List<Adres>();
                    Gemeente g = null;
                    Straat s = null;
                    conn.Open();
                    command.Parameters.AddWithValue("@straatid", straatid);
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (g == null) g = new Gemeente((int)dataReader["NIScode"], (string)dataReader["gemeentenaam"]);
                        if (s == null) s = new Straat(straatid, (string)dataReader["straatnaam"], g);
                        Adreslocatie l = new Adreslocatie((double)dataReader["xcoord"], (double)dataReader["ycoord"]);
                        string appnr;
                        string busnr;
                        if (dataReader.IsDBNull(dataReader.GetOrdinal("appartementnummer")))
                            appnr = null;
                        else appnr = (string)dataReader["appartementnummer"];
                        if (dataReader.IsDBNull(dataReader.GetOrdinal("busnummer")))
                            busnr = null;
                        else busnr = (string)dataReader["busnummer"];
                        Adres a = new Adres((int)dataReader["id"], s, (string)dataReader["huisnummer"], appnr
                            , busnr, (int)dataReader["postcode"], l);
                        adressen.Add(a);
                    }
                    dataReader.Close();
                    return adressen;
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
        public void UpdateAdres(Adres adres)
        {
            string query = "UPDATE dbo.adres SET huisnummer=@huisnummer,appartementnummer=@appartementnummer"
                +",busnummer=@busnummer,xcoord=@xcoord,ycoord=@ycoord,postcode=@postcode WHERE id=@id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@huisnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@appartementnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@busnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@xcoord", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@ycoord", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));
                    command.Parameters["@id"].Value = adres.ID;
                    command.Parameters["@huisnummer"].Value = adres.Huisnummer;
                    command.Parameters["@appartementnummer"].Value = adres.Appartementnummer;
                    command.Parameters["@busnummer"].Value = adres.Busnummer;
                    command.Parameters["@xcoord"].Value = adres.Locatie.X;
                    command.Parameters["@ycoord"].Value = adres.Locatie.Y;
                    command.Parameters["@postcode"].Value = adres.Postcode;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    GemeenteRepositoryException dbex = new GemeenteRepositoryException("UpdateAdres niet gelukt", ex);
                    dbex.Data.Add("Adres", adres);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VerwijderAdres(int id)
        {
            string query = "DELETE FROM dbo.adres WHERE id=@id";
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
                    AdresRepositoryException dbex = new AdresRepositoryException("VerwijderAdres niet gelukt", ex);
                    dbex.Data.Add("id", id);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public Adres VoegAdresToe(Adres adres)
        {
            string query = "INSERT INTO dbo.adres (straatId,huisnummer,appartementnummer,busnummer,xcoord,ycoord,postcode) "
                + " output INSERTED.ID VALUES(@straatId,@huisnummer,@appartementnummer,@busnummer,@xcoord,@ycoord,@postcode)";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@straatId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@huisnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@appartementnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@busnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@xcoord", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@ycoord", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));
                    command.Parameters["@straatId"].Value = adres.Straat.ID;
                    command.Parameters["@huisnummer"].Value = adres.Huisnummer;
                    command.Parameters["@appartementnummer"].Value = adres.Appartementnummer;
                    command.Parameters["@busnummer"].Value = adres.Busnummer;
                    command.Parameters["@xcoord"].Value = adres.Locatie.X;
                    command.Parameters["@ycoord"].Value = adres.Locatie.Y;
                    command.Parameters["@postcode"].Value = adres.Postcode;
                    int newId = (int)command.ExecuteScalar();
                    adres.ZetID(newId);
                    return adres;
                }
                catch (Exception ex)
                {
                    AdresRepositoryException dbex = new AdresRepositoryException("VoegAdresToe niet gelukt", ex);
                    dbex.Data.Add("adres", adres);
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
