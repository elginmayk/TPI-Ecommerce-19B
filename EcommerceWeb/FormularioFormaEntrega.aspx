<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioFormaEntrega.aspx.cs" Inherits="EcommerceWeb.FormularioFormaEntrega" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6">

                <h2><asp:Literal ID="litTitulo" runat="server" /></h2>
                <hr />

                <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />

                <div class="mb-3">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>

                <asp:Button ID="btnGuardar" Text="Guardar" runat="server"
                    CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <a href="ListaFormasEntrega.aspx" class="btn btn-secondary ms-2">Cancelar</a>

            </div>
        </div>
    </div>
</asp:Content>
