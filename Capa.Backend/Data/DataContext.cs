using Capa.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<ProyectoGrado> ProyectoGrados { get; set; }

        //public DbSet<ProyectoGrado> ProyectoGrados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Carrera>().HasIndex(x => x.Nombre).IsUnique();
            modelBuilder.Entity<Docente>().HasIndex(x => x.NroCi).IsUnique();
            modelBuilder.Entity<Estudiante>().HasIndex(x => x.NroCi).IsUnique();
            modelBuilder.Entity<ProyectoGrado>().HasIndex(x => x.EstudianteId).IsUnique();
            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
