namespace WebAcademia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        [Key]
        public int LogID { get; set; }

        public int? UsuarioID { get; set; }

        [StringLength(100)]
        public string Accion { get; set; }

        public DateTime? Fecha { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
    }

}
