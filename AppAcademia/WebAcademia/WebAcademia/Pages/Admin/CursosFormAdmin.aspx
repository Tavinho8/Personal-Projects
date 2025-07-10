<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CursosFormAdmin.aspx.cs" Inherits="WebAcademia.Pages.Admin.CursosFormAdmin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Metadatos básicos de la página -->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Curso Formulario</title>

    <!-- Bootstrap CSS para estilos base -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- FontAwesome para íconos -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <!-- SweetAlert2 para confirmaciones -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- Hoja de estilos personalizada para el formulario de cursos -->
    <link rel="stylesheet" href="../Shared/StyleCursosFormAdmin.css" />
</head>

<body>
    <%-- Formulario principal ASP.NET --%>
    <form id="form1" runat="server">
        <%-- Contenedor principal del formulario --%>
        <div class="form-card">

            <%-- Ícono decorativo superior --%>
            <div class="profile-icon">
                <i class="fas fa-book"></i>
            </div>

            <%-- Título del formulario --%>
            <h5 class="mb-4 mt-4">Curso</h5>

            <%-- Label para mostrar mensajes del servidor (éxito/error) --%>
            <asp:Label ID="lblMensaje" runat="server" CssClass="form-text fw-bold mb-3"></asp:Label>

            <%-- Campo oculto para almacenar el ID del curso (modo editar) --%>
            <asp:HiddenField ID="hfProductoID" runat="server" />

            <%-- Campo: Nombre del curso --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-book"></i></span>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre Curso" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Campo: Descripción del curso --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-align-left"></i></span>
                    <asp:TextBox ID="txtDescripcion" CssClass="form-control" placeholder="Descripción" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Campo: Cupo máximo del curso --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-users"></i></span>
                    <asp:TextBox ID="txtCupo" CssClass="form-control" placeholder="Cupo Máximo" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Campo: Instructor (DropDownList) --%>
            <div class="mb-4">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-user-tie"></i></span>
                    <asp:DropDownList ID="ddlInstructor" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
            </div>

            <%-- CheckBox: Curso activo o inactivo --%>
            <div class="mb-4 text-start">
                <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" />
                Activo
            </div>

            <%-- Botones de acción: Guardar y Cancelar --%>
            <div class="d-flex justify-content-between">
                <%-- Botón Guardar: ejecuta validación de confirmación y luego postback --%>
                <asp:Button
                    ID="btnGuardar"
                    runat="server"
                    Text="Guardar"
                    CssClass="btn btn-primary w-50 me-2"
                    OnClick="btnGuardar_Click" />

                <%-- Botón Cancelar: redirige o limpia datos --%>
                <asp:Button
                    ID="btnCancelar"
                    CssClass="btn btn-secondary w-50"
                    runat="server"
                    Text="Cancelar"
                    OnClick="btnCancelar_Click" />
            </div>
        </div>
    </form>

    <!-- Bootstrap JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <%-- Script para interceptar el clic en Guardar y confirmar con SweetAlert2 --%>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            // Obtiene el botón Guardar por su ID de cliente generado
            const guardarBtn = document.getElementById('<%= btnGuardar.ClientID %>');

            // Guarda cualquier onclick generado por ASP.NET
            const originalClick = guardarBtn.onclick;

            // Handler para interceptar el clic y mostrar SweetAlert2
            guardarBtn.addEventListener("click", function handler(e) {
                e.preventDefault(); // Evita postback inmediato

                Swal.fire({
                    title: "¿Estás seguro?",
                    text: "¿Deseas guardar este curso?",
                    icon: "question",
                    showCancelButton: true,
                    confirmButtonText: "Sí, guardar",
                    cancelButtonText: "Cancelar"
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Remueve el handler para evitar bucle infinito
                        guardarBtn.removeEventListener("click", handler);
                        // Relanza el clic original para disparar postback real de ASP.NET
                        guardarBtn.click();
                    }
                });
            });
        });
    </script>
</body>
</html>
