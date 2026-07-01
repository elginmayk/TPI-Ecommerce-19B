<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="EcommerceWeb.DetalleProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">

        <asp:Panel ID="pnlProducto" runat="server">
            <div class="row">
                <div class="col-md-5">
                    <asp:Image ID="imgProducto" runat="server" CssClass="img-fluid rounded" />
                </div>
                <div class="col-md-7">
                    <h2><asp:Literal ID="litNombre" runat="server" /></h2>
                    <p class="text-muted"><asp:Literal ID="litCategoria" runat="server" /></p>

                    <p>
                        ⭐ <asp:Literal ID="litPromedio" runat="server" />
                        <asp:Literal ID="litCantidadResenas" runat="server" />
                    </p>

                    <p><asp:Literal ID="litDescripcion" runat="server" /></p>

                    <h3 class="precio"><asp:Literal ID="litPrecio" runat="server" /></h3>

                    <asp:Button ID="btnAgregarCarrito" runat="server" Text="🛒 Agregar al carrito"
                        CssClass="btn-carrito" OnClick="btnAgregarCarrito_Click" />
                </div>
            </div>
        </asp:Panel>

        <asp:Label ID="lblProductoNoEncontrado" runat="server" CssClass="text-danger" Visible="false"
            Text="El producto que buscás no existe o ya no está disponible." />

        <hr class="mt-5" />

        
        <!-- FORMULARIO DE RESEÑA -->
        
        <div class="row mt-4">
            <div class="col-md-6">
                <h4><asp:Literal ID="litTituloForm" runat="server" Text="Dejá tu reseña" /></h4>

                <asp:Panel ID="pnlFormResena" runat="server">
                    <asp:Label ID="lblErrorResena" runat="server" CssClass="text-danger d-block mb-2" Visible="false" />

                    <div class="mb-3">
                        <label class="form-label">Tu puntuación</label>
                        <asp:DropDownList ID="ddlPuntuacion" runat="server" CssClass="form-select">
                            <asp:ListItem Text="⭐ (1) - Malo" Value="1" />
                            <asp:ListItem Text="⭐⭐ (2) - Regular" Value="2" />
                            <asp:ListItem Text="⭐⭐⭐ (3) - Bueno" Value="3" />
                            <asp:ListItem Text="⭐⭐⭐⭐ (4) - Muy bueno" Value="4" Selected="True" />
                            <asp:ListItem Text="⭐⭐⭐⭐⭐ (5) - Excelente" Value="5" />
                        </asp:DropDownList>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Comentario</label>
                        <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control"
                            TextMode="MultiLine" Rows="3" MaxLength="500" />
                    </div>

                    <asp:Button ID="btnGuardarResena" runat="server" Text="Publicar reseña"
                        CssClass="btn btn-primary" OnClick="btnGuardarResena_Click" />
                </asp:Panel>

                <asp:Panel ID="pnlLoginRequerido" runat="server" Visible="false">
                    <p>Tenés que <a href="Login.aspx">iniciar sesión</a> para dejar una reseña.</p>
                </asp:Panel>
            </div>
        </div>

        
        <!-- LISTA DE RESEÑAS -->
      
        <div class="row mt-5">
            <div class="col-md-8">
                <h4>Reseñas de compradores</h4>

                <asp:Label ID="lblSinResenas" runat="server" Text="Todavía no hay reseñas para este producto."
                    Visible="false" CssClass="text-muted" />

                <asp:Repeater ID="rptResenas" runat="server">
                    <ItemTemplate>
                        <div class="border-bottom py-2">
                            <strong><%# Eval("Usuario.Nombre") %> <%# Eval("Usuario.Apellido") %></strong>
                            <span class="text-muted"> — <%# Eval("Fecha", "{0:dd/MM/yyyy}") %></span>
                            <div><%# new string('⭐', (int)Eval("Puntuacion")) %></div>
                            <p><%# Eval("Comentario") %></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

    </div>
</asp:Content>
