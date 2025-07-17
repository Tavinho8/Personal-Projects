using System;
using System.Collections.Generic;
using System.Web.UI;
using WebAcademia.DAL;
using WebAcademia.Models;

namespace WebAcademia.Pages.Admin
{
    public partial class CursosFormAdmin : System.Web.UI.Page
    {
        // Instancias de capas de acceso a datos (DAL)
        private readonly UsuarioDAL usuarioDAL = new UsuarioDAL();
        private readonly CursosDAL cursosDAL = new CursosDAL();

        /// <summary>
        /// Evento que se ejecuta al cargar la página.
        /// Si no es postback, carga instructores y, si hay un ID en query string, carga datos del curso para editar.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarInstructores();

                if (Request.QueryString["id"] != null &&
                    int.TryParse(Request.QueryString["id"], out int cursoID))
                {
                    CargarCursoParaEditar(cursoID);
                }
            }
        }

        /// <summary>
        /// Botón Cancelar: redirige al dashboard de administración.
        /// </summary>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CursosAdmin.aspx");
        }

        /// <summary>
        /// Carga la lista de instructores disponibles desde la base de datos.
        /// </summary>
        private void CargarInstructores()
        {
            var instructores = usuarioDAL.ListadoIntructores();

            ddlInstructor.DataSource = instructores;
            ddlInstructor.DataTextField = "Nombre";         // Campo visible
            ddlInstructor.DataValueField = "UsuarioID";     // Clave primaria
            ddlInstructor.DataBind();

            // Inserta opción por defecto
            ddlInstructor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione un Instructor --", "0"));
        }

        /// <summary>
        /// Evento del botón Guardar: decide si inserta o actualiza según exista ID.
        /// </summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfProductoID.Value))
                ActualizarCurso();
            else
                InsertarCurso();
        }

        /// <summary>
        /// Inserta un nuevo curso en la base de datos.
        /// Valida campos, muestra mensajes SweetAlert2 y redirige al finalizar.
        /// </summary>
        private bool InsertarCurso()
        {
            try
            {
                var errores = ValidarCampos();

                if (errores.Count > 0)
                {
                    MostrarAlerta("warning", "Campos obligatorios", string.Join("<br/>", errores));
                    return false;
                }

                var curso = MapearCursoForm();
                cursosDAL.InsertarCurso(curso);

                MostrarAlertaRedireccion("success", "Curso guardado correctamente", "CursosAdmin.aspx");
                return true;
            }
            catch (Exception ex)
            {
                MostrarAlerta("error", "Error", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Actualiza un curso existente.
        /// Valida campos, muestra mensajes SweetAlert2 y redirige al finalizar.
        /// </summary>
        private bool ActualizarCurso()
        {
            try
            {
                var errores = ValidarCampos();

                if (errores.Count > 0)
                {
                    MostrarAlerta("warning", "Campos obligatorios", string.Join("<br/>", errores));
                    return false;
                }

                if (!int.TryParse(hfProductoID.Value, out int cursoID))
                    throw new Exception("ID del curso no válido.");

                var curso = MapearCursoForm();
                curso.CursoID = cursoID;

                cursosDAL.ActualizarCurso(curso);

                MostrarAlertaRedireccion("success", "Curso actualizado correctamente", "CursosAdmin.aspx");
                return true;
            }
            catch (Exception ex)
            {
                MostrarAlerta("error", "Error", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Carga los datos del curso para edición, usando su ID.
        /// </summary>
        private void CargarCursoParaEditar(int id)
        {
            var curso = cursosDAL.ObtenerPorID(id);
            if (curso != null)
            {
                txtNombre.Text = curso.Nombre;
                txtDescripcion.Text = curso.Descripcion;
                ddlInstructor.SelectedValue = curso.InstructorID.ToString();
                txtCupo.Text = curso.CupoMaximo.ToString();
                chkActivo.Checked = curso.Activo;

                hfProductoID.Value = curso.CursoID.ToString(); // Guarda ID en HiddenField
            }
        }

        /// <summary>
        /// Mapea los campos del formulario a un objeto Curso.
        /// </summary>
        private Curso MapearCursoForm()
        {
            int.TryParse(txtCupo.Text.Trim(), out int cupo);

            return new Curso
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                InstructorID = ddlInstructor.SelectedIndex > 0 ? int.Parse(ddlInstructor.SelectedValue) : 0,
                CupoMaximo = cupo,
                Activo = chkActivo.Checked
            };
        }

        /// <summary>
        /// Valida los campos obligatorios del formulario.
        /// </summary>
        private List<string> ValidarCampos()
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                errores.Add("⚠️ El nombre del curso es obligatorio");

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                errores.Add("⚠️ La descripción del curso es obligatoria");

            if (string.IsNullOrWhiteSpace(txtCupo.Text))
                errores.Add("⚠️ El cupo máximo del curso es obligatorio");

            if (ddlInstructor.SelectedIndex <= 0)
                errores.Add("⚠️ Debe seleccionar un Instructor.");

            if (!int.TryParse(txtCupo.Text.Trim(), out int cupo) || cupo <= 0)
                errores.Add("⚠️ El Cupo Máximo debe ser un número mayor a cero.");

            return errores;
        }

        /// <summary>
        /// Muestra un SweetAlert2 simple sin redirección.
        /// </summary>
        private void MostrarAlerta(string icono, string titulo, string mensaje)
        {
            string script = $"Swal.fire({{ icon: '{icono}', title: '{titulo}', html: '{mensaje}' }});";
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

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
        }
    }
}
