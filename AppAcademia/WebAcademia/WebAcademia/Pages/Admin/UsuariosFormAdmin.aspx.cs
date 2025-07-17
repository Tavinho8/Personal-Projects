using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAcademia.DAL;
using WebAcademia.Helpers;
using WebAcademia.Models;

namespace WebAcademia.Pages.Admin
{
    public partial class UsuariosFormAdmin : System.Web.UI.Page
    {
        // Instancias de capas de acceso a datos (DAL)
        RolesDAL rolesDAL = new RolesDAL();
        UsuarioDAL usuarioDAL = new UsuarioDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();

                if (Request.QueryString["id"] != null &&
                    int.TryParse(Request.QueryString["id"], out int cursoID))
                {
                    CargarUsuarioParaEditar(cursoID);
                }
            }
        }

        /// <summary>
        /// Botón Cancelar: redirige al dashboard de administración.
        /// </summary>
        protected void btnCancelar_Click(object sender, EventArgs e)
        { Response.Redirect("UsuariosAdmin.aspx");  }

        /// <summary>
        /// Evento del botón Guardar: decide si inserta o actualiza según exista ID.
        /// </summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfProductoID.Value))
                ActualizarUsuario();
            else
                InsertarUsuario();
        }//btnGuardar_Click

        /// <summary>
        /// Carga la lista de roles disponibles desde la base de datos al dropList.
        /// </summary>
        private void CargarRoles()
        {
            var roles = rolesDAL.getRoles();
            ddlRol.DataSource = roles;
            ddlRol.DataTextField = "NombreRol";
            ddlRol.DataValueField = "RolID";
            ddlRol.DataBind();

            ddlRol.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione un Usuario --", "0"));
        }//CargarUsuarios

        /// <summary>
        /// Inserta un nuevo Usuario en la base de datos.
        /// Valida campos, muestra mensajes SweetAlert2 y redirige al finalizar.
        /// </summary>
        private bool InsertarUsuario()
        {
            try
            {
                var errores = ValidarCampos();

                if (errores.Count > 0)
                {
                    MostrarAlerta("warning", "Campos obligatorios", string.Join("<br/>", errores));
                    return false;
                }

                var usuario = MapearUsuarioForm();
                usuarioDAL.InsertUsuario(usuario);

                MostrarAlertaRedireccion("success", "Usuario guardado correctamente", "UsuariosAdmin.aspx");
                return true;
            }
            catch (Exception ex)
            {
                MostrarAlerta("error", "Error", ex.Message);
                return false;
            }
        }//InsertarUsuario

        /// <summary>
        /// Actualiza un curso existente.
        /// Valida campos, muestra mensajes SweetAlert2 y redirige al finalizar.
        /// </summary>
        private bool ActualizarUsuario()
        {
            try
            {
                var errores = ValidarCampos();

                if (errores.Count > 0)
                {
                    MostrarAlerta("warning", "Campos obligatorios", string.Join("<br/>", errores));
                    return false;
                }

                if (!int.TryParse(hfProductoID.Value, out int usuarioID))
                    throw new Exception("ID del Usuario no válido.");

                var usuario = MapearUsuarioForm();
                usuario.UsuarioID = usuarioID;

                usuarioDAL.UpdateUsuario(usuario);

                MostrarAlertaRedireccion("success", "Usuario Actualizado correctamente", "UsuariosAdmin.aspx");
                return true;
            }
            catch (Exception ex)
            {
                MostrarAlerta("error", "Error", ex.Message);
                return false;
            }
        }//ActualizarUsuario

        /// <summary>
        /// Carga los datos del Usuario para edición, usando su ID.
        /// </summary>
        private void CargarUsuarioParaEditar(int id)
        {
            var usuario = usuarioDAL.GetUsuario(id);
            if(usuario != null)
            {
                txtNombre.Text = usuario.Nombre;
                txtEmail.Text = usuario.Email;
                txtPassword.Text = usuario.PasswordHash;
                ddlRol.SelectedValue = usuario.RolID.ToString();               
                chkActivo.Checked = usuario.Activo;
                hfProductoID.Value = usuario.UsuarioID.ToString(); // Guarda ID en HiddenField
            }
        }//CargarUsuarioParaEditar




        /// <summary>
        /// Mapea los campos del formulario a un objeto Usuario.
        /// </summary>
        private Usuario MapearUsuarioForm()
        {
            // Generar hash y salt.
            string salt = PasswordHelper.GenerateSalt();
            string hash = PasswordHelper.HashPassword(txtPassword.Text, salt);

            return new Usuario
            {
                Nombre = txtNombre.Text,
                Email = txtEmail.Text,
                PasswordHash = hash,
                Salt = salt,
                RolID = ddlRol.SelectedIndex > 0 ? int.Parse(ddlRol.SelectedValue) : 0,
                Activo = chkActivo.Checked
            };
        }//MapearUsuarioForm

        /// <summary>
        /// Valida los campos obligatorios del formulario.
        /// nombre, corre, password, ddlRol
        /// </summary>
        private List<string> ValidarCampos()
        {
            var errores = new List<string>();

            if (string.IsNullOrEmpty(txtNombre.Text))
                errores.Add("⚠️ El Nombre del usuario es obligatorio");

            if (string.IsNullOrEmpty(txtEmail.Text))
                errores.Add("⚠️ El Email del usuario es obligatorio");

            if (string.IsNullOrEmpty(txtPassword.Text))
                errores.Add("⚠️ El Password del usuario es obligatorio");

            if (ddlRol.SelectedIndex <= 0)
                errores.Add("⚠️ Debe seleccionar un Usuario.");
            return errores;
        }//ValidarCampos

        /// <summary>
        /// Muestra un SweetAlert2 simple sin redirección.
        /// </summary>
        private void MostrarAlerta(string icono, string titulo, string mensaje)
        {
            string script = $"Swal.fire({{ icon: '{icono}', title: '{titulo}', html: '{mensaje}' }});";
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }//MostrarAlerta

        /// <summary>
        /// Muestra un SweetAlert2 y redirige al cerrar.
        /// </summary>
        private void MostrarAlertaRedireccion(string icono, string titulo, string url)
        {
            string script = $@"
                Swal.fire({{
                    icon: '{icono}',
                    title: '{titulo}'
                }}).then(() => {{
                    window.location.href = '{url}';
                }});";
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }//MostrarAlertaRedireccion

    }//class
}