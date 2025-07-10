using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAcademia.DAL;

namespace WebAcademia.Pages.Admin
{
    public partial class CursosAdmin : System.Web.UI.Page
    {
        // Instancia única de la DAL
        private readonly CursosDAL cursosDAL = new CursosDAL();

        /// <summary>
        /// Evento de carga de página.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarInstructores();
                CargarCursos();
            }
        }

        /// <summary>
        /// Carga instructores activos para el filtro.
        /// </summary>
        private void CargarInstructores()
        {
            try
            {
                using (var contexto = new WebAcademia.Data.AppDBContext())
                {
                    ddlInstructor.DataSource = contexto.Usuarios
                        .Where(u => u.RolID == 2 && u.Activo == true)
                        .Select(u => new { u.UsuarioID, u.Nombre })
                        .ToList();

                    ddlInstructor.DataTextField = "Nombre";
                    ddlInstructor.DataValueField = "UsuarioID";
                    ddlInstructor.DataBind();

                    ddlInstructor.Items.Insert(0, new ListItem("-- Todos --", ""));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar instructores: " + ex.Message;
                lblMensaje.CssClass = "text-danger fw-bold";
            }
        }

        /// <summary>
        /// Carga la lista de cursos aplicando filtros y paginación.
        /// </summary>
        private void CargarCursos()
        {
            try
            {
                // ✅ Leer filtros
                string nombre = txtBuscarNombre.Text.Trim();
                bool? activo = null;
                int? instructorId = null;

                if (!string.IsNullOrEmpty(ddlActivo.SelectedValue))
                {
                    activo = bool.Parse(ddlActivo.SelectedValue);
                }

                if (!string.IsNullOrEmpty(ddlInstructor.SelectedValue))
                {
                    instructorId = int.Parse(ddlInstructor.SelectedValue);
                }

                // ✅ Obtener cursos filtrados
                var cursos = cursosDAL.ObtenerCursosFiltrados(nombre, activo, instructorId);

                // ✅ Configurar tamaño de página
                gvCursos.PageSize = int.Parse(ddlPageSize.SelectedValue);

                // ✅ Vincular datos
                gvCursos.DataSource = cursos;
                gvCursos.DataBind();

                // ✅ Mostrar total
                lblTotalRegistros.Text = $"Total de cursos encontrados: {cursos.Count}";
                lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los cursos: " + ex.Message;
                lblMensaje.CssClass = "text-danger fw-bold";
            }
        }

        /// <summary>
        /// Evento botón Buscar.
        /// </summary>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvCursos.PageIndex = 0; // Reiniciar página
            CargarCursos();
        }

        /// <summary>
        /// Evento botón Limpiar.
        /// </summary>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Text = string.Empty;
            ddlActivo.SelectedIndex = 0;
            ddlInstructor.SelectedIndex = 0;

            gvCursos.PageIndex = 0; // Reiniciar página
            CargarCursos();
        }

        /// <summary>
        /// Cambia de página en el GridView.
        /// </summary>
        protected void gvCursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCursos.PageIndex = e.NewPageIndex;
            CargarCursos();
        }

        /// <summary>
        /// Cambia el tamaño de página.
        /// </summary>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCursos.PageIndex = 0; // Reiniciar página
            CargarCursos();
        }

        /// <summary>
        /// Elimina un curso.
        /// </summary>
        protected void gvCursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                if (int.TryParse(e.CommandArgument.ToString(), out int idCurso))
                {
                    try
                    {
                        cursosDAL.EliminarCurso(idCurso);
                        CargarCursos();

                        string script = @"Swal.fire({
                            icon: 'success',
                            title: 'Curso eliminado correctamente'
                        });";
                        ClientScript.RegisterStartupScript(this.GetType(), "CursoEliminado", script, true);
                    }
                    catch (Exception ex)
                    {
                        string script = $"Swal.fire({{ icon: 'error', title: 'Error', text: '{ex.Message}' }});";
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorEliminar", script, true);
                    }
                }
                else
                {
                    lblMensaje.Text = "ID de curso inválido.";
                    lblMensaje.CssClass = "text-danger fw-bold";
                }
            }
        }
    }
}
