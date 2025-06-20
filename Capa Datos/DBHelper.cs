using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.CodeDom;

namespace Capa_Datos
{
    public class DBHelper
    {
        static string cad_cn =
            ConfigurationManager.ConnectionStrings["cn1"]
            .ConnectionString;


        private static void LlenarParametros(
            SqlCommand cmd, params object[] valores)
        {
            int indice = 0;

            //descubrir y crear los parametros que esta usando
            // en el Sqlcommand
            SqlCommandBuilder.DeriveParameters(cmd);

            //recorrer los parametros
            foreach (SqlParameter p in cmd.Parameters)
            {
                // si el nombre del parámetro es diferente a
                // @RETURN_VALUE
                if (p.ParameterName != "@RETURN_VALUE")
                {
                    //asignar el valor correspondiente
                    p.Value = valores[indice];
                    indice++;
                }
            }
        }

        //metodo para ejecutar procedimiento almacenados al usar el INSERT , UPDATE, DELETE
        public static void EjecutarSP(
            string nombreSP, params object[] valoresparametros)
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open ();

            SqlCommand cmd = new SqlCommand(nombreSP, cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            if(valoresparametros.Length > 0 )
                LlenarParametros(cmd, valoresparametros);

            cmd.ExecuteNonQuery ();
            cnx.Close();
        }

        // metodo para ejecuta procedimientos almacenados que use SELECT
        public static DataTable RetornaDataTable(
            string nombreSP, params object[] valoresparametros)
        {
            DataTable tabla = new DataTable();
            //
            using (SqlDataAdapter adap = new SqlDataAdapter(
                nombreSP, cad_cn))
            {
                //abir la conexion a la bd
                adap.SelectCommand.Connection.Open();
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                //si hay valores para los parametros
                if (valoresparametros.Length > 0)
                {
                    LlenarParametros(adap.SelectCommand,
                        valoresparametros);
                }
                //llenar el DataTable
                adap.Fill(tabla);
            }

            return tabla;   
        }


        // metodo que ejecuta un SP y retorna un valor scalar
        public static object RetornaValor(
            string nombreSP, params object[] valoresparametros) // ExecuteScalar
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            try
            {
                cnx.Open();

                SqlCommand cmd = new SqlCommand(nombreSP, cnx);
                cmd.CommandType = CommandType.StoredProcedure;

                if (valoresparametros.Length > 0)
                    LlenarParametros(cmd, valoresparametros);

                object rpta = cmd.ExecuteScalar();

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                    cnx.Close();
            }

        }
        //
        // metodo que ejecuta un SP para insertar, eliminar, actualizar
        //haciendo uso de una transaccioon
        public static void EjecutarSP_TRX(
           string nombreSP, params object[] valoresparametros)
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();

            //definir un objeto SqlTransacction
            // usando con el SqlConnection
            SqlTransaction trx = cnx.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(nombreSP, cnx, trx);
                cmd.CommandType = CommandType.StoredProcedure;

                if (valoresparametros.Length > 0)
                    LlenarParametros(cmd, valoresparametros);

                cmd.ExecuteNonQuery();

                //si no hubo error confirmamos la transaccion
                trx.Commit();                
            }
            catch (Exception ex) 
            {
                //si hubo un error, cancelamos la transaccion
                trx.Rollback();
                throw new Exception(ex.Message);               
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                    cnx.Close();
            }
            
        }


    }//fin del DBHelper
}