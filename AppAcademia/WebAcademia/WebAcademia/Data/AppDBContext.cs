using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using WebAcademia.Models;

namespace WebAcademia.Data
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<ContenidoCurso> ContenidoCursos { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Inscripcion> Inscripciones { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>()
                .HasMany(e => e.ContenidoCurso)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.Inscripciones)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Cursos)
                .WithRequired(e => e.Instructor)   // Aquí cambio Usuarios por Instructor, según sugerencia
                .HasForeignKey(e => e.InstructorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Inscripciones)
                .WithRequired(e => e.Estudiante)  // Aquí cambio Usuarios por Estudiante
                .HasForeignKey(e => e.EstudianteID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Logs)
                .WithOptional(e => e.Usuario)     // Logs permiten UsuarioID nullable, con WithOptional
                .HasForeignKey(e => e.UsuarioID)
                .WillCascadeOnDelete(false);
        }

    }
}
