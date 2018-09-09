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
    public class GestionCarruselesDAL
    {
        public DataTable ExportarCarruseles(DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand comando = new SqlCommand("sp_exportar_carruseles", con);
                comando.Connection.Open();
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@fechainicio", FechaInicio);
                comando.Parameters.AddWithValue("@fechafin", FechaFin);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataTable data = new DataTable();
                adapter.Fill(data);
                comando.Connection.Close();
                return data;
            }
            catch (Exception e)
            {
                throw new Exception("DAO:" + e.Message);
            }
        }

        public int ImportCliente(List<GestionCarruseles> gestion)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ImportCliente", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProyectoAntiguo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@CodigoSGAAntiguo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@CodigoSGANuevo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@ClienteAntiguo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@ClienteNuevo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@SOTBaja", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@FechaGeneracion", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@DireccionAntiguo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@DistritoAntiguo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@DistritoNuevo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@DepartamentoAntiguo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@DepartamentoNuevo", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("@Pendiente", SqlDbType.Bit));
                        cmd.Parameters.Add(new SqlParameter("@Carrusel", SqlDbType.Bit));
                        cmd.Parameters.Add(new SqlParameter("@Baseid", SqlDbType.Int));

                        foreach (var item in gestion)
                        {
                            cmd.Parameters["@ProyectoAntiguo"].Value = item.ProyectoAntiguo;
                            cmd.Parameters["@CodigoSGAAntiguo"].Value = item.CodigoSGAAntiguo;
                            cmd.Parameters["@CodigoSGANuevo"].Value = item.CodigoSGANuevo;
                            cmd.Parameters["@ClienteAntiguo"].Value = item.ClienteAntiguo;
                            cmd.Parameters["@ClienteNuevo"].Value = item.ClienteNuevo;
                            cmd.Parameters["@SOTBaja"].Value = item.SOTBaja;
                            cmd.Parameters["@FechaGeneracion"].Value = item.FechaGeneracion;
                            cmd.Parameters["@DireccionAntiguo"].Value = item.DireccionAntiguo;
                            cmd.Parameters["@DistritoAntiguo"].Value = item.DistritoAntiguo;
                            cmd.Parameters["@DistritoNuevo"].Value = item.DistritoNuevo;
                            cmd.Parameters["@DepartamentoAntiguo"].Value = item.DepartamentoAntiguo;
                            cmd.Parameters["@DepartamentoNuevo"].Value = item.DepartamentoNuevo;
                            cmd.Parameters["@Pendiente"].Value = item.Pendiente;
                            cmd.Parameters["@Carrusel"].Value = item.Carrusel;
                            cmd.Parameters["@Baseid"].Value = item.BaseId;
                            sql.Open();
                            cmd.ExecuteNonQuery();
                            sql.Close();
                        }

                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Service: " + e.Message);
            }
        }
    }
}