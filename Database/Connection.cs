using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_ASSIGN1.Database
{
    public class Connection
    {
        static SqlConnection My_Connection = null;
        internal static SqlConnection conn()
        {
            try
            {
                if (My_Connection == null)
                {
                    //string strconn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                    My_Connection = new SqlConnection("Data Source=DESKTOP-N2MSQ1H;Initial Catalog=DucatTraining;Integrated Security=True");
                    My_Connection.Open();
                }
                if (My_Connection.State == ConnectionState.Closed)
                {
                    My_Connection.Open();
                }
                return My_Connection;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}