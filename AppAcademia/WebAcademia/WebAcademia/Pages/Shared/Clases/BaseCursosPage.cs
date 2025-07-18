using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebAcademia.DAL;

namespace WebAcademia.Pages.Shared.Clases
{
    /// <summary>
    /// Clase base abstracta que proporciona funcionalidad común para la gestión de cursos
    /// en páginas que heredan de esta. Centraliza operaciones relacionadas con la carga,
    /// búsqueda, paginación y limpieza de filtros en un GridView de cursos.
    /// </summary>
    public abstract class BaseCursosPage : System.Web.UI.Page
    {
        /// <summary>
        /// Objeto de acceso a datos (DAL) para interactuar con la capa de datos de los cursos.
        /// </summary>
        protected CursosDAL cursosDAL = new CursosDAL();

        /// <summary>
        /// Carga los cursos filtrados en un GridView, con base en los valores de los controles proporcionados.
        /// También actualiza las etiquetas de total de cursos y de mensajes.
        /// </summary>
        protected void CargarCursos(GridView grid, Label lblTotal, Label lblMensaje,
                                    TextBox txtNombre, DropDownList ddlActivo, DropDownList ddlInstructor, DropDownList ddlPageSize)
        {
            try
            {
                // Obtener los filtros ingresados por el usuario
                string nombre = txtNombre.Text.Trim();
                bool? activo = string.IsNullOrEmpty(ddlActivo.SelectedValue)
                               ? null
                               : (bool?)bool.Parse(ddlActivo.SelectedValue);
                int? instructorId = string.IsNullOrEmpty(ddlInstructor.SelectedValue)
                                    ? null
                                    : (int?)int.Parse(ddlInstructor.SelectedValue);

                // Obtener los cursos desde la base de datos usando filtros
                var cursos = cursosDAL.ObtenerCursosFiltrados(nombre, activo, instructorId);

                // Configurar el tamaño de página del GridView
                grid.PageSize = int.Parse(ddlPageSize.SelectedValue);

                // Asignar los datos y enlazarlos al GridView
                grid.DataSource = cursos;
                grid.DataBind();

                // Mostrar el total de cursos encontrados
                lblTotal.Text = $"Total de cursos encontrados: {cursos.Count}";
                lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error si ocurre alguna excepción
                lblMensaje.Text = "Error al cargar los cursos: " + ex.Message;
                lblMensaje.CssClass = "text-danger fw-bold";
            }
        }

        /// <summary>
        /// Reinicia la paginación y vuelve a cargar los cursos con los filtros actuales.
        /// </summary>
        protected void BuscarCursos(GridView grid, Label lblTotal, Label lblMensaje,
                                    TextBox txtNombre, DropDownList ddlActivo, DropDownList ddlInstructor, DropDownList ddlPageSize)
        {
            grid.PageIndex = 0;
            CargarCursos(grid, lblTotal, lblMensaje, txtNombre, ddlActivo, ddlInstructor, ddlPageSize);
        }

        /// <summary>
        /// Limpia todos los filtros del formulario y vuelve a cargar los cursos.
        /// </summary>
        protected void LimpiarFiltros(GridView grid, Label lblTotal, Label lblMensaje,
                                      TextBox txtNombre, DropDownList ddlActivo, DropDownList ddlInstructor, DropDownList ddlPageSize)
        {
            txtNombre.Text = string.Empty;
            ddlActivo.SelectedIndex = 0;
            ddlInstructor.SelectedIndex = 0;
            grid.PageIndex = 0;
            CargarCursos(grid, lblTotal, lblMensaje, txtNombre, ddlActivo, ddlInstructor, ddlPageSize);
        }

        /// <summary>
        /// Cambia la página actual del GridView y recarga los cursos con los filtros actuales.
        /// </summary>
        protected void CambiarPagina(GridView grid, Label lblTotal, Label lblMensaje,
                                     TextBox txtNombre, DropDownList ddlActivo, DropDownList ddlInstructor, DropDownList ddlPageSize,
                                     GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            CargarCursos(grid, lblTotal, lblMensaje, txtNombre, ddlActivo, ddlInstructor, ddlPageSize);
        }

        /// <summary>
        /// Cambia el tamaño de página (cantidad de elementos por página) del GridView.
        /// </summary>
        protected void CambiarTamanioPagina(GridView grid, Label lblTotal, Label lblMensaje,
                                            TextBox txtNombre, DropDownList ddlActivo, DropDownList ddlInstructor, DropDownList ddlPageSize)
        {
            grid.PageIndex = 0;
            CargarCursos(grid, lblTotal, lblMensaje, txtNombre, ddlActivo, ddlInstructor, ddlPageSize);
        }
    }//class
}
