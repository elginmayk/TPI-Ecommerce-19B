<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialPedidos.aspx.cs" Inherits="EcommerceWeb.HistorialPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">📋 Mis Pedidos</h2>

        <asp:Label ID="lblSinPedidos" runat="server" Visible="false"
            CssClass="alert alert-info d-block"
            Text="No tenés pedidos realizados aún." />

        <asp:Repeater ID="rptPedidos" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-header bg-dark text-white d-flex justify-content-between">
                        <span><strong>Pedido #<%# Eval("Id") %></strong> — <%# Eval("Fecha", "{0:dd/MM/yyyy}") %></span>
                        <span class="badge bg-warning text-dark"><%# Eval("Estado") %></span>
                    </div>
                    <div class="card-body">
                        <p><strong>Forma de pago:</strong> <%# Eval("FormaPago.Nombre") %></p>
                        <p><strong>Forma de entrega:</strong> <%# Eval("FormaEntrega.Nombre") %></p>
                        <p class="text-success fs-5"><strong>Total: $ <%# Eval("Total", "{0:N2}") %></strong></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
