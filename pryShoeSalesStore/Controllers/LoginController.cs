using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Capa_Entidad;
using Capa_Negocio;

namespace pryShoeSalesStore.Controllers
{
    public class LoginController : Controller
    {

        UsuarioNegocio negocio = new UsuarioNegocio();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string password)
        {
            var user = negocio.ValidarUsuario(usuario, password);
            if (user != null)
            {
                Session["usuario"] = user.usuario;
                return RedirectToAction("Bienvenida");
            }
            ViewBag.Error = "Password o usuario incorrecto";
            return View();
        }

        public ActionResult Bienvenida()
        {
            if (Session["usuario"] == null) return RedirectToAction("Login");
            ViewBag.NombreUsuario = Session["usuario"].ToString().ToUpper();
            return View();
        }
    }
}