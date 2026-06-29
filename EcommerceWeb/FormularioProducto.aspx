<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="EcommerceWeb.FormularioProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-6">
                <h2>Cargar Nuevo Producto</h2>
                <hr />
                 <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
                <div class="mb-3">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Precio</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Stock</label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label">URL Imagen</label>
                    <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Estado</label>
                    <asp:CheckBox ID="chkEstado" runat="server" Text=" Activo" />
                </div>

                <asp:RequiredFieldValidator 
                    ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio."
                    CssClass="text-danger d-block"
                    Display="Dynamic" />

                <asp:RequiredFieldValidator 
                    ID="rfvPrecio" runat="server"
                    ControlToValidate="txtPrecio"
                    ErrorMessage="El precio es obligatorio."
                    CssClass="text-danger d-block"
                    Display="Dynamic" />

                <asp:RequiredFieldValidator 
                    ID="rfvStock" runat="server"
                    ControlToValidate="txtStock"
                    ErrorMessage="El stock es obligatorio."
                    CssClass="text-danger d-block"
                    Display="Dynamic" />

                <asp:Button ID="btnGuardar" Text="Guardar" runat="server" 
                    CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <a href="ListaProductos.aspx" class="btn btn-secondary ms-2">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
