namespace WebAcademia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
    using System.Data.Entity.Spatial;

    public partial class Usuario
    {
        public Usuario()
        {
            Cursos = new HashSet<Curso>();
            Inscripciones = new HashSet<Inscripcion>();
            Logs = new HashSet<Log>();
            Activo = true; // default activo
        }

        [Key]
        public int UsuarioID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(50)]
        public string Salt { get; set; }

        public int RolID { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }
        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
        public virtual ICollection<Log> Logs { get; set; }

        [ForeignKey("RolID")]
        public virtual Rol Rol { get; set; }
    }

}
