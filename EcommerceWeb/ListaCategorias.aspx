<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaCategorias.aspx.cs" Inherits="EcommerceWeb.ListaCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col">
                <h2>Categorías</h2>
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
                <asp:GridView ID="gvCategorias" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-bordered table-hover"
                    DataKeyNames="Id"
                    OnRowCommand="gvCategorias_RowCommand">

                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <a href='FormularioCategoria.aspx?id=<%# Eval("Id") %>' 
                                   class="btn btn-warning btn-sm">Editar</a>
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-danger btn-sm"
                                    OnClientClick="return confirm('¿Seguro que querés eliminar esta categoría?');"
                                    Text="Eliminar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col">
                <a href="FormularioCategoria.aspx" class="btn btn-success">Nueva Categoría</a>
                <a href="Default.aspx" class="btn btn-secondary ms-2">Volver</a>
            </div>
        </div>

    </div>
</asp:Content>