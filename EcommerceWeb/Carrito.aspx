<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="EcommerceWeb.Carrito" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container mt-5">
    <h2>Mi Carrito 🛒</h2>
    <hr />
    <asp:Label ID="lblVacio" runat="server" Visible="false" 
    CssClass="alert alert-warning mt-3 d-block" 
    Text="Tu carrito está vacío. ¡Agregá productos para continuar!" />

    <asp:Repeater ID="rptCarrito" runat="server" OnItemCommand="rptCarrito_ItemCommand">
        <HeaderTemplate>
            <table class="table table-hover">
                <thead class="table-dark">
                    <tr>
                        <th>Producto</th>
                        <th>Precio Unitario</th>
                        <th>Cantidad</th>
                        <th>Subtotal</th>
                        <th>Acción</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
                    <tr>
                        <td><%# Eval("Nombre") %></td>
                        <td>$ <%# Eval("Precio", "{0:N2}") %></td>
                        <td><%# Eval("Cantidad") %></td>
                        <td>$ <%# Eval("Subtotal", "{0:N2}") %></td>
                    <td>
                        <asp:Button runat="server" Text="Eliminar"
                            CssClass="btn btn-danger btn-sm"
                            CommandName="Eliminar"
                            CommandArgument='<%# Eval("IdProducto") %>' />
                        </td>
                    </tr>
        </ItemTemplate>
        <FooterTemplate>
                </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <div class="text-end mt-4">
        <h4>Total a Pagar: <asp:Label ID="lblTotal" runat="server" CssClass="text-success" Text="$ 0,00" /></h4>
        <br />
        <a href="Productos.aspx" class="btn btn-primary">Seguir Comprando</a>
        <asp:Button Text="Finalizar Compra" ID="btnFinalizar" CssClass="btn btn-success" runat="server" OnClick="btnFinalizar_Click" />
    </div>
 </div>
</asp:Content>