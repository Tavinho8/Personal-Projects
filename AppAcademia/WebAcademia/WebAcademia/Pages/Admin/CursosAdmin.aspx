<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CursosAdmin.aspx.cs" Inherits="WebAcademia.Pages.Admin.CursosAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestionar Cursos</title>

    <!-- ✅ Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- ✅ FontAwesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <!-- ✅ SweetAlert2 -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- ✅ Estilo personalizado -->
    <link href="../Shared/StyleAdmin.css" rel="stylesheet" />
</head>

<body>
    <%-- ✅ Formulario principal --%>
    <form id="form1" runat="server">

        <%-- ✅ Contenedor principal centrado --%>
        <div class="courses-card">

            <%-- ✅ Encabezado con icono, botón agregar y volver --%>
            <div class="d-flex justify-content-between align-items-center mb-4 flex-wrap">
                <h4 class="m-0">
                    <i class="fas fa-list-ul me-2 text-primary"></i>Lista de Cursos
                </h4>
                <div class="d-flex flex-wrap gap-2">
                    <a href="AdminDashboard.aspx" class="btn btn-outline-secondary">
                        <i class="fas fa-home me-2"></i>Volver
                    </a>
                    <a href="CursosFormAdmin.aspx" class="btn btn-success">
                        <i class="fas fa-plus me-2"></i>Curso
                    </a>
                </div>
            </div>



            <%-- ✅ Filtros de búsqueda --%>
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
                            <%-- ✅ Acciones --%>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <div class="d-flex justify-content-center">
                                        <a href='<%# Eval("CursoID", "CursosFormAdmin.aspx?id={0}") %>' class="btn btn-sm btn-primary me-2">
                                            <i class="fas fa-edit"></i>
                                        </a>
                                        <asp:LinkButton ID="lnkEliminar" runat="server"
                                            CssClass="btn btn-sm btn-danger eliminar-btn"
                                            CommandArgument='<%# Eval("CursoID") %>'
                                            CommandName="Eliminar">
                                            <i class="fas fa-trash"></i>
                                        </asp:LinkButton>
                                    </div>
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

    </form>

    <!-- ✅ Bootstrap Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <!-- ✅ Confirmación SweetAlert2 -->
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll(".eliminar-btn").forEach(function (btn) {
                btn.addEventListener("click", function handler(e) {
                    e.preventDefault();
                    Swal.fire({
                        title: "¿Estás seguro?",
                        text: "No podrás deshacerlo.",
                        icon: "warning",
                        showCancelButton: true,
                        confirmButtonText: "Sí, eliminar",
                        cancelButtonText: "Cancelar"
                    }).then((result) => {
                        if (result.isConfirmed) {
                            btn.removeEventListener("click", handler);
                            btn.click();
                        }
                    });
                });
            });
        });
    </script>
</body>
</html>
