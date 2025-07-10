namespace WebAcademia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cursos")]
    public partial class Curso
    {
        public Curso()
        {
            ContenidoCurso = new HashSet<ContenidoCurso>();
            Inscripciones = new HashSet<Inscripcion>();
        }

        [Key]
        public int CursoID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int InstructorID { get; set; }

        public int CupoMaximo { get; set; }

        public bool Activo { get; set; } = true; // mejor no nullable

        public virtual ICollection<ContenidoCurso> ContenidoCurso { get; set; }

        // Cambiar a singular y usar ForeignKey para claridad
        [ForeignKey("InstructorID")]
        public virtual Usuario Instructor { get; set; }

        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
    }

}
