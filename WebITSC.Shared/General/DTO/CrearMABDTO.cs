using WebITSC.DB.Data.Entity;
using WebITSC.DB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO
{
    public class CrearMABDTO
    {

        [Required(ErrorMessage = "El profesor es necesario")]
        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        [Required(ErrorMessage = "El CUPOF de profesor es necesario")]
        public int CUPOF_ProfesorId { get; set; }
        public CUPOF_Profesor CUPOF_Profesor { get; set; }

        [Required(ErrorMessage = "El Id del MAB (código del MAB) es necesario")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string IdMab { get; set; }

        [Required(ErrorMessage = "La situacion revista es necesaria")]
        [MaxLength(45, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string SitRevista { get; set; }

        [Required(ErrorMessage = "La Fecha de Inicio es necesaria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La Fecha de Fin de contrato es necesaria")]
        public DateTime FechaFin { get; set; }
    }
}
