<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAcademia.Pages.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Metadatos básicos -->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar Sesión</title>

    <!-- Bootstrap 5 para estilos base -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- FontAwesome para íconos -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <!-- Hoja de estilos personalizada para el Login -->
    <link rel="stylesheet" href="Shared/StyleLogin.css" />

    <!-- SweetAlert2 para alertas modernas -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>

<body>
    <%-- Formulario principal de Login --%>
    <form id="form1" runat="server">
        <%-- Contenedor principal del Login --%>
        <div class="login-card">
            
            <%-- Ícono decorativo de perfil/seguridad --%>
            <div class="profile-icon">
                <i class="fas fa-lock"></i>
            </div>

            <%-- Título --%>
            <h5 class="mb-4 mt-4">Iniciar Sesión</h5>

            <%-- Campo: Correo electrónico --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text">
                        <i class="fas fa-envelope"></i>
                    </span>
                    <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Correo Electrónico" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- Campo: Contraseña --%>
            <div class="mb-3">
                <div class="input-group">
                    <span class="input-group-text">
                        <i class="fas fa-lock"></i>
                    </span>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Contraseña" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </div>

            <%-- Botón Ingresar --%>
            <asp:Button ID="btnLogin" CssClass="btn btn-primary w-100" runat="server" Text="Ingresar" OnClick="btnLogin_Click" />

            <%-- Link para registro de nuevos usuarios --%>
            <div class="card-footer text-center mt-3">
                <small class="text-muted">
                    ¿No tienes cuenta? <a href="Register.aspx">Regístrate aquí</a>
                </small>
            </div>
        </div>
    </form>

    <!-- Bootstrap Bundle JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
