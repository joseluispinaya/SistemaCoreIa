using Capa.Backend.Data;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Repositories.Implementations
{
    public class EstudiantesRepository : IEstudiantesRepository
    {
        private readonly DataContext _context;

        public EstudiantesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ActionResponse<IEnumerable<Estudiante>>> GetAsync()
        {
            var estudiantes = await _context.Estudiantes
            .Include(c => c.Carrera)
            .Include(x => x.ProyectoGrado)
            .ToListAsync();
            return new ActionResponse<IEnumerable<Estudiante>>
            {
                WasSuccess = true,
                Result = estudiantes
            };
        }
    }
}
