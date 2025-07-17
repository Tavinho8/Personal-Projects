<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuariosFormAdmin.aspx.cs" Inherits="WebAcademia.Pages.Admin.UsuariosFormAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Usuario Formulario</title>

    <!-- Bootstrap CSS para estilos base -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- FontAwesome para íconos -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <!-- SweetAlert2 para confirmaciones -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- Hoja de estilos personalizada para el formulario de cursos -->
    <link rel="stylesheet" href="../Shared/StyleFormAdmin.css" />
</head>

<body>
    <%-- Formulario principal ASP.NET --%>
    <form id="form1" runat="server">
        <%-- Contenedor principal del formulario --%>
        <div class="form-card">

            <%-- Ícono decorativo superior --%>
            <div class="profile-icon text-center">
                <i class="fas fa-user-circle fa-3x"></i>
            </div>

            <%-- Título del formulario --%>
            <h5 class="mb-4 mt-4 text-center">Usuario</h5>

            <%-- Label para mostrar mensajes del servidor (éxito/error) --%>
            <asp:Label ID="lblMensaje" runat="server" CssClass="form-text fw-bold mb-3 text-center d-block"></asp:Label>

            <%-- Campo oculto para almacenar el ID del usuario (modo editar) --%>
            <asp:HiddenField ID="hfProductoID" runat="server" />

            <%-- Campo: Nombre del Usuario --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-user"></i></span>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Campo: Email del Usuario --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-envelope"></i></span>
                    <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Correo electrónico" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Campo: Password del Usuario --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-lock"></i></span>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Contraseña" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Campo: Rol (DropDownList) --%>
            <div class="mb-4">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-user-tag"></i></span>
                    <asp:DropDownList ID="ddlRol" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
            </div>

            <%-- CheckBox: Usuario activo o inactivo --%>
            <div class="mb-4 text-start">
                <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input me-2" />
                <label for="chkActivo" class="form-check-label">Activo</label>
            </div>

            <%-- Botones de acción: Guardar y Cancelar --%>
            <div class="d-flex justify-content-between">
                <asp:LinkButton
                    ID="btnGuardar"
                    runat="server"
                    CssClass="btn btn-primary w-50 me-2"
                    OnClick="btnGuardar_Click">
                    <i class="fas fa-save me-1"></i> Guardar
                </asp:LinkButton>

                <asp:LinkButton
                    ID="btnCancelar"
                    runat="server"
                    CssClass="btn btn-secondary w-50"
                    OnClick="btnCancelar_Click">
                    <i class="fas fa-times me-1"></i> Cancelar
                </asp:LinkButton>
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
