<%@ Page Title="Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="EcommerceWeb.Perfil" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 

<div class="perfil-container">

    <h2><i class="icon-user"></i> Mi Perfil</h2>

    <!-- ✅ DATOS PERSONALES -->
    <div class="perfil-card">

        <h3>Datos Personales</h3>

        <div class="form-group">
            <label>Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Apellido</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Correo Electrónico</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-actions">
            <asp:Button ID="btnEditarDatos" runat="server" Text="Editar Datos"
                CssClass="btn-guardar" OnClick="btnEditarDatos_Click" />

            <asp:Button ID="btnGuardarDatos" runat="server" Text="Guardar Cambios"
                CssClass="btn-guardar" OnClick="btnGuardarDatos_Click" Visible="false" />
        </div>

    </div>

    <!-- ✅ DIRECCIÓN -->
    <div class="perfil-card">

        <h3>Dirección de Entrega</h3>

       
<div class="form-group">
    <label>Dirección</label>
    <asp:TextBox ID="txtDireccion" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
</div>

<div class="form-group">
    <label>Localidad</label>
    <asp:TextBox ID="txtCiudad" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
</div>

<div class="form-group">
    <label>Código Postal</label>
    <asp:TextBox ID="txtCP" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
</div>


        <div class="form-actions">
            <asp:Button ID="btnEditarDireccion" runat="server" Text="Editar Dirección"
                CssClass="btn-guardar" OnClick="btnEditarDireccion_Click" />

            <asp:Button ID="btnGuardarDireccion" runat="server" Text="Guardar Dirección"
                CssClass="btn-guardar" OnClick="btnGuardarDireccion_Click" Visible="false" />
        </div>

    </div>

    <!-- ✅ SEGURIDAD -->
    <div class="perfil-card">

        <h3>Seguridad</h3>

        <div class="form-group">
            <label>Contraseña actual</label>
            <asp:TextBox ID="txtPassActual" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Nueva contraseña</label>
            <asp:TextBox ID="txtPassNueva" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Confirmar contraseña</label>
            <asp:TextBox ID="txtConfirmarPass" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
        </div>

        <div class="form-actions">
            <asp:Button ID="btnCambiarPass" runat="server"
                Text="Cambiar contraseña"
                CssClass="btn-guardar"
                OnClick="btnCambiarPass_Click" />
        </div>

    </div>

</div>

</asp:Content>

