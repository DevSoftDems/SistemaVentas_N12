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
    public class ClientesNegocio
    {
        ClientesDAO dao = new ClientesDAO();

        public List<Cliente> ListarClientes()
        {
            return dao.ListarClientes();
        }
               
        public List<Productos> ListarProductos()
        {
            return dao.ListarProductos("");
        }

        public string GrabarCliente(Cliente obj)
        {
            try
            {
                return dao.GrabarCliente(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ActualizarCliente(Cliente obj)
        {
            try
            {
                return dao.ActualizarCliente(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EliminarCliente(string idCli)
        {
            try
            {
                return dao.EliminarCliente(idCli);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    } //fin de la clase ClientesNegocio
}
