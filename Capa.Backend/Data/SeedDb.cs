using Capa.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCarrerasAsync();
            await CheckDocentesAsync();
            await CheckEstudiantesAsync();
            await CheckProyectosAsync();
        }

        private async Task CheckCarrerasAsync()
        {
            if (!_context.Carreras.Any())
            {
                _context.Carreras.Add(new Carrera { Nombre = "Ing. Sistemas" });
                _context.Carreras.Add(new Carrera { Nombre = "Ing. Comercial" });
                _context.Carreras.Add(new Carrera { Nombre = "Ing. Civil" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckDocentesAsync()
        {
            if (!_context.Docentes.Any())
            {
                await AddDocenteAsync(
                    "21222321",
                    "Fernando",
                    "Apaza Luna",
                    "fernando@yopmail.com",
                    @"Ingeniero de Sistemas con amplia experiencia en desarrollo de software empresarial,
                      bases de datos relacionales, arquitectura de sistemas y proyectos de transformación digital.
                      Ha participado como tutor en proyectos relacionados con sistemas académicos, plataformas web,
                      sistemas de control administrativo y aplicaciones con inteligencia artificial.
                      Especialista en .NET, SQL Server, APIs REST y análisis de requerimientos.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "45875212",
                    "Lidia",
                    "Mamani Mara",
                    "lidia@yopmail.com",
                    @"Ingeniera de Sistemas con enfoque en ciencia de datos, machine learning y análisis predictivo.
                      Experiencia en proyectos de minería de datos, visualización de información y
                      aplicaciones académicas con modelos de aprendizaje automático.
                      Conocimientos en Python, Scikit-Learn, análisis estadístico y procesamiento de lenguaje natural.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "88745212",
                    "Marcos",
                    "Pedraza Campos",
                    "pedraza@yopmail.com",
                    @"Ingeniero de Sistemas con especialidad en desarrollo web full stack,
                      aplicaciones móviles y arquitectura de software.
                      Tutor de proyectos relacionados con sistemas de gestión, plataformas educativas,
                      aplicaciones móviles y comercio electrónico.
                      Experiencia en .NET, JavaScript, React, bases de datos SQL y diseño de sistemas distribuidos.",
                    "Ing. Sistemas");

                await AddDocenteAsync(
                    "15874521",
                    "Marcelo",
                    "Aponte Chavez",
                    "aponte@yopmail.com",
                    @"Ingeniero Comercial con experiencia en sistemas de información empresarial,
                      gestión de procesos, análisis financiero y control administrativo.
                      Ha asesorado proyectos de grado relacionados con sistemas de ventas,
                      gestión de inventarios, contabilidad y administración de empresas.
                      Especialista en análisis de costos, planificación estratégica y sistemas ERP.",
                    "Ing. Comercial");

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckEstudiantesAsync()
        {
            if (!_context.Estudiantes.Any())
            {
                await AddEstudianteAsync("65652544", "Ever", "Muchairo Lazo", "muchairo@yopmail.com", "R298-4", "Ing. Sistemas");
                await AddEstudianteAsync("11255712", "Waldo", "Saenz Pilco", "waldo@yopmail.com", "R299-2", "Ing. Sistemas");
                await AddEstudianteAsync("22745225", "Milton", "Yujra Pally", "milton@yopmail.com", "R300-5", "Ing. Comercial");
                await AddEstudianteAsync("21874588", "Dario", "Miranda Lino", "dario@yopmail.com", "R450-1", "Ing. Comercial");
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProyectosAsync()
        {
            if (!_context.ProyectoGrados.Any())
            {
                var estudiantes = await _context.Estudiantes.ToListAsync();
                var docentes = await _context.Docentes.ToListAsync();

                AddProyecto(
                    estudiantes[0],
                    docentes.First(d => d.Nombres == "Fernando"),
                    "Sistema de gestión académica con inteligencia artificial",
                    @"Desarrollo de un sistema web que permita gestionar procesos académicos
                    utilizando algoritmos de inteligencia artificial para análisis de rendimiento
                    estudiantil, predicción de deserción y recomendación de tutorías.",
                    "2024");

                AddProyecto(
                    estudiantes[1],
                    docentes.First(d => d.Nombres == "Lidia"),
                    "Plataforma de análisis predictivo para rendimiento estudiantil",
                    @"Implementación de una plataforma que emplea técnicas de machine learning
                    para analizar el desempeño de los estudiantes y predecir su rendimiento
                    académico, proporcionando reportes y visualizaciones interactivas.",
                    "2024");

                AddProyecto(
                    estudiantes[2],
                    docentes.First(d => d.Nombres == "Marcelo"),
                    "Sistema de control de ventas e inventarios para PYMES",
                    @"Desarrollo de un sistema empresarial para gestionar ventas, compras,
                    inventarios y reportes financieros, orientado a pequeñas y medianas empresas.",
                    "2023");

                AddProyecto(
                    estudiantes[3],
                    docentes.First(d => d.Nombres == "Marcos"),
                    "Aplicación móvil para gestión de tareas colaborativas",
                    @"Creación de una aplicación móvil multiplataforma que permita organizar tareas,
                    asignar responsabilidades y mejorar la productividad de equipos de trabajo.",
                    "2023");

                await _context.SaveChangesAsync();
            }
        }

        private async Task AddDocenteAsync(string nroCi, string nombres, string apellidos, string correo, string resumenPerfil, string carrera)
        {
            var carrer = await _context.Carreras.FirstOrDefaultAsync(x => x.Nombre == carrera);

            carrer ??= await _context.Carreras.FirstOrDefaultAsync();
            //carrer ??= await _context.Carreras.FirstOrDefaultAsync();

            Docente docente = new()
            {
                NroCi = nroCi,
                Nombres = nombres,
                Apellidos = apellidos,
                Correo = correo,
                ResumenPerfil = resumenPerfil,
                Carrera = carrer!
            };

            _context.Docentes.Add(docente);
        }

        private async Task AddEstudianteAsync(string nroCi, string nombres, string apellidos, string correo, string codigo, string carrera)
        {
            var carrer = await _context.Carreras.FirstOrDefaultAsync(x => x.Nombre == carrera);
            carrer ??= await _context.Carreras.FirstOrDefaultAsync();

            Estudiante estudiante = new()
            {
                NroCi = nroCi,
                Nombres = nombres,
                Apellidos = apellidos,
                Correo = correo,
                Codigo = codigo,
                Carrera = carrer!
            };

            _context.Estudiantes.Add(estudiante);
        }

        private void AddProyecto(Estudiante estudiante, Docente docente, string titulo, string resumen, string gestion)
        {
            ProyectoGrado proyecto = new()
            {
                Estudiante = estudiante,
                Docente = docente,
                Titulo = titulo,
                Resumen = resumen,
                Gestion = gestion
            };

            _context.ProyectoGrados.Add(proyecto);
        }
    }
}
