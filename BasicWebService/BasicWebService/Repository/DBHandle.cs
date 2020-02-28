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

        public bool isExistCard(CardInfos card)
        { 
            ConnectionDB();
            CARDEntities nd = new CARDEntities();
            var existCardInfo = nd.ExistCard(card.cardNum);
            foreach (var rec in existCardInfo)
            {
                return true;
            }
            return false;

        }
    }
}