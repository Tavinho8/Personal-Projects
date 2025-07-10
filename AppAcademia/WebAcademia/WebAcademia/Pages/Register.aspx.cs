using System;
using System.Linq;
using System.Web.UI;
using WebAcademia.Data;
using WebAcademia.Helpers;
using WebAcademia.Models;

namespace WebAcademia.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        /// <summary>
        /// Contexto EF para acceder a la base de datos.
        /// </summary>
        private readonly AppDBContext db = new AppDBContext();

        /// <summary>
        /// Evento que se ejecuta al cargar la página.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
        }

        /// <summary>
        /// Llena el DropDownList con los roles disponibles.
        /// </summary>
        private void CargarRoles()
        {
            ddlRol.DataSource = db.Roles.ToList();
            ddlRol.DataTextField = "NombreRol";
            ddlRol.DataValueField = "RolID";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Selecciona un Rol --", "0"));
        }

        /// <summary>
        /// Evento click para registrar usuario.
        /// Valida, verifica duplicados y guarda.
        /// </summary>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            int.TryParse(ddlRol.SelectedValue, out int rolId);

            // Validar campos obligatorios.
            var errores = ValidarCampos(nombre, email, password, rolId);

            if (errores.Length > 0)
            {
                MostrarAlerta("warning", "Campos obligatorios", errores);
                return;
            }

            // Validar correo duplicado.
            if (db.Usuarios.Any(u => u.Email == email))
            {
                MostrarAlerta("error", "Correo duplicado", "El correo ingresado ya está registrado.");
                return;
            }

            // Generar hash y salt.
            string salt = PasswordHelper.GenerateSalt();
            string hash = PasswordHelper.HashPassword(password, salt);

            Usuario nuevo = new Usuario
            {
                Nombre = nombre,
                Email = email,
                PasswordHash = hash,
                Salt = salt,
                RolID = rolId,
                Activo = true
            };

            db.Usuarios.Add(nuevo);
            db.SaveChanges();

            // Mostrar alerta de éxito y redirigir.
            string script = @"
                Swal.fire({
                    icon: 'success',
                    title: '¡Registro exitoso!',
                    text: 'Tu usuario fue creado correctamente.'
                }).then(() => {
                    window.location.href = 'Login.aspx';
                });
            ";

            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        /// <summary>
        /// Valida campos obligatorios y retorna texto de error concatenado.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="rolId"></param>
        /// <returns></returns>
        private string ValidarCampos(string nombre, string email, string password, int rolId)
        {
            string errores = "";

            if (string.IsNullOrEmpty(nombre))
                errores += "⚠️ El nombre es obligatorio.<br/>";
            if (string.IsNullOrEmpty(email))
                errores += "⚠️ El correo es obligatorio.<br/>";
            if (string.IsNullOrEmpty(password))
                errores += "⚠️ La contraseña es obligatoria.<br/>";
            if (rolId <= 0)
                errores += "⚠️ Debes seleccionar un rol válido.<br/>";

            return errores;
        }

        /// <summary>
        /// Método helper para mostrar SweetAlert2.
        /// </summary>
        private void MostrarAlerta(string icono, string titulo, string mensaje)
        {
            string script = $@"
                Swal.fire({{
                    icon: '{icono}',
                    title: '{titulo}',
                    html: '{mensaje}'
                }});";

            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
