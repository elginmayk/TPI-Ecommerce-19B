<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="EcommerceWeb.Productos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="titulo-seccion">🛍 Productos</h2>

    <!-- BUSCADOR Y FILTRO -->
<div class="row mb-4">
    <div class="col-md-6">
        <asp:TextBox ID="txtBuscar" runat="server" 
            CssClass="form-control" 
            placeholder="Buscar producto..." />
    </div>
    <div class="col-md-4">
        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
            <asp:ListItem Text="Todas las categorías" Value="0" />
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

                                <img src='<%# Eval("ImagenUrl") %>' />

                                <h4><%# Eval("Nombre") %></h4>

                                <p class="precio">
                                    $ <%# Eval("Precio", "{0:N2}") %>
                                </p>
                                
                          <!--  BOTON CARRITO -->
                          <asp:Button runat="server" 
                                Text="🛒 Agregar al carrito"
                                CssClass="btn-carrito"
                                CommandName="Agregar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnCommand="AgregarCarrito_Click" />

                            </div>

                        </ItemTemplate>

                    </asp:Repeater>

                </div>

            </div>

        </ItemTemplate>

    </asp:Repeater>

</asp:Content>
