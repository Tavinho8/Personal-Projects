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

    <style>
        /* Variables de color */
        :root {
            --sidebar-bg: #0f3b57;
            --sidebar-logo-bg: #f25c05;
            --hover-bg: #2fb5c1;
            --btn-orange-bg: #f25c05;
            --btn-orange-hover-bg: #d94c00;
            --text-color-light: #fff;
            --box-shadow: 0 0 10px rgb(0 0 0 / 0.1);
            --font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        /* Reset y cuerpo */
        body {
            min-height: 100vh;
            margin: 0;
            padding: 0;
            background: #f8f9fa;
            font-family: var(--font-family);
        }

        /* Sidebar */
        .sidebar {
            position: fixed;
            top: 0;
            left: 0;
            height: 100vh;
            width: 220px;
            background-color: var(--sidebar-bg);
            color: var(--text-color-light);
            transition: width 0.4s ease;
            overflow-x: hidden;
            display: flex;
            flex-direction: column;
            z-index: 1050;
        }

            .sidebar.collapsed {
                width: 60px;
            }

            .sidebar .logo {
                background-color: var(--sidebar-logo-bg);
                font-weight: 900;
                font-size: 1rem;
                text-align: center;
                padding: 1.5rem 0;
                letter-spacing: 1.5px;
                color: var(--text-color-light);
                text-shadow: 0 1px 3px rgba(0,0,0,0.3);
                user-select: none;
                white-space: nowrap;
            }

            .sidebar.collapsed .logo span {
                display: none;
            }

            /* Enlaces Sidebar */
            .sidebar a {
                display: flex;
                align-items: center;
                color: var(--text-color-light);
                padding: 12px 20px;
                font-weight: 500;
                white-space: nowrap;
                text-decoration: none;
                transition: background-color 0.3s ease;
            }

                .sidebar a i {
                    min-width: 25px;
                    font-size: 1.2rem;
                    margin-right: 12px;
                    text-align: center;
                }

                .sidebar a:hover {
                    background-color: var(--hover-bg);
                    color: var(--text-color-light);
                }

            .sidebar.collapsed a span {
                display: none;
            }

        /* Topbar */
        .topbar {
            position: fixed;
            top: 0;
            left: 220px;
            right: 0;
            height: 60px;
            background-color: var(--sidebar-bg);
            color: var(--text-color-light);
            display: flex;
            align-items: center;
            padding: 0 1rem;
            transition: left 0.4s ease;
            z-index: 1040;
        }

            .topbar.collapsed {
                left: 60px;
            }

            .topbar .btn-toggle {
                color: var(--text-color-light);
                font-size: 1.25rem;
                border: none;
                background: transparent;
                cursor: pointer;
            }

                .topbar .btn-toggle:focus {
                    outline: none;
                    box-shadow: none;
                }

        /* Contenido */
        .content {
            margin-top: 60px;
            margin-left: 220px;
            padding: 1.5rem;
            transition: margin-left 0.4s ease;
        }

            .content.collapsed {
                margin-left: 60px;
            }

        /* Cards estadísticos */
        .stats-box {
            display: flex;
            flex-wrap: wrap;
            gap: 1rem;
            margin-bottom: 1.5rem;
        }

            .stats-box .card {
                flex: 1 1 200px;
                border-radius: 10px;
                color: var(--text-color-light);
                padding: 1.5rem;
                box-shadow: var(--box-shadow);
                display: flex;
                align-items: center;
                gap: 1rem;
            }

                .stats-box .card i {
                    font-size: 2.5rem;
                }

        .stats-usuarios {
            background-color: var(--sidebar-bg);
        }

        .stats-cursos {
            background-color: var(--sidebar-logo-bg);
        }

        .stats-inscripciones {
            background-color: var(--hover-bg);
        }

        /* Botones */
        .btn-orange {
            background-color: var(--btn-orange-bg);
            border: none;
            color: var(--text-color-light);
            transition: background-color 0.3s ease;
        }

            .btn-orange:hover {
                background-color: var(--btn-orange-hover-bg);
                color: var(--text-color-light);
            }

        /* Tabla */
        .table-responsive {
            box-shadow: var(--box-shadow);
            background: #fff;
            border-radius: 10px;
            padding: 1rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Sidebar -->
        <nav class="sidebar" id="sidebar">
            <div class="logo"><span>WebAcademia Admin</span></div>
            <a href="AdminDashboard.aspx"><i class="fas fa-home"></i><span>Dashboard</span></a>
            <a href="CursosAdmin.aspx"><i class="fas fa-book"></i><span>Cursos</span></a>
            <a href="#"><i class="fas fa-user-graduate"></i><span>Estudiantes</span></a>
            <a href="#"><i class="fas fa-chalkboard-teacher"></i><span>Instructores</span></a>
            <a href="../Logout.aspx"><i class="fas fa-sign-out-alt"></i><span>Cerrar Sesión</span></a>
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
                <a href="#" class="btn btn-primary mb-2">Gestionar Usuarios</a>
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
