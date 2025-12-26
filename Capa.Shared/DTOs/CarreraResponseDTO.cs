using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Shared.DTOs
{
    public class CarreraResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CantidadDocentes { get; set; }
        public int CantidadEstudiantes { get; set; }
    }
}
