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
    public class ProductosDAO
    {
        public List<Productos> ListarProductos()
        {
            var lista = new List<Productos>();
            try
            {
                DataTable dt =
                    DBHelper.RetornaDataTable("PA_PRODUCTOS");
                //serealizar el datatable a Json
                string cad_json = JsonConvert.SerializeObject(dt);

                //deserealizamos el Json a una lista
                lista = JsonConvert.DeserializeObject<List<Productos>>(cad_json);

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string GrabarProducto(Productos obj)
        {
            try
            {
                DBHelper.EjecutarSP("PA_GRABAR_PRODUCTO",
                    obj.idProd, obj.nomProd, obj.precio,
                    obj.stock, obj.talla
                    );
                //enviamos mensaje si se graba correctamente
                return "Nuevo Producto registrado";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string ActualizarProducto(Productos obj)
        {
            try
            {
                DBHelper.EjecutarSP("PA_ACTUALIZAR_PRODUCTO",
                    obj.idProd, obj.nomProd, obj.precio,
                    obj.stock, obj.talla
                    );

                // si se actualiza correctamente
                return $"Se actualizo el Producto: {obj.nomProd}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string EliminarProducto(string idProd)
        {
            try
            {
                DBHelper.EjecutarSP("PA_ELIMINAR_PRODUCTO",
                    idProd);

                // si se elimino de forma logica
                return $"Se elimino de forma logica al Producto: {idProd}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
