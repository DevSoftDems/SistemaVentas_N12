using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Carrito
    {
        [Display(Name = "Código")]
        public String idProd { get; set; }
        [Display(Name = "Nombre Producto")]
        public String nomProd { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
        [Display(Name = "Cantidad")]
        public int stock { get; set; }
        [Display(Name = "Talla")]
        public String talla { get; set; }
        [Display(Name = "Sub-Total")]
        public decimal importe
        {
            get
            {
                return precio * stock;
            }
        }
    }
}
