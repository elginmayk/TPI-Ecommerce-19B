<%@ Page Title="Lista productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaProductos.aspx.cs" Inherits="EcommerceWeb.ListaProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <div class="row">
            <div class="col">
                <h1 class="display-4 text-center">Listado de Artículos</h1>
                <p class="lead text-center">Gestión del catálogo de nuestro E-commerce</p>
                <hr />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col">
                <asp:Label ID="lblMensaje" runat="server" Visible="false" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtFiltroRapido" runat="server" CssClass="form-control" placeholder="Buscar por nombre..." />
                    <asp:Button ID="btnBuscar" runat="server" Text="🔍" CssClass="btn btn-outline-secondary" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:GridView ID="dgvArticulos" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-dark table-hover table-striped table-bordered"
                    DataKeyNames="Id"
                    OnRowCommand="dgvArticulos_RowCommand">

                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="CategoriaNombre" HeaderText="Categoría" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C2}" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                        <asp:BoundField DataField="Estado" HeaderText="Activo" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <a href='FormularioProducto.aspx?id=<%# Eval("Id") %>' 
                                   class="btn btn-warning btn-sm">Editar</a>
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-danger btn-sm"
                                    OnClientClick="return confirm('¿Seguro que querés eliminar este producto?');"
                                    Text="Eliminar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col">
                <a href="Default.aspx" class="btn btn-secondary">Regresar</a>
                <a href="FormularioProducto.aspx" class="btn btn-success float-end">Agregar Nuevo Producto</a>
            </div>
        </div>
    </div>

</asp:Content>
