using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Entidad;
using Capa_Datos;
using System.Data;

namespace Capa_Negocio
{
    public class ProductosNegocio
    {
        ProductosDAO dao = new ProductosDAO();

        public List<Productos> ListarProductos()
        {
            return dao.ListarProductos();
        }                

        public string GrabarProducto(Productos obj)
        {
            try
            {
                return dao.GrabarProducto(obj);
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
                return dao.ActualizarProducto(obj);
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
                return dao.EliminarProducto(idProd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
