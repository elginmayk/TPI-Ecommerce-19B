<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EcommerceWeb._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<main>
        

<div class="banner">
    <div class="text-center">
        <h1>Bienvenido a The Diba Store</h1>
        <p>Los mejores productos al mejor precio</p>
        <a href="/Productos" class="btn-banner">Ver productos</a>
    </div>
</div>

     

<h2 class="titulo-seccion">Productos destacados</h2>

<div class="contenedor-productos">

    <asp:Repeater ID="rptProductos" runat="server">

        <ItemTemplate>

            <div class="card-producto">
            <a href='DetalleProducto.aspx?id=<%# Eval("Id") %>'>
                <img src='<%# Eval("ImagenUrl") %>' />

                <h4><%# Eval("Nombre") %></h4>
            </a>
                <p class="precio">
                    $ <%# Eval("Precio", "{0:N2}") %>
                </p>

            </div>

        </ItemTemplate>

    </asp:Repeater>

</div>

<div class="beneficios">

    <div class="beneficio">
        🚚 <h4>Envíos rápidos</h4>
        <p>A todo el país</p>
    </div>

    <div class="beneficio">
        💳 <h4>Pagos seguros</h4>
        <p>Protección garantizada</p>
    </div>

    <div class="beneficio">
        🔄 <h4>Cambios gratis</h4>
        <p>Hasta 30 días</p>
    </div>

</div>


<h2 class="titulo-categorias">Categorías</h2>

<div class="categorias">

<asp:Repeater ID="rptCategorias" runat="server">
    <ItemTemplate>
        <a class="categoria-item" runat="server"
           href='<%# Eval("Id", "~/Productos.aspx?idCat={0}") %>'
           style="text-decoration: none; color: inherit;">
            <%# Eval("Nombre") %>
        </a>
    </ItemTemplate>
</asp:Repeater>

</div>


 </main>

</asp:Content>
