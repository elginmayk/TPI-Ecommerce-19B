<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaUsuarios.aspx.cs" Inherits="EcommerceWeb.ListaUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <div class="row">
            <div class="col">
                <h2>Usuarios</h2>
                <hr />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col">
                <asp:Label ID="lblMensaje" runat="server" Visible="false" />
            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:GridView ID="gvUsuarios" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-bordered table-hover"
                    DataKeyNames="Id"
                    OnRowCommand="gvUsuarios_RowCommand">

                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="Rol" HeaderText="Rol" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <a href='FormularioUsuario.aspx?id=<%# Eval("Id") %>' 
                                   class="btn btn-warning btn-sm">Editar</a>
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-danger btn-sm"
                                    OnClientClick="return confirm('¿Seguro que querés eliminar este usuario?');"
                                    Text="Eliminar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col">
                <a href="FormularioUsuario.aspx" class="btn btn-success">Nuevo Usuario</a>
                <a href="Default.aspx" class="btn btn-secondary ms-2">Volver</a>
            </div>
        </div>

    </div>
</asp:Content>