using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO.Carreraa
{
    public class GetCarreraDTO
    {
        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La duración de la carrera (en años) es necesaria")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string DuracionCarrera { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Modalidad { get; set; }
    }
}
