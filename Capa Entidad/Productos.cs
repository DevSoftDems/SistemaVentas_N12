using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Productos
    {
        [Display(Name = "Código")]
        [RegularExpression(@"^P\d{4}$", ErrorMessage = "El código inicia con 'P' y 4 dígitos (ejemplo: P0034)")]
        public String idProd { get; set; }
        [Display(Name = "Nombre Producto")]
        public String nomProd { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
        [Display(Name = "Stock")]
        public int stock { get; set; }
        [Display(Name = "Talla")]
        public String talla { get; set; }
        [Display(Name = "Eliminar")]
        public String eliProd { get; set; }
    }
}
