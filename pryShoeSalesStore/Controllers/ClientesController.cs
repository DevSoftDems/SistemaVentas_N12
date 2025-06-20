using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Capa_Entidad;
using Capa_Negocio;

namespace pryShoeSalesStore.Controllers
{
    public class ClientesController : Controller
    {

        ClientesNegocio neg1 = new ClientesNegocio();

        // GET: Clientes
        public ActionResult Index(int nro_pag = 0, string nomCli = " ") //Listamos nuestros clientes
        {
            var listado = neg1.ListarClientes();
           

            // Filtrado si se proporciona un código
            if (!string.IsNullOrWhiteSpace(nomCli))
            {
                listado = listado.Where(a => a.nomCli.Contains(nomCli.Trim())).ToList();
            }

            //agrgamos contador
            ViewBag.contador = listado.Count;

            //PARA LA PAGINACION cuando se usa parametros enla consulta
            ViewBag.idProd = nomCli;

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

        // GET: Clientes/Details/5
        public ActionResult Details(string id)
        {
            // buscar el cliente por el id
            var buscado = neg1.ListarClientes()
                     .Find(x => x.idCli.Equals(id));

            return View(buscado);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {     
            return View(new Cliente());
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(Cliente obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = neg1.GrabarCliente(obj);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                ViewBag.mensaje = ex.Message;
            }
            return View(obj);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(string id)
        {
            // buscar el cliente por el id
            var buscado = neg1.ListarClientes()
                     .Find(x => x.idCli.Equals(id));

            return View(buscado);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Cliente obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = neg1.ActualizarCliente(obj);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View(obj);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(string id)
        {
            // buscar el cliente por el id
            var buscado = neg1.ListarClientes()
                     .Find(x => x.idCli.Equals(id));

            return View(buscado);
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                TempData["mensaje"] = neg1.EliminarCliente(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View();
        }



    }
}
