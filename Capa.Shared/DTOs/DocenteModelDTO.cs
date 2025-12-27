namespace Capa.Shared.DTOs
{
    public class DocenteModelDTO
    {
        //public int Id { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string ResumenPerfil { get; set; } = null!;
        public List<ProyectoSimpleDTO> Proyectos { get; set; } = [];
    }
}
