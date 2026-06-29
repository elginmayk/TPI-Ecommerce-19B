<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EcommerceWeb.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            
            <div class="col-md-5 col-lg-4"> 

                <div class="card shadow-sm border-0">
                    <div class="card-body p-4">
                        
                        <h3 class="text-center mb-4">Iniciar Sesión</h3>

                        <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3 text-center" Visible="false" />

                        <div class="mb-3 text-start">
                            <label class="form-label fw-bold">Correo Electrónico</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="nombre@ejemplo.com" />
                        </div>

                        <div class="mb-4 text-start">
                            <label class="form-label fw-bold">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="********" />
                        </div>

                        <asp:RequiredFieldValidator 
                            ID="rfvEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="El email es obligatorio."
                            CssClass="text-danger"
                            Display="Dynamic" />

                        <asp:RequiredFieldValidator 
                            ID="rfvPassword" runat="server"
                            ControlToValidate="txtPassword"
                            ErrorMessage="La contraseña es obligatoria."
                            CssClass="text-danger"
                            Display="Dynamic" />

                        <div class="d-grid mb-3">
                            <asp:Button ID="btnLogin" Text="Login" runat="server" CssClass="btn btn-primary py-2" OnClick="btnLogin_Click" />
                        </div>

                        <div class="mt-3">
                            <span class="text-muted">¿No tienes cuenta?</span> 
                            <a href="FormularioUsuario.aspx" class="text-decoration-none fw-bold">Regístrate aquí</a>
                        </div>
                        <div class="mt-3">
                            <a href="FormularioContrasena.aspx" class="text-decoration-none fw-bold">Olvidé mi contraseña</a>
                        </div>

                        <div class="text-center mt-3">
                            <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red"></asp:Label>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>