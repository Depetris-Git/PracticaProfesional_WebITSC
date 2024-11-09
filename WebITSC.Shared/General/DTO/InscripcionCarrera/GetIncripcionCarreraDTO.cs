using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO.InscripcionCarrera
{
    public class GetIncripcionCarreraDTO
    {
 
        public string AlumnoNombre { get; set; }
        public string AlumnoApellido { get; set; }
        public string AlumnoDocumento { get; set; }

        
        //public int CarreraId { get; set; }

        //public string CarreraName { get; set; }
        //public Carrera Carrera { get; set; }

        public int Cohorte { get; set; }

    }
}

