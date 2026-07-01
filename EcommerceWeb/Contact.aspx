<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="EcommerceWeb.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your contact page.</h3>
        <address>
            One Microsoft Way<br />
            Redmond, WA 98052-6399<br />
            <abbr title="Phone">P:</abbr>
            425.555.0100
        </address>

        <address>
            <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
            <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
        </address>

        <hr />

        <h3>Envianos un mensaje</h3>

        <asp:Panel ID="pnlExito" runat="server" Visible="false" CssClass="alert alert-success">
            ✅ ¡Mensaje enviado! Nos pondremos en contacto pronto.
        </asp:Panel>

        <asp:Panel ID="pnlFormulario" runat="server" style="max-width: 600px;">

            <div class="mb-3">
                <label class="form-label">Nombre y apellido *</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
                    placeholder="Ej: Juan Pérez" MaxLength="100" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio."
                    CssClass="text-danger small" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Email *</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                    placeholder="Ej: juan@mail.com" MaxLength="100" TextMode="Email" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio."
                    CssClass="text-danger small" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w{2,}$"
                    ErrorMessage="El email no tiene un formato válido."
                    CssClass="text-danger small" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"
                    placeholder="Ej: 1123456789" MaxLength="20" />
            </div>

            <div class="mb-3">
                <label class="form-label">Mensaje *</label>
                <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control"
                    TextMode="MultiLine" Rows="5" MaxLength="500"
                    placeholder="Escribí tu consulta acá..." />
                <asp:RequiredFieldValidator ID="rfvMensaje" runat="server"
                    ControlToValidate="txtMensaje"
                    ErrorMessage="El mensaje es obligatorio."
                    CssClass="text-danger small" Display="Dynamic" />
            </div>

            <asp:Button ID="btnEnviar" runat="server" Text="Enviar mensaje"
                CssClass="btn btn-primary" OnClick="btnEnviar_Click" />

        </asp:Panel>

    </main>
</asp:Content>
