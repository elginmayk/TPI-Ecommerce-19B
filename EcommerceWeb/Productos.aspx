<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="EcommerceWeb.Productos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="titulo-seccion">🛍 Productos</h2>

<!-- BUSCADOR Y FILTRO -->
<div class="row mb-4">
    <div class="col-md-4">
        <asp:TextBox ID="txtBuscar" runat="server"
            CssClass="form-control"
            placeholder="Buscar producto..." />
    </div>
    <div class="col-md-3">
        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
            <asp:ListItem Text="Todas las categorías" Value="0" />
        </asp:DropDownList>
    </div>
    <div class="col-md-3">
        <asp:DropDownList ID="ddlOrden" runat="server" CssClass="form-select">
            <asp:ListItem Text="Ordenar por..." Value="0" />
            <asp:ListItem Text="Nombre A-Z" Value="nombre_asc" />
            <asp:ListItem Text="Nombre Z-A" Value="nombre_desc" />
            <asp:ListItem Text="Precio menor a mayor" Value="precio_asc" />
            <asp:ListItem Text="Precio mayor a menor" Value="precio_desc" />
        </asp:DropDownList>
    </div>
    <div class="col-md-2">
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
            CssClass="btn btn-primary w-100"
            OnClick="btnBuscar_Click" />
    </div>
</div>

<asp:Repeater ID="rptCategorias" runat="server" OnItemDataBound="rptCategorias_ItemDataBound">

    <ItemTemplate>

        <div class="categoria">

            <h3><%# Eval("Key") %></h3>

            <div class="contenedor-productos">

                <asp:Repeater ID="rptProductos" runat="server"
                    DataSource='<%# Container.DataItem %>'>

                    <ItemTemplate>

                        <div class="card-producto">

                            <a href='DetalleProducto.aspx?id=<%# Eval("Id") %>'>
                                <img src='<%# Eval("ImagenUrl") %>' />

                                <h4><%# Eval("Nombre") %></h4>
                            </a>

                            <p class="precio">
                                $ <%# Eval("Precio", "{0:N2}") %>
                            </p>

                            <p class="text-muted small">
                                <%# (int)Eval("CantidadResenas") > 0
                                    ? "⭐ " + Eval("PromedioResena", "{0:N1}") + " (" + Eval("CantidadResenas") + ")"
                                    : "Sin reseñas todavía" %>
                                     <p class="small fw-bold">
                                <%# Convert.ToInt32(Eval("Stock")) == 0
                                    ? "🔴 Artículo agotado"
                                    : Convert.ToInt32(Eval("Stock")) == 1
                                        ? "🟡 Última unidad disponible"
                                        : "🟢 Stock disponible: " + Eval("Stock") %>
                            </p>

                            <!-- BOTON CARRITO -->
                            <asp:Button runat="server"
                                Text="🛒 Agregar al carrito"
                                CssClass="btn-carrito"
                                CommandName="Agregar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnCommand="AgregarCarrito_Click"
                                Enabled='<%# Convert.ToInt32(Eval("Stock")) > 0 %>' />

                        </div>

                    </ItemTemplate>

                </asp:Repeater>

            </div>

        </div>

    </ItemTemplate>

</asp:Repeater>
</asp:Content>
