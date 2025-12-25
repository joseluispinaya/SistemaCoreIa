using Capa.Backend.Data;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Repositories.Implementations
{
    public class CarrerasRepository : ICarrerasRepository
    {
        private readonly DataContext _context;

        public CarrerasRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResponse<Carrera>> AddAsync(Carrera carrera)
        {
            _context.Add(carrera);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Carrera>
                {
                    WasSuccess = true,
                    Result = carrera
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Carrera>
                {
                    WasSuccess = false,
                    Message = "Ya existe el registro."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Carrera>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public async Task<ActionResponse<IEnumerable<Carrera>>> GetAsync()
        {
            var carreras = await _context.Carreras
            .Include(x => x.Docentes)
            .ToListAsync();
            return new ActionResponse<IEnumerable<Carrera>>
            {
                WasSuccess = true,
                Result = carreras
            };
        }
    }
}
