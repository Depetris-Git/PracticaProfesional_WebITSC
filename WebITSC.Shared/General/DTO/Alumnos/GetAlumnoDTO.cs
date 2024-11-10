using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO.Alumnos
{
    public class GetAlumnoDTO
    {
        public int Id { get; set; }
        public int CarreraId { get; set; }  // Carrera en la que se va a inscribir
        public string NameCarrera { get; set; }
        public string NombrePersona { get; set; }
        public string ApellidoPersona { get; set; }
        public string DocumentoPersona { get; set; }
        public string? TelefonoPersona { get; set; }
        public string DomicilioPersona { get; set; }
        public string Sexo { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento del alumno es necesario")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La edad del alumno es necesario")]
        public int Edad { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? CUIL { get; set; }

        [Required(ErrorMessage = "El país de nacimiento del alumno es necesario")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Pais { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Provincia { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Departamento { get; set; }

        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? TituloBase { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? FotocopiaDNI { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? ConstanciaCUIL { get; set; } //esto es para indicar que el alumno trajo o mandó un documento virtual de la constancia de CUIL, no tiene que ver con el atributo "CUIL", el cual es el cuil real.

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? PartidaNacimiento { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Analitico { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? FotoCarnet { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? CUS { get; set; }
        
        public bool Estado { get; set; } = true;

        //_persona_____________________________________________________________________ 
        //public string PersonaId { get; set; }

        //_cohorte_____________________________________________________________________
       public int Cohorte { get; set; }

    }
}
