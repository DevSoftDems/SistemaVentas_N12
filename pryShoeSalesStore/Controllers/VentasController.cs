using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Capa_Entidad;
using Capa_Negocio;

using Newtonsoft.Json;
using System.Transactions;

namespace pryShoeSalesStore.Controllers
{

    public class VentasController : Controller
    {
        VentasNegocio neg1 = new VentasNegocio();

        List<Carrito> listacar = new List<Carrito>();

        void GrabarCarrito()
        {
            Session["Carrito"] = JsonConvert.SerializeObject(listacar);
        }

        void LeerCarrito()
        {
                listacar = JsonConvert.DeserializeObject<List<Carrito>>(Session["Carrito"].ToString());
        }

        // GET: Ventas
        public ActionResult IndexProductos(string nombre = "")
        {
            if (Session["Carrito"] == null)
            {
                GrabarCarrito();
            }

            return View(neg1.ListarProductos(nombre));
            
        }

        // ---------------------------------------------------------------------

        // GET: Ventas/AgregarCarrito
        public ActionResult AgregarArticulo(string id = "")
        {
            Productos buscado = neg1.ListarProductos("").Find(a=>a.idProd.Equals(id));

            return View(buscado);
        }

        // POST: Ventas/AgregarCarrito
        [HttpPost]
        public ActionResult AgregarArticulo(string id ="",int cantidad =0)
        {
            LeerCarrito(); // Leer el carrito de compras desde la sesión

            // Encontrar datos de producto que sera agregado al carrito
            Productos buscado = neg1.ListarProductos("").Find(a => a.idProd.Equals(id));

            Carrito car = new Carrito()
            {
                idProd= id,
                nomProd = buscado.nomProd,
                precio = buscado.precio,
                stock = cantidad,
                talla = buscado.talla
            };
            Carrito encontrado = listacar.Find(a => a.idProd.Equals(id));

            if (encontrado == null)
            {
                listacar.Add(car);
                ViewBag.Mensaje = "Producto agregado al carrito correctamente.";
            }
            else
            {
                encontrado.stock += cantidad; // Si ya existe, aumentar la cantidad
                ViewBag.Mensaje = "Cantidad del producto actualizada en el carrito.";
            }

            GrabarCarrito(); // Guardar el carrito actualizado en la sesión
            return View(buscado);
        }

        // ---------------------------------------------------------------------

        // GET: Ventas/VerCarrito
        public ActionResult VerCarrito()
        {
            LeerCarrito(); // Leer el carrito de compras desde la sesión

            if(listacar.Count == 0)
            {
                return RedirectToAction("IndexProductos");
            }
            ViewBag.Total = listacar.Sum(a => a.importe); // Calcular el total del carrito
            return View(listacar);
        }

        // ---------------------------------------------------------------------

        // GET: Ventas/EliminarItem
        public ActionResult EliminarItem(string id)
        {
            LeerCarrito(); // Leer el carrito de compras desde la sesión

            if ( listacar.Count>0)
            {
                Carrito encontrado = listacar.Find(a => a.idProd.Equals(id));
                encontrado.stock -= 1; // Disminuir la cantidad del producto en el carrito

                if (encontrado.stock == 0)
                {
                    listacar.Remove(encontrado); // Si la cantidad llega a 0, eliminar el producto del carrito
                }
                GrabarCarrito(); // Guardar el carrito actualizado en la sesión
            }

            
            return RedirectToAction("VerCarrito");
        }

        // GET: Ventas/AgregarItem
        public ActionResult AgregarItem(string id)
        {
            LeerCarrito(); // Leer el carrito de compras desde la sesión
            Carrito encontrado = listacar.Find(a => a.idProd.Equals(id));
            if (encontrado != null)
            {
                encontrado.stock += 1; // Aumentar la cantidad del producto en el carrito
            }
            GrabarCarrito(); // Guardar el carrito actualizado en la sesión
            return RedirectToAction("VerCarrito");
        }

        // --------------------------------------------------------------------

        // GET: Ventas/PagarCarrito
        public ActionResult PagarCarrito()
        {
            LeerCarrito(); // Leer el carrito de compras desde la sesión
            ViewBag.total = listacar.Sum(a => a.importe); // Calcular el total del carrito
            ViewBag.clientes = new SelectList(neg1.ListarClientes(), "idCli", "nomCli", "apeCli");
            return View(listacar);
        }

        // POST: Ventas/PagarCarrito
        [HttpPost]
        public ActionResult PagarCarrito(string codCli="")
        {
            LeerCarrito(); // Leer el carrito de compras desde la sesión

            using (TransactionScope trx = new TransactionScope())
            {
                decimal total = listacar.Sum(a => a.importe);
                ViewBag.total = total; // Calcular el total del carrito
                try
                {
                    // Calcular el total del carrito
                    TempData["mensaje"] = neg1.GrabarVenta(codCli, total, listacar); // Grabar la venta

                    trx.Complete(); // Confirmar la transacción si todo sale bien
                    // Eliminar la sesión del carrito después de procesar el pago
                    Session.Remove("Carrito");
                    return RedirectToAction("IndexProductos");
                }
                catch (Exception ex)
                {
                    ViewBag.mensaje = "Error al procesar el pago: " + ex.Message;
                }

            }

            ViewBag.clientes = new SelectList(neg1.ListarClientes(), "idCli", "nomCli", "apeCli");
            return View(listacar);
        }


        // --------------------------------------------------------------------
        // GET: Ventas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ventas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
