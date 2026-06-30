<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EcommerceWeb.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">📊 Dashboard de Ventas</h2>

        <div class="row mb-4">
            <div class="col-md-4">
                <div class="card text-white bg-success">
                    <div class="card-body">
                        <h6>Total Vendido</h6>
                        <h3><asp:Label ID="lblTotalVendido" runat="server" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white bg-primary">
                    <div class="card-body">
                        <h6>Cantidad de Pedidos</h6>
                        <h3><asp:Label ID="lblCantidadPedidos" runat="server" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white bg-dark">
                    <div class="card-body">
                        <h6>Ticket Promedio</h6>
                        <h3><asp:Label ID="lblTicketPromedio" runat="server" /></h3>
                    </div>
                </div>
            </div>
        </div>

        <div class="card mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0">🏆 Productos más vendidos</h5>
            </div>
            <div class="card-body">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Producto</th>
                            <th>Unidades vendidas</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptTopProductos" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("Cantidad") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

