using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAcademia.DAL;
using WebAcademia.Models;

namespace WebAcademia.Pages.Admin
{
    public partial class UsuariosAdmin : System.Web.UI.Page
    {
        //Instancia para la DAL de Usuarios
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        /// <summary>
        /// Evento de carga de página.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        /// <summary>
        /// Carga la lista de Usuario aplicando filtros y paginación.
        /// </summary>
        public void CargarUsuarios()
        {
            try
            {

                bool? activo = null;
                if (!string.IsNullOrEmpty(ddlActivo.SelectedValue) && ddlActivo.SelectedValue != "--Seleccione--")
                {
                    activo = bool.Parse(ddlActivo.SelectedValue);
                }

                var usuarios = usuarioDAL.GetUsuariosFiltrados(
                    nombre: txtBuscarNombre.Text.Trim(),
                    email: txtBuscarEmail.Text.Trim(), // Corregir aquí
                    activo: activo
                );

                if (int.TryParse(ddlPageSize.SelectedValue, out int pageSize))
                {
                    gvUsuarios.PageSize = pageSize;
                }
                else
                {
                    gvUsuarios.PageSize = 5; // fallback seguro
                }


                gvUsuarios.DataSource = usuarios;
                gvUsuarios.DataBind();

                lblTotalRegistros.Text = $"Total de usuarios encontrados: {usuarios.Count}";
                lblMensaje.Visible = false;
                lblMensaje.Text = "";

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar los usuarios: " + ex.Message);

            }
        }//CargarUsuarios

        /// <summary>
        /// Evento botón Buscar.
        /// </summary>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvUsuarios.PageIndex = 0; // Reiniciar página
            CargarUsuarios();
        }//btnBuscar_Click

        /// <summary>
        /// Evento botón Limpiar.
        /// </summary>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Text = string.Empty;
            ddlActivo.SelectedIndex = 0;
            txtBuscarEmail.Text = string.Empty;

            gvUsuarios.PageIndex = 0; // Reiniciar página
            CargarUsuarios();
        }//btnLimpiar_Click

        /// <summary>
        /// Cambia de página en el GridView.
        /// </summary>
        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
            CargarUsuarios();
        }

        /// <summary>
        /// Cambia el tamaño de página.
        /// </summary>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvUsuarios.PageIndex = 0; // Reiniciar página
            CargarUsuarios();
        }

        /// <summary>
        /// Elimina el usuario apartir de su ID
        /// </summary>
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                if (int.TryParse(e.CommandArgument.ToString(), out int idUsuario))
                {
                    try
                    {
                        usuarioDAL.DeleteUsuario(idUsuario);
                        CargarUsuarios();

                        string script = @"Swal.fire({
                            icon: 'success',
                            title: 'Usuario eliminado correctamente'
                        });";
                        ClientScript.RegisterStartupScript(this.GetType(), "UsuarioEliminado", script, true);
                    }
                    catch (Exception ex)
                    {
                        string safeMsg = ex.Message.Replace("'", "\\'");
                        string script = $"Swal.fire({{ icon: 'error', title: 'Error', text: '{safeMsg}' }});";
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorEliminar", script, true);
                    }

                }
                else
                {
                    MostrarMensaje("ID de Usuario inválido.");
                }
            }
        }//gvUsuarios_RowCommand

        /// <summary>
        /// Muestra mensaje de acuerdo a los parametros
        /// </summary>
        private void MostrarMensaje(string mensaje, string claseCss = "text-danger fw-bold")
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = claseCss;
            lblMensaje.Visible = true;
        }

    }//class
}