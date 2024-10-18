using WebITSC.DB.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.Shared.General.DTO
{
    public class CrearPlanEstudioDTO
    {
        [Required(ErrorMessage = "La carrera es necesaria")]
        public int CarreraId { get; set; }

        [Required(ErrorMessage = "El nombre del plan es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El año del plan es necesario")]
        public int Anno { get; set; }

        [Required(ErrorMessage = "El estado del plan es necesario")]
        public bool EstadoPlan { get; set; } //  Activo/Inactivo (basicamente, si en algún año de la carrera actualmente se está usando ese plan)

        public List<MateriaEnPlanEstudio> MateriasEnPlanEstudio = new List<MateriaEnPlanEstudio>();
    }
}
