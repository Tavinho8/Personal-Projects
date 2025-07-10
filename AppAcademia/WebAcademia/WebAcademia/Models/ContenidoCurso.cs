namespace WebAcademia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContenidoCurso")]
    public partial class ContenidoCurso
    {
        [Key]
        public int ContenidoID { get; set; }

        public int CursoID { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; }

        public string URL { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [ForeignKey("CursoID")]
        public virtual Curso Curso { get; set; }
    }

}
