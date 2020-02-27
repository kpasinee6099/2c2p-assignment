using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using BasicWebService.Models;
using System.Data;

namespace BasicWebService.Repository
{
    public class DBHandle
    {

        SqlConnection cnn;
        string connetionString = @"Data Source = 172.31.3.27; Initial Catalog = CARD; Persist Security Info = True; User ID = sa; Password = sinaptiqPa$$";

        public void ConnectionDB()
        {
            cnn = new SqlConnection(connetionString);
            cnn.Open();
        }

        public bool isExistCard(CardInfo card)
        {
            ConnectionDB();
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("isExistCard", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@CardNum", card.cardNum));
            rdr = cmd.ExecuteReader();
            if (!rdr.HasRows)
            {
                return false;
            }
            else
            {
                return true;
            }            
        } 
    }
}