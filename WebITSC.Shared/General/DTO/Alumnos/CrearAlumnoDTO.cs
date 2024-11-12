using WebITSC.DB.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO.Alumnos
{
    public class CrearAlumnoDTO
    {
            public int CarreraId { get; set; }  // Carrera en la que se va a inscribir
                                            // Datos de Persona
            [Required(ErrorMessage = "El nombre de la persona es necesario")]
            [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El apellido de la persona es necesario")]
            [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Apellido { get; set; }

            [Required(ErrorMessage = "El número de documento de la persona es necesario")]
            [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Documento { get; set; }

            [Required(ErrorMessage = "El tipo de documento es necesario")]
            public int TipoDocumentoId { get; set; }

            [Required(ErrorMessage = "El número de telefono de la persona es necesario")]
            [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Telefono { get; set; }

            [Required(ErrorMessage = "El Domicilio de la persona es necesario")]
            [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Domicilio { get; set; }



            // Datos de Usuario
            [Required(ErrorMessage = "El email de la persona es necesario")]
            [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "El nombre de usuario de la persona es necesario")]
            [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Contrasena { get; set; }

            // Datos específicos del Alumno
            [Required(ErrorMessage = "El sexo del alumno es necesario")]
            [MaxLength(20, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Sexo { get; set; }

            [Required(ErrorMessage = "La fecha de nacimiento del alumno es necesario")]
            public DateTime FechaNacimiento { get; set; }

            [Required(ErrorMessage = "La edad del alumno es necesario")]
            public int Edad { get; set; }

            [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? CUIL { get; set; }

            [Required(ErrorMessage = "El país de nacimiento del alumno es necesario")]
            [MaxLength(30, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string Pais { get; set; }

            [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? Provincia { get; set; }

            [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? Departamento { get; set; }

            // Otros campos opcionales
            [MaxLength(60, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? TituloBase { get; set; }

            [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? FotocopiaDNI { get; set; }

            [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? ConstanciaCUIL { get; set; }

            [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? PartidaNacimiento { get; set; }

            [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? Analitico { get; set; }

            [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? FotoCarnet { get; set; }

            [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
            public string? CUS { get; set; }

            //public string Cohorte { get; set; } /*= DateTime.Now.Year.ToString();*/


    }
}

