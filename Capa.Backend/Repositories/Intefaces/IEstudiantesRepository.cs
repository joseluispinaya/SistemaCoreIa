using Capa.Shared.Entities;
using Capa.Shared.Responses;

namespace Capa.Backend.Repositories.Intefaces
{
    public interface IEstudiantesRepository
    {
        Task<ActionResponse<IEnumerable<Estudiante>>> GetAsync();
    }
}
