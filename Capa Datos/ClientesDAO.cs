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
    public class ClientesDAO
    {
        public List<Cliente> ListarClientes()
        {
            var lista = new List<Cliente>();
            try
            {
                DataTable dt =
                    DBHelper.RetornaDataTable("PA_CLIENTES");
                //serealizar el datatable a Json
                string cad_json = JsonConvert.SerializeObject(dt);

                //deserealizamos el Json a una lista
                lista = JsonConvert.DeserializeObject<List<Cliente>>(cad_json);

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Productos> ListarProductos(string nombre)
        {
            var lista = new List<Productos>();
            try
            {
                DataTable dt =
                    DBHelper.RetornaDataTable("PA_PRODUCTOS", nombre);

                string cad_json = JsonConvert.SerializeObject(dt);

                lista = JsonConvert.DeserializeObject<List<Productos>>(cad_json);

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GrabarCliente(Cliente obj)
        {
            try
            {
                DBHelper.EjecutarSP("PA_GRABAR_CLIENTE",
                    obj.idCli, obj.nomCli, obj.apeCli,
                    obj.dni, obj.dirCli, obj.fecReg
                    );
                //enviamos mensaje si se graba correctamente
                return "Nuevo Cliente registrado";
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
                DBHelper.EjecutarSP("PA_ACTUALIZAR_CLIENTE",
                    obj.idCli, obj.nomCli, obj.apeCli,
                    obj.dni, obj.dirCli, obj.fecReg
                    );

                // si se actualiza correctamente
                return $"Se actualizo el cliente: {obj.nomCli}";
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
                DBHelper.EjecutarSP("PA_ELIMINAR_CLIENTE",
                    idCli);

                // si se elimino de forma logica
                return $"Se elimino de forma logica al Cliente: {idCli}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }//fin de la clase clientesDAO
}
