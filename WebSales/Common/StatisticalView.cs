using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using WebSales.Models;

namespace WebSales.Common
{
    public class StatisticalView
    {
        public static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public static StatisticalViewModel Statistical()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<StatisticalViewModel>("sp_Statistical", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}