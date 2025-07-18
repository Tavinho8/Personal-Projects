using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAcademia.DAL;
using WebAcademia.Pages.Shared.Clases;

namespace WebAcademia.Pages.Estudiante
{
    /// <summary>
    /// Página de administración de cursos para el estudiante.
    /// Hereda de BaseCursosPage para reutilizar la lógica de carga, búsqueda y paginación de cursos.
    /// </summary>
    public partial class EstudianteAdmin : BaseCursosPage
    {
        /// <summary> Total de cursos disponibles, se puede usar para mostrar estadísticas. </summary>
        protected int TotalCursosDisponibles = 0;

        /// <summary> Total de cursos en los que el estudiante está inscrito. </summary>
        protected int MisInscripciones = 0;

        /// <summary> Nombre del estudiante actual. </summary>
        protected string nombreEstudiante;

        // Instancia de la capa de acceso a datos de usuario.
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        /// <summary>
        /// Evento que se ejecuta al cargar la página. 
        /// Valida la sesión, obtiene datos del estudiante y carga los cursos disponibles.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioID"] == null)
            {
                Response.Redirect("../Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int ID = (int)Session["UsuarioID"];
                nombreEstudiante = usuarioDAL.GetUsuario(ID).Nombre;
                CargarCursos();
            }
        }

        /// <summary>
        /// Llama al método heredado para cargar los cursos filtrados.
        /// </summary>
        private void CargarCursos()
        {
            CargarCursos(gvCursos, lblTotalRegistros, lblMensaje, txtBuscarNombre, ddlActivo, ddlInstructor, ddlPageSize);
        }

        /// <summary>
        /// Evento de botón para aplicar filtros de búsqueda.
        /// </summary>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCursos(gvCursos, lblTotalRegistros, lblMensaje, txtBuscarNombre, ddlActivo, ddlInstructor, ddlPageSize);
        }

        /// <summary>
        /// Evento de botón para limpiar los filtros de búsqueda y recargar todos los cursos.
        /// </summary>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFiltros(gvCursos, lblTotalRegistros, lblMensaje, txtBuscarNombre, ddlActivo, ddlInstructor, ddlPageSize);
        }

        /// <summary>
        /// Evento que se dispara cuando se cambia de página en el GridView.
        /// </summary>
        protected void gvCursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CambiarPagina(gvCursos, lblTotalRegistros, lblMensaje, txtBuscarNombre, ddlActivo, ddlInstructor, ddlPageSize, e);
        }

        /// <summary>
        /// Evento que se dispara cuando se cambia la cantidad de cursos mostrados por página.
        /// </summary>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambiarTamanioPagina(gvCursos, lblTotalRegistros, lblMensaje, txtBuscarNombre, ddlActivo, ddlInstructor, ddlPageSize);
        }

        /// <summary>
        /// Evento para manejar acciones personalizadas en el GridView (como ver más información o inscribirse).
        /// </summary>
        protected void gvCursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Aquí podrías implementar lógica para inscribir al estudiante o ver detalles del curso.
            // Ejemplo:
            // if (e.CommandName == "Inscribirse")
            // {
            //     int rowIndex = Convert.ToInt32(e.CommandArgument);
            //     int cursoId = (int)gvCursos.DataKeys[rowIndex].Value;
            //     // Lógica para inscribir al estudiante...
            // }
        }
    }
}
