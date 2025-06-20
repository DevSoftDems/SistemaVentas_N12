using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Entidad;
using System;
using System.Data;


namespace Capa_Datos
{
    public class UsuariosDAO
    {

        public Usuario ValidarUsuario(string usuario, string password)
        {
            try
            {
                var dt = DBHelper.RetornaDataTable("PA_VALIDAR_USUARIO", usuario, password);
                if (dt.Rows.Count == 0) return null;

                var row = dt.Rows[0];
                return new Usuario
                {
                    idUser = Convert.ToInt32(row["idUser"]),
                    usuario = row["usuario"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar usuario: " + ex.Message);
            }
        }


    }
}
