<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="WebAcademia.Pages.Admin.AdminDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Admin Dashboard</title>
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- FontAwesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <!-- Hoja estilo  -->
    <link href="../Shared/StyleAdmin/StyleAdminDashboard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- Sidebar -->
        <nav class="sidebar" id="sidebar">
            <div class="logo"><span>WebAcademia Admin</span></div>
            <a href="AdminDashboard.aspx"><i class="fas fa-home me-2"></i><span>Dashboard</span></a>
            <a href="CursosAdmin.aspx"><i class="fas fa-book me-2"></i><span>Cursos</span></a>
            <a href="#"><i class="fas fa-user-graduate me-2"></i><span>Estudiantes</span></a>
            <a href="#"><i class="fas fa-chalkboard-teacher me-2"></i><span>Inscripciones</span></a>
            <a href="../Logout.aspx"><i class="fas fa-sign-out-alt me-2"></i><span>Cerrar Sesión</span></a>
        </nav>


        <!-- Topbar -->
        <header class="topbar" id="topbar">
            <button type="button" class="btn-toggle" id="toggleSidebar" aria-label="Toggle sidebar">
                <i class="fas fa-bars"></i>
            </button>
            <h5 class="ms-3 mb-0">Bienvenido, Admin</h5>
        </header>

        <!-- Main Content -->
        <main class="content" id="content">
            <div class="stats-box">
                <div class="card stats-usuarios">
                    <i class="fas fa-users"></i>
                    <div>
                        <h6>Usuarios Totales</h6>
                        <h3><%= TotalUsuarios %></h3>
                    </div>
                </div>
                <div class="card stats-cursos">
                    <i class="fas fa-book"></i>
                    <div>
                        <h6>Cursos Activos</h6>
                        <h3><%= TotalCursos %></h3>
                    </div>
                </div>
                <div class="card stats-inscripciones">
                    <i class="fas fa-user-graduate"></i>
                    <div>
                        <h6>Inscripciones</h6>
                        <h3><%= TotalInscripciones %></h3>
                    </div>
                </div>
            </div>

            <section class="mb-4">
                <h5>Acciones Rápidas</h5>
                <a href="CursosFormAdmin.aspx" class="btn btn-orange me-2 mb-2">Agregar Curso</a>
                <a href="UsuariosAdmin.aspx" class="btn btn-primary mb-2">Gestionar Usuarios</a>
            </section>

            <section class="card-box mb-4 p-4 bg-white rounded shadow-sm">
                <h5>Últimos Cursos Creados</h5>
                <div class="table-responsive">
                    <table class="table table-striped table-hover align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>Curso</th>
                                <th>Instructor</th>
                                <th>Cupo Máximo</th>
                                <th>Estado</th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- Aquí tu loop para mostrar cursos -->
                        </tbody>
                    </table>
                </div>
            </section>
        </main>
    </form>

    <!-- Scripts -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/js/all.min.js"></script>
    <script>
        (() => {
            const toggleBtn = document.getElementById('toggleSidebar');
            const sidebar = document.getElementById('sidebar');
            const content = document.getElementById('content');
            const topbar = document.getElementById('topbar');

            toggleBtn.addEventListener('click', () => {
                sidebar.classList.toggle('collapsed');
                content.classList.toggle('collapsed');
                topbar.classList.toggle('collapsed');
            });
        })();
    </script>
</body>
</html>
