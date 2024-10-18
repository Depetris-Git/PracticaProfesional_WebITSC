using WebITSC.DB.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO
{
    public class CrearEvaluacionDTO
    {
        [Required(ErrorMessage = "El turno es necesario")]
        public int TurnoId { get; set; }

        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El tipo de la evaluacion es necesario")]
        [MaxLength(36, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string TipoEvaluacion { get; set; } //Parcial, Final, IEFI, recuperatorio, etc.

        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Folio { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Libro { get; set; }

        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Sede { get; set; }

        public List<Nota>? Notas { get; set; } = new List<Nota>();
    }
}
