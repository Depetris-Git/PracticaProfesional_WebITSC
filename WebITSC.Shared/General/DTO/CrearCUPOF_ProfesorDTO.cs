using WebITSC.DB.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO
{
    public class CrearCUPOF_ProfesorDTO
    {
        [Required(ErrorMessage = "El turno es necesario")]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        [Required(ErrorMessage = "El codigo del CUPOF (CUPOF en sí) es necesario")]
        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string CUPOF { get; set; }

        [Required(ErrorMessage = "Saber si el CUPOF está ocpuado o libre es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Ocupado_Libre { get; set; } //para escribir si el puesto del CUPOF está ya ocupado o no

        [Required(ErrorMessage = "El estado del CUPOF es necesario")]
        public bool Estado { get; set; }
    }
}
