using System.ComponentModel.DataAnnotations;

namespace Capa.Shared.Entities
{
    public class Carrera
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo Carrera no puede tener más de 100 carácteres.")]
        [Required(ErrorMessage = "El campo Carrera es obligatorio.")]
        public string Nombre { get; set; } = null!;

        public ICollection<Docente> Docentes { get; set; } = [];
        public ICollection<Estudiante> Estudiantes { get; set; } = [];

        //public ICollection<Docente>? Docentes { get; set; }

        //public int DocentesCount => Docentes == null ? 0 : Docentes.Count;

        //public ICollection<Estudiante>? Estudiantes { get; set; }

        //public int EstudiantesCount => Estudiantes == null ? 0 : Estudiantes.Count;

    }
}
