<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioUsuario.aspx.cs" Inherits="EcommerceWeb.FormularioUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6">

                <h2><asp:Literal ID="litTitulo" runat="server" /></h2>
                <hr />

                <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />

                <div class="mb-3">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <asp:label ID="lblRol" class="form-label" runat="server">Rol</asp:label>
                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Cliente" Value="2" />
                        <asp:ListItem Text="Admin" Value="1" />
                    </asp:DropDownList>
                </div>

<%--                <div class="mb-3">
                    <label class="form-label" id="lblPasswordLabel" runat="server">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:Label ID="lblPasswordHelp" runat="server" CssClass="form-text text-muted" 
                        Text="Dejar en blanco para no cambiarla" Visible="false" />
                </div>--%>

                <asp:Button ID="btnGuardar" Text="Guardar" runat="server"
                    CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <a href="ListaUsuarios.aspx" class="btn btn-secondary ms-2">Cancelar</a>

            </div>
        </div>
    </div>
</asp:Content>
