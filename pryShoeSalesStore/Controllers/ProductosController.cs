using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Capa_Entidad;
using Capa_Negocio;

namespace pryShoeSalesStore.Controllers
{
    public class ProductosController : Controller
    {

        ProductosNegocio neg1 = new ProductosNegocio();

        // GET: Productos
        public ActionResult Productos(int nro_pag = 0, string nomProd = " ")
        {
            var listado = neg1.ListarProductos();

            // Filtrado si se proporciona un código
            if (!string.IsNullOrWhiteSpace(nomProd))
            {
                listado = listado.Where(a => a.nomProd.Contains(nomProd.Trim())).ToList();
            }

            //agrgamos contador
            ViewBag.contador = listado.Count;

            //PARA LA PAGINACION cuando se usa parametros enla consulta
            ViewBag.idProd = nomProd;

            //INICIO DE LA PAGINACION
            int n = listado.Count;

            // definir las variables ViewBag necesarias para la vista
            ViewBag.CONTADOR = n;

            // variables para la paginación
            int cant_filas = 5;

            // calcular la cantidad de paginas
            int cant_paginas = (n % cant_filas == 0) ? n / cant_filas : n / cant_filas + 1;

            ViewBag.CANT_PAGINAS = cant_paginas;

            // enviamos los datos del modelo a la vista
            return View(listado.Skip(nro_pag * cant_filas).Take(cant_filas));
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            // buscar el producto por el id
            var buscado = neg1.ListarProductos()
                     .Find(x => x.idProd.Equals(id));

            return View(buscado);
        }

        // GET: Productos/Create
        public ActionResult CreateProductos()
        {
            return View(new Productos());
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult CreateProductos(Productos obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = neg1.GrabarProducto(obj);

                    return RedirectToAction("Productos");
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View(obj);
        }

        // GET: Productos/Edit/5
        public ActionResult EditarProductos(string id)
        {
            // buscar el producto por el id
            var buscado = neg1.ListarProductos()
                     .Find(x => x.idProd.Equals(id));

            return View(buscado);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult EditarProductos(string id, Productos obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = neg1.ActualizarProducto(obj);

                    return RedirectToAction("Productos");
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View(obj);
        }

        // GET: Productos/Delete/5
        public ActionResult EliminarProductos(string id)
        {
            // buscar el producto por el id
            var buscado = neg1.ListarProductos()
                     .Find(x => x.idProd.Equals(id));

            return View(buscado);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public ActionResult EliminarProductos(string id, FormCollection collection)
        {
            try
            {
                TempData["mensaje"] = neg1.EliminarProducto(id);

                return RedirectToAction("Productos");
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View();
        }




    }
}
