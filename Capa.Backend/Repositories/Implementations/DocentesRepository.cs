using Capa.Backend.Data;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Repositories.Implementations
{
    public class DocentesRepository : IDocentesRepository
    {
        private readonly DataContext _context;
        public DocentesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResponse<Docente>> AddAsync(DocenteDTO docenteDTO)
        {

            var carrera = await _context.Carreras.FindAsync(docenteDTO.CarreraId);
            if (carrera == null)
            {
                return new ActionResponse<Docente>
                {
                    WasSuccess = false,
                    Message = "Carrera no encontrada"
                };
            }

            var docente = new Docente
            {
                NroCi = docenteDTO.NroCi,
                Nombres = docenteDTO.Nombres,
                Apellidos = docenteDTO.Apellidos,
                Correo = docenteDTO.Correo,
                ResumenPerfil = docenteDTO.ResumenPerfil,
                CarreraId = docenteDTO.CarreraId,
            };

            _context.Add(docente);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Docente>
                {
                    WasSuccess = true,
                    Result = docente
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Docente>
                {
                    WasSuccess = false,
                    Message = "Ya existe un registro con el Nro CI."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Docente>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public async Task<ActionResponse<IEnumerable<Docente>>> GetAsync()
        {
            var docentes = await _context.Docentes
            .Include(c => c.Carrera)
            .Include(x => x.Proyectos)
            .ToListAsync();
            return new ActionResponse<IEnumerable<Docente>>
            {
                WasSuccess = true,
                Result = docentes
            };
        }

        public async Task<ActionResponse<IEnumerable<DocenteResponseDTO>>> GetNewAsync()
        {
            var docentes = await _context.Docentes
                .Select(d => new DocenteResponseDTO
                {
                    Id = d.Id,
                    NroCi = d.NroCi,
                    Nombres = d.Nombres,
                    Apellidos = d.Apellidos,
                    Correo = d.Correo,
                    ResumenPerfil = d.ResumenPerfil,

                    Carrera = new CarreraSimpleDTO
                    {
                        Id = d.Carrera.Id,
                        Nombre = d.Carrera.Nombre
                    },

                    Proyectos = d.Proyectos.Select(p => new ProyectoSimpleDTO
                    {
                        Id = p.Id,
                        Titulo = p.Titulo,
                        Gestion = p.Gestion
                    }).ToList()
                })
                .ToListAsync();

            return new ActionResponse<IEnumerable<DocenteResponseDTO>>
            {
                WasSuccess = true,
                Result = docentes
            };
        }
    }
}
