<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="EcommerceWeb.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
    <h2>Mi Carrito 🛒</h2>
    <hr />
    <table class="table table-hover">
        <thead class="table-dark">
            <tr>
                <th>Producto</th>
                <th>Cantidad</th>
                <th>Precio Unitario</th>
                <th>Subtotal</th>
                <th>Acción</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Teclado Mecánico (Ejemplo)</td>
                <td>1</td>
                <td>$4500,00</td>
                <td>$4500,00</td>
                <td><button class="btn btn-danger btn-sm">Eliminar</button></td>
            </tr>
        </tbody>
    </table>
    <div class="text-end mt-4">
        <h4>Total a Pagar: <span class="text-success">$4500,00</span></h4>
        <br />
        <a href="ListaProductos.aspx" class="btn btn-primary">Seguir Comprando</a>
        <asp:Button Text="Finalizar Compra" ID="btnFinalizar" CssClass="btn btn-success" runat="server" />
    </div>
</div>
</asp:Content>
