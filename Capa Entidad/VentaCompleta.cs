using System;
using System.Collections.Generic;

namespace Capa_Entidad
{
    public class VentaCompleta
    {
        public string numVenta { get; set; }
        public DateTime fecha { get; set; }
        public Cliente cliente { get; set; }
        public decimal total { get; set; }
        public List<Carrito> detalle { get; set; }
    }
}
