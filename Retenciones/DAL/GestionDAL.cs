using Retenciones.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Retenciones.DAL
{
    public class GestionDAL
    {
        public DataTable ExportarRetenciones(DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_exportar_retenciones", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@fechainicio", FechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechafin", FechaFin));
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

        public DataTable Avance(string usuario)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_avance", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
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

        public DataSet Dashboard()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_dashboard1", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //DataTable dt = new DataTable();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        sql.Open();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("DAL:" + e.Message);
            }
        }

        public DataSet Dashboard2(DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_dashboard2", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@fechainicio", FechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@fechafin", FechaFin));
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        sql.Open();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("DAL:" + e.Message);
            }
        }

        public DataSet GetClienteTablas(string cli_id)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getclientetablas", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@cli_id", cli_id));
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        sql.Open();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("DAL:" + e.Message);
            }
        }

        public int ImportEdificio(Edificio edificio)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ImportEdificio", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Direccion", edificio.Direccion));
                        cmd.Parameters.Add(new SqlParameter("@Nodo", edificio.Nodo));
                        cmd.Parameters.Add(new SqlParameter("@EdificioCodigo", edificio.EdificioCodigo));
                        cmd.Parameters.Add(new SqlParameter("@Distrito", edificio.Distrito));
                        cmd.Parameters.Add(new SqlParameter("@FechaActivacion", edificio.FechaActivacion));
                        cmd.Parameters.Add(new SqlParameter("@BaseId", edificio.BaseId));
                        sql.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("DAO: " + e.Message);
            }
        }
    }
}