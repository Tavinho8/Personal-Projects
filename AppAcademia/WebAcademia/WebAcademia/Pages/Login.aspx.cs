using System;
using System.Linq;
using System.Web.UI;
using WebAcademia.Data;
using WebAcademia.Helpers;

namespace WebAcademia.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Contexto de base de datos (Entity Framework).
        /// </summary>
        private readonly AppDBContext db = new AppDBContext();

        /// <summary>
        /// Evento que se ejecuta cuando se carga la página.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // No hay lógica especial al cargar.
        }

        /// <summary>
        /// Evento click del botón de Login.
        /// Valida credenciales y redirige según rol.
        /// </summary>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Busca usuario por email y verifica que esté activo.
            var usuario = db.Usuarios.FirstOrDefault(u => u.Email == email && u.Activo);

            if (usuario == null)
            {
                MostrarAlerta("error", "Usuario no encontrado o inactivo", "Verifica tu correo.");
                return;
            }

            // Genera el hash de la contraseña ingresada usando el salt del usuario.
            string hashInput = PasswordHelper.HashPassword(password, usuario.Salt);

            if (usuario.PasswordHash != hashInput)
            {
                MostrarAlerta("error", "Contraseña incorrecta", "La contraseña ingresada no es válida.");
                return;
            }

            // Credenciales correctas → guardar datos de sesión.
            Session["UsuarioID"] = usuario.UsuarioID;
            Session["RolID"] = usuario.RolID;
            Session["Nombre"] = usuario.Nombre;

            // Redirigir según rol del usuario.
            switch (usuario.RolID)
            {
                case 1:
                    Response.Redirect("Admin/AdminDashboard.aspx");
                    break;
                case 2:
                    Response.Redirect("InstructorDashboard.aspx");
                    break;
                case 3:
                    Response.Redirect("Estudiante/EstudianteAdmin.aspx");
                    break;
                default:
                    Response.Redirect("Default.aspx");
                    break;
            }
        }

        /// <summary>
        /// Muestra un mensaje SweetAlert2 con icono, título y texto.
        /// </summary>
        /// <param name="icono">Icono de alerta: success, error, warning, info</param>
        /// <param name="titulo">Título de la alerta</param>
        /// <param name="texto">Texto descriptivo</param>
        private void MostrarAlerta(string icono, string titulo, string texto)
        {
            string script = $@"
                Swal.fire({{
                    icon: '{icono}',
                    title: '{titulo}',
                    text: '{texto}'
                }});";

            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
