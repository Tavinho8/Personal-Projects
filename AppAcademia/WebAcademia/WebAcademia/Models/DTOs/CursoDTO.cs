using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAcademia.Models.DTOs
{
    public class CursoDTO
    {
       
            public int CursoID { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string InstructorNombre { get; set; }
            public int CupoMaximo { get; set; }
            public bool? Activo { get; set; }
        

    }
}