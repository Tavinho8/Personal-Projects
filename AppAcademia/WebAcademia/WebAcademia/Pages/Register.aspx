<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebAcademia.Pages.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- Metadatos básicos --%>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro Usuario</title>

    <%-- Bootstrap 5 para estilos base --%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <%-- FontAwesome para íconos --%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <%-- Hoja de estilos personalizada --%>
    <link rel="stylesheet" href="Shared/StyleRegister.css" />

    <%-- SweetAlert2 para alertas elegantes --%>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>

<body>
    <%-- Formulario de Registro --%>
    <form id="form1" runat="server">
        <div class="register-card">
            
            <%-- Ícono decorativo --%>
            <div class="profile-icon">
                <i class="fas fa-user"></i>
            </div>

            <%-- Título --%>
            <h5 class="mb-4 mt-4">Crear Cuenta</h5>

            <%-- Nombre --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-user"></i></span>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre Completo" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Correo --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-envelope"></i></span>
                    <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Correo Electrónico" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Contraseña --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-lock"></i></span>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Contraseña" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </div>

            <%-- Rol --%>
            <div class="mb-4">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-user-tag"></i></span>
                    <asp:DropDownList ID="ddlRol" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
            </div>

            <%-- Botón Registrar: se intercepta con confirmación --%>
            <asp:Button 
                ID="btnRegister" 
                CssClass="btn btn-primary w-100" 
                runat="server" 
                Text="Registrarse" 
                OnClick="btnRegister_Click" />

            <%-- Link para usuarios existentes --%>
            <div class="card-footer text-center mt-3">
                <small class="text-muted">
                    ¿Ya tienes cuenta? <a href="Login.aspx">Inicia sesión</a>
                </small>
            </div>
        </div>
    </form>

    <%-- Bootstrap Bundle --%>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <%-- Script para interceptar registro con SweetAlert2 --%>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const registerBtn = document.getElementById('<%= btnRegister.ClientID %>');

            // Guarda cualquier onclick de ASP.NET
            const originalClick = registerBtn.onclick;

            registerBtn.addEventListener("click", function handler(e) {
                e.preventDefault();

                Swal.fire({
                    title: "¿Estás seguro?",
                    text: "¿Deseas registrar este usuario?",
                    icon: "question",
                    showCancelButton: true,
                    confirmButtonText: "Sí, registrar",
                    cancelButtonText: "Cancelar"
                }).then((result) => {
                    if (result.isConfirmed) {
                        registerBtn.removeEventListener("click", handler);
                        registerBtn.click();
                    }
                });
            });
        });
    </script>
</body>
</html>
