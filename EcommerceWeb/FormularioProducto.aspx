<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="EcommerceWeb.FormularioProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-6">
                <h2>Cargar Nuevo Producto</h2>
                <div class="mb-3">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Precio</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione..." Value="0" />
                        <asp:ListItem Text="Electrónica" Value="1" />
                    </asp:DropDownList>
                </div>
                <asp:Button Text="Guardar" ID="btnAceptar" CssClass="btn btn-primary" runat="server" />
                <a href="ListaProductos.aspx" class="btn btn-link">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
