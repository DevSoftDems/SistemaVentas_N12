using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using System.Data;
using Newtonsoft.Json;

namespace Capa_Datos
{
    public class VentasDAO
    {
        public List<Cliente> ListarClientes()
        {
            var lista = new List<Cliente>();
            DataTable dt = DBHelper.RetornaDataTable("PA_CLIENTES");
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Cliente>>(cad_json);
            return lista;
        }

        public List<Productos> ListarProductos(string nombre)
        {
            var lista = new List<Productos>();
            DataTable dt = DBHelper.RetornaDataTable("PA_PRODUCTOS", nombre);
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Productos>>(cad_json);
            return lista;
        }

        public string GrabarVenta(string codigo, decimal total, List<Carrito> listaCarrito)
        {
            try
            {
                // Grabar cabecera de la venta PA_GRABAR_VENTAS_CAB
                string num_vta = DBHelper.RetornaValor("PA_GRABAR_VENTAS_CAB", codigo, total).ToString();

                // Grabar detalle de la venta PA_GRABAR_DETALLE_VENTA
                foreach (Carrito item in listaCarrito)
                {// ------- Modificacion para que se grabe el stock en vez de la Talla
                    DBHelper.EjecutarSP("PA_GRABAR_DETALLE_VENTA", num_vta, item.idProd, item.stock, item.precio, item.nomProd);
                }
                return num_vta;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public VentaCompleta ObtenerVentaCompleta(string numVenta)
        {
            var venta = new VentaCompleta();
            DataSet ds = DBHelper.RetornaDataSet("PA_OBTENER_VENTA_COMPLETA", numVenta);

            if (ds.Tables.Count >= 2)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    venta.numVenta = row["numVenta"].ToString();
                    venta.fecha = Convert.ToDateTime(row["fecha"]);
                    venta.total = Convert.ToDecimal(row["total"]);
                    venta.cliente = new Cliente
                    {
                        idCli = row["idCli"].ToString(),
                        nomCli = row["nomCli"].ToString(),
                        apeCli = row["apeCli"].ToString(),
                        dni = row["dni"].ToString(),
                        dirCli = row["dirCli"].ToString()
                    };
                }

                string cad = JsonConvert.SerializeObject(ds.Tables[1]);
                venta.detalle = JsonConvert.DeserializeObject<List<Carrito>>(cad);
            }

            return venta;
        }

    }
}
