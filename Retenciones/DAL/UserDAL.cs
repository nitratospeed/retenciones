using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Retenciones.DAL
{
    public class UserDAL
    {
        public DataTable ListUsers()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_list_users", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        sql.Open();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("DAL:" + e.Message);
            }
        }
    }
}