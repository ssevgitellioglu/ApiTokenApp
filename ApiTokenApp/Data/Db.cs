using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTokenApp.Data
{
    public class Db
    {
        SqlConnection connection = new SqlConnection("Data Source=SEVGI-PC\\SQLEXPRESS; Initial Catalog=Users; Integrated Security=true");

        public DataSet Kullanicilar(Users users, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";

            try
            {

                SqlCommand sqlCommand = new SqlCommand("", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", users.id);
                sqlCommand.Parameters.AddWithValue("@ad", users.ad);
                sqlCommand.Parameters.AddWithValue("@soyad", users.soyad);
                sqlCommand.Parameters.AddWithValue("@token", users.token);
                sqlCommand.Parameters.AddWithValue("@kullaniciAdi", users.kullaniciAdi);
                sqlCommand.Parameters.AddWithValue("@sifre", users.sifre);
                SqlDataAdapter da = new SqlDataAdapter();
                msg = "OK";
                da.Fill(ds);
                return ds;

            }
            catch (Exception hata)
            {
                msg = hata.Message;
                return ds;
            }
        }

    

        public string Kullanici(Users users, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";

            try
            {

                SqlCommand sqlCommand = new SqlCommand("", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", users.id);
                sqlCommand.Parameters.AddWithValue("@ad", users.ad);
                sqlCommand.Parameters.AddWithValue("@soyad", users.soyad);
                sqlCommand.Parameters.AddWithValue("@token", users.token);
                sqlCommand.Parameters.AddWithValue("@kullaniciAdi", users.kullaniciAdi);
                sqlCommand.Parameters.AddWithValue("@sifre", users.sifre);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                msg = "OK";
                return msg;
                

            }
            catch (Exception hata)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                msg = hata.Message;
                return msg;
            }
        }
    }
}

        
    
