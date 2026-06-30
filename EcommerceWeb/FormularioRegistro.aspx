<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioRegistro.aspx.cs" Inherits="EcommerceWeb.FormularioRegistro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="d-flex flex-column align-items-center justify-content-center my-5">
        
        <div class="w-100" style="max-width: 400px;">

            <div class="text-center">
                <h2><asp:Literal ID="litTitulo" runat="server" /></h2>
                <hr />
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger text-center d-block mb-3" Visible="false" />

            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control text-center" />
            </div>

            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control text-center" />
            </div>

            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control text-center" />
            </div>

            <div class="mb-3">
                <label class="form-label">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control text-center" />
            </div>

            <div class="mb-3">
                <label class="form-label" id="lblPassword" runat="server">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control text-center" TextMode="Password" />
            </div>

            <div class="mb-3">
                <label class="form-label" id="lblConfirmarPassword" runat="server">Confirmar Contraseña</label>
                <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="form-control text-center" TextMode="Password" />
            </div>

            <div class="mb-3">
                <asp:RequiredFieldValidator 
                    ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio."
                    CssClass="text-danger d-block"
                    Display="Dynamic" />

                <asp:RequiredFieldValidator 
                    ID="rfvApellido" runat="server"
                    ControlToValidate="txtApellido"
                    ErrorMessage="El apellido es obligatorio."
                    CssClass="text-danger d-block"
                    Display="Dynamic" />

                <asp:RequiredFieldValidator 
                    ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio."
                    CssClass="text-danger d-block"
                    Display="Dynamic" />

                <asp:RequiredFieldValidator 
                    ID="rfvTelefono" runat="server"
                    ControlToValidate="txtTelefono"
                    ErrorMessage="El teléfono es obligatorio."
                    CssClass="text-danger d-block"
                    Display="Dynamic" />

                <asp:RequiredFieldValidator 
                    ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="La contraseña es obligatoria."
                    CssClass="text-danger d-block"
                    Display="Dynamic" />
            </div>

            <div class="text-center mt-4">
                <asp:Button ID="btnRegistrar" Text="Registrar" runat="server"
                    CssClass="btn btn-primary w-100" OnClick="btnRegistrar_Click" />
            </div>

        </div>

    </div>
</asp:Content>
