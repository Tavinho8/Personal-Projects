namespace WebAcademia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inscripciones")]
    public partial class Inscripcion
    {
        [Key]
        public int InscripcionID { get; set; }

        public int CursoID { get; set; }

        public int EstudianteID { get; set; }

        public DateTime FechaInscripcion { get; set; } = DateTime.Now;  // No nullable con default

        [ForeignKey("CursoID")]
        public virtual Curso Curso { get; set; }

        [ForeignKey("EstudianteID")]
        public virtual Usuario Estudiante { get; set; }
    }

}
