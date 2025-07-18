<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EstudianteAdmin.aspx.cs" Inherits="WebAcademia.Pages.Estudiante.EstudianteAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Dashboard Estudiante</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <link href="../Shared/StyleAdmin/StyleAdminDashboard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Sidebar -->
        <nav class="sidebar" id="sidebar">
            <div class="logo"><span>WebAcademia</span></div>
            <a href="EstudianteDashboard.aspx"><i class="fas fa-home me-2"></i><span>Inicio</span></a>
            <a href="#"><i class="fas fa-book me-2"></i><span>Mis Cursos</span></a>
            <a href="#"><i class="fas fa-clipboard-list me-2"></i><span>Historial</span></a>
            <a href="../Logout.aspx"><i class="fas fa-sign-out-alt me-2"></i><span>Salir</span></a>
        </nav>

        <!-- Topbar -->
        <header class="topbar" id="topbar">
            <button type="button" class="btn-toggle" id="toggleSidebar" aria-label="Toggle sidebar">
                <i class="fas fa-bars"></i>
            </button>
            <h5 class="ms-3 mb-0">Bienvenido, <%= nombreEstudiante %></h5>
        </header>

        <!-- Main Content -->
        <main class="content" id="content">
            <div class="stats-box">
                <div class="card stats-cursos">
                    <i class="fas fa-book"></i>
                    <div>
                        <h6>Cursos Disponibles</h6>
                        <h3><%= TotalCursosDisponibles %></h3>
                    </div>
                </div>
                <div class="card stats-inscripciones">
                    <i class="fas fa-user-graduate"></i>
                    <div>
                        <h6>Mis Inscripciones</h6>
                        <h3><%= MisInscripciones %></h3>
                    </div>
                </div>
            </div>

            <div class="mb-4 p-3 bg-light rounded shadow-sm">
                <div class="row g-2 align-items-end">
                    <%-- 🔎 Nombre Curso --%>
                    <div class="col-md-4">
                        <label for="txtBuscarNombre" class="form-label">Nombre del Curso</label>
                        <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control" placeholder="Ej: Programación"></asp:TextBox>
                    </div>

                    <%-- ✅ Activo --%>
                    <div class="col-md-3">
                        <label for="ddlActivo" class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlActivo" runat="server" CssClass="form-select">
                            <asp:ListItem Value="">-- Todos --</asp:ListItem>
                            <asp:ListItem Value="true">Activo</asp:ListItem>
                            <asp:ListItem Value="false">Inactivo</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <%-- ✅ Instructor --%>
                    <div class="col-md-3">
                        <label for="ddlInstructor" class="form-label">Instructor</label>
                        <asp:DropDownList ID="ddlInstructor" runat="server" CssClass="form-select">
                            <%-- Se llena en el code-behind --%>
                        </asp:DropDownList>
                    </div>

                    <%-- ✅ Botones con iconos --%>
                    <div class="col-md-2 text-end">
                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary w-100 mb-2" OnClick="btnBuscar_Click">
                <i class="fas fa-search"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnLimpiar" runat="server" CssClass="btn btn-secondary w-100" OnClick="btnLimpiar_Click">
                <i class="fas fa-eraser"></i>
                        </asp:LinkButton>
                    </div>

                </div>
            </div>

            <%-- ✅ Label para mensajes --%>
            <asp:Label ID="lblMensaje" runat="server" CssClass="form-text fw-bold mb-3"></asp:Label>

            <%-- ✅ Tabla Cursos centrada --%>
            <div class="table-container">
                <div class="table-responsive" style="max-height: 400px; overflow-y: auto;">
                    <asp:GridView ID="gvCursos" runat="server"
                        OnRowCommand="gvCursos_RowCommand"
                        CssClass="table table-striped table-bordered text-center"
                        AutoGenerateColumns="False"
                        AllowPaging="True"
                        PageSize="5"
                        PagerStyle-CssClass="text-center"
                        PagerStyle-HorizontalAlign="Center"
                        OnPageIndexChanging="gvCursos_PageIndexChanging">

                        <Columns>
                            <%-- ✅ Nombre --%>
                            <asp:BoundField DataField="Nombre" HeaderText="Curso" />
                            <%-- ✅ Descripción --%>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <%-- ✅ Instructor --%>
                            <asp:BoundField DataField="InstructorNombre" HeaderText="Instructor" />
                            <%-- ✅ Cupo --%>
                            <asp:BoundField DataField="CupoMaximo" HeaderText="Cupo Máximo" />
                            <%-- ✅ Estado --%>
                            <asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("Activo")) ? "Sí" : "No" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- ✅ Acción para matricular al estudiante --%>
                            <asp:TemplateField HeaderText="Acción">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMatricular" runat="server"
                                        CssClass="btn btn-sm btn-success"
                                        CommandArgument='<%# Eval("CursoID") %>'
                                        CommandName="Matricular">
                                         <i class="fas fa-user-plus"></i> Matricular
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                        <%-- ✅ Paginación personalizada centrada --%>
                        <PagerTemplate>
                            <div class="d-flex justify-content-center">
                                <asp:LinkButton runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-sm btn-outline-secondary me-2">
                        <i class="fas fa-angle-left"></i> Anterior
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-sm btn-outline-secondary">
                        Siguiente <i class="fas fa-angle-right"></i>
                                </asp:LinkButton>
                            </div>
                        </PagerTemplate>

                    </asp:GridView>
                </div>

                <%-- ✅ Controles inferiores --%>
                <div class="d-flex justify-content-between align-items-center mt-3 flex-wrap">
                    <%-- ✅ Selector de tamaño de página --%>
                    <div class="d-flex align-items-center mb-2 mb-md-0">
                        <asp:Label runat="server" Text="Registros por página:" CssClass="me-2 fw-bold"></asp:Label>
                        <asp:DropDownList ID="ddlPageSize" runat="server"
                            AutoPostBack="true"
                            CssClass="form-select w-auto"
                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                        </asp:DropDownList>
                    </div>

                    <%-- ✅ Total registros --%>
                    <asp:Label ID="lblTotalRegistros" runat="server" CssClass="fw-bold"></asp:Label>
                </div>
            </div>
        </main>
    </form>

    <!-- Scripts -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
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
