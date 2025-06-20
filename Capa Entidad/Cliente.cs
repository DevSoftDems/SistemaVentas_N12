using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Cliente
    {
        [Display(Name = "Código")]
        [RegularExpression(@"^P\d{4}$", ErrorMessage = "El código inicia con 'C' y 4 dígitos (ejemplo: C0034)")]
        public string idCli {  get; set; }
        [Display(Name = "Nombre")]
        public string nomCli {  get; set; }
        [Display(Name = "Apellido")]
        public string apeCli {  get; set; }
        [Display(Name = "DNI")]
        public string dni {  get; set; }
        [Display(Name = "Dirección")]
        public string dirCli {  get; set; }
        [Display(Name = "Eliminado")]
        public string eliCli {  get; set; }
        [Display(Name = "Fecha")]
        public DateTime fecReg { get; set; }
    }
}
