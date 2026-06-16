<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioContrasena.aspx.cs" Inherits="EcommerceWeb.FormularioContrasena" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3 text-start">
        <label class="form-label fw-bold">Correo Electrónico</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="nombre@ejemplo.com" />
    </div>
    
    <div class="mb-4 text-start">
        <label class="form-label fw-bold">Contraseña</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="********" />
    </div>

    <div class="d-grid mb-3">
        <asp:Button ID="btnConfirmar" Text="Confirmar" runat="server" CssClass="btn btn-primary py-2" OnClick="btnConfirmar_Click" />
    </div>

    <div class="text-center mt-3">
        <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
