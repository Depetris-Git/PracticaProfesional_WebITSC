using WebITSC.DB.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO
{
    public class CrearCertificadoAlumnoDTO
    {
        [Required(ErrorMessage = "El alumno es necesario")]
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        [Required(ErrorMessage = "La fecha de emisión del certificado es necesaria")]
        public DateTime FechaEmision { get; set; }
    }
}
