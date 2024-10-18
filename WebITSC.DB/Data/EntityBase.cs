using WebITSC.DB.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebITSC.DB.Data
{
    public class EntityBase : IEntityBase
    {
        [Required(ErrorMessage = "El Id es necesario")]
        public int Id { get; set; }
    }
}
