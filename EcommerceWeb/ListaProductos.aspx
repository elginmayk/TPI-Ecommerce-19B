<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaProductos.aspx.cs" Inherits="EcommerceWeb.ListaProductos" %>
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
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtFiltroRapido" runat="server" CssClass="form-control" placeholder="Buscar por nombre..." />
                    <asp:Button ID="btnBuscar" runat="server" Text="🔍" CssClass="btn btn-outline-secondary" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:GridView ID="dgvArticulos" runat="server" AutoGenerateColumns="true" CssClass="table table-dark table-hover table-striped table-bordered">
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
