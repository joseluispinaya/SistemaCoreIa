using Capa.Backend.Data;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.DTOs;
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

        public async Task<ActionResponse<IEnumerable<EstudianteResponseDTO>>> GetNewAsync()
        {
            var estudiantes = await _context.Estudiantes
                .Select(d => new EstudianteResponseDTO
                {
                    Id = d.Id,
                    NroCi = d.NroCi,
                    Nombres = d.Nombres,
                    Apellidos = d.Apellidos,
                    Correo = d.Correo,
                    Codigo = d.Codigo,

                    Carrera = new CarreraSimpleDTO
                    {
                        Id = d.Carrera.Id,
                        Nombre = d.Carrera.Nombre
                    },

                    Proyecto = d.ProyectoGrado == null
                    ? null
                    : new ProyectoSimpleDTO
                    {
                        Id = d.ProyectoGrado.Id,
                        Titulo = d.ProyectoGrado.Titulo,
                        Gestion = d.ProyectoGrado.Gestion
                    }
                })
                .ToListAsync();

            return new ActionResponse<IEnumerable<EstudianteResponseDTO>>
            {
                WasSuccess = true,
                Result = estudiantes
            };
        }
    }
}
