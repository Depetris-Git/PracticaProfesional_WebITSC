using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        private readonly Context context;

        public PersonaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Persona> FullGetById(int id)
        {
            return await context.Personas
                .Include(p => p.TipoDocumento)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Persona>> FullGetAll()
        {
            return await context.Personas
                .Include(p => p.TipoDocumento)  
                .ToListAsync();
        }

        public async Task FullInsert(Persona persona)
        {
            await context.Personas.AddAsync(persona);
            await context.SaveChangesAsync();
        }

        public async Task FullUpdate(Persona persona)
        {
            context.Personas.Update(persona);
            await context.SaveChangesAsync();
        }
        //public async Task<Persona> ObtenerPersonaConCohorte(int personaId)
        //{
        //    var persona = await context.Personas
        //        .Where(p => p.Id == personaId)
        //        .Include(p => p.InscripcionesCarrera) // Incluye las inscripciones del alumno
        //        .ThenInclude(i => i.Cohorte) // Incluye la relación con Cohorte
        //        .FirstOrDefaultAsync();

        //    if (persona == null)
        //        return null;

        //    var cohorte = persona.InscripcionesCarrera
        //        .FirstOrDefault()? // Obtener el primer cohorte asociado
        //        .Cohorte;

        //    return new Persona
        //    {
        //        Id = persona.Id,
        //        Nombre = persona.Nombre,
        //        FechaNacimiento = persona.FechaNacimiento,
        //        Cohorte = cohorte?.Id ?? 0, // Asegúrate de que no sea nulo
                
        //    };
        //}

    }
}
