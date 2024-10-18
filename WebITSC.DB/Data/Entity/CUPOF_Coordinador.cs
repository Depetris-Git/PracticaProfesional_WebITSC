using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.DB.Data.Entity
{
    public class CUPOF_Coordinador : EntityBase
    {
        [Required(ErrorMessage = "La carrera es necesaria")]
        public int CarreraId { get; set; } //La carrera a la que corresponde el puesto del CUPOF, ej: Desarrollo de Software, Enfermería, etc.
        public Carrera Carrera { get; set; }

        public int? CoordinadorId { get; set; } //El coordinador al que le corresponderá el CUPOF. Como ven es nullable porque en teoría la vacante existe antes de ser ocupada
        public Coordinador? Coordinador { get; set; }

        [Required(ErrorMessage = "El codigo del CUPOF (CUPOF en sí) es necesario")]
        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string CUPOF { get; set; } //el código del CUPOF, el CUPOF en sí digamos

        [Required(ErrorMessage = "Saber si el puesto está ocupado o libre es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Ocupado_Libre { get; set; } //para escribir si el puesto del CUPOF está ya ocupado o no

        [Required(ErrorMessage = "El estado del CUPOF es necesario")]
        public bool Estado { get; set; } //Activo o Inactivo

        [Required(ErrorMessage = "La sede es necesaria")]
        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Sede { get; set; } //Sede del ITSC que corresponde al CUPOF, ejemplo: "Sede: Córdoba, Río Negro" o "capital", no sé, chequeen si quieren como escriben las Sedes
    }
}
