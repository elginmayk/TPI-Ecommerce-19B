<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="EcommerceWeb.Confirmacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 text-center">
    <div class="card shadow p-5">
        <h1 class="text-success mb-3">✅ ¡Compra confirmada!</h1>
        <p class="fs-5">Tu pedido fue registrado correctamente.</p>
        
        <div class="alert alert-success mt-3">
            <h4>Número de pedido: 
                <strong><asp:Label ID="lblNroPedido" runat="server" /></strong>
            </h4>
        </div>

        <p class="text-muted mt-3">
            Recibirás un mail con los detalles de tu compra en breve.
        </p>

        <div class="mt-4">
            <a href="Productos.aspx" class="btn btn-primary me-2">
                🛍️ Seguir comprando
            </a>
            <a href="Default.aspx" class="btn btn-outline-secondary">
                🏠 Volver al inicio
            </a>
        </div>
    </div>
</div>
</asp:Content>
