using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.DB.Data.Entity
{
    public class CUPOF_Profesor : EntityBase
    {
        [Required(ErrorMessage = "El turno es necesario")]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        [Required(ErrorMessage = "El codigo del CUPOF (CUPOF en sí) es necesario")]
        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string CUPOF { get; set; } //el código del CUPOF, el CUPOF en sí.

        [Required(ErrorMessage = "Saber si el CUPOF está ocpuado o libre es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Ocupado_Libre { get; set; } //para escribir si el puesto del CUPOF está ya ocupado o no.

        [Required(ErrorMessage = "El estado del CUPOF es necesario")]
        public bool Estado { get; set; } //Activo o Inactivo. Puede estar Inactivo si el CUPOF para un puesto fue reemplazado por otro CUPOF actualizado, o el puesto ya no existe. No confundir con Ocupado/Libre.
    }
}
