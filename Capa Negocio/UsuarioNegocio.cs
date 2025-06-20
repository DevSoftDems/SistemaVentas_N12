using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class UsuarioNegocio
    {
        UsuariosDAO dao = new UsuariosDAO();

        public Usuario ValidarUsuario(string usuario, string password)
        {
            return dao.ValidarUsuario(usuario, password);
        }

    }
}
