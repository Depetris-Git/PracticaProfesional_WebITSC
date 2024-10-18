using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.DB.Data.Entity
{
    public class Coordinador : EntityBase
    {
        [Required(ErrorMessage = "El usuario que será coordinador es necesario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "La carrera es necesaria")]
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        public bool Estado { get; set; } = true; //si está activo o no el coordinador, para bloquear o dar acceso
                                                 //al usuario en su calidad de coordinador
    }
}
