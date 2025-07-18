using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAcademia.DAL;
using WebAcademia.Data;

namespace WebAcademia.Pages.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        // Variables para almacenar los totales de usuarios, cursos e inscripciones
        protected int TotalUsuarios;
        protected int TotalCursos;
        protected int TotalInscripciones;

        // Instancia del DAL para acceder a los cursos
        CursosDAL cursoDAL = new CursosDAL();
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
            CargarUltimosCursos();
        }
        private void CargarUltimosCursos()
        {
            // Supón que tienes un método en tu DAL que retorna los últimos cursos
            var cursos = cursoDAL.ObtenerUltimosCursos(10); // Por ejemplo, los 5 más recientes
            rptUltimosCursos.DataSource = cursos;
            rptUltimosCursos.DataBind();
        }//CargarUltimosCursos


    }//class
}
