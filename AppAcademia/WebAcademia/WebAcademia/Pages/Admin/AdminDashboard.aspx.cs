using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAcademia.Data;

namespace WebAcademia.Pages.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected int TotalUsuarios;
        protected int TotalCursos;
        protected int TotalInscripciones;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioID"] == null || (int)Session["RolID"] != 1)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            using (var db = new AppDBContext())
            {
                TotalUsuarios = db.Usuarios.Count();
                TotalCursos = db.Cursos.Count();
                TotalInscripciones = db.Inscripciones.Count();
            }
        }
    }
}
