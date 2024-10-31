using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO
{
    public class GetPersonaDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Número de Documento es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El Número de Documento es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")] 
        public int Cohorte { get; set; }

    }
}
