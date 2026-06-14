<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EcommerceWeb._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<main>
        

<div class="banner">
    <div class="overlay-left">
        <h1>Bienvenido a The Diba Store</h1>
        <p>Los mejores productos al mejor precio</p>
        <a href="#productos" class="btn-banner">Ver productos</a>
    </div>
</div>




      

<h2 class="titulo-seccion">Productos destacados</h2>

<div class="contenedor-productos">

    <asp:Repeater ID="rptProductos" runat="server">

        <ItemTemplate>

            <div class="card-producto">

                <img src='<%# Eval("ImagenUrl") %>' />

                <h4><%# Eval("Nombre") %></h4>

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

    <div class="categoria-item">👟 Calzado</div>
    <div class="categoria-item">🎒 Accesorios</div>
    <div class="categoria-item">👕 Ropa</div>

</div>


 </main>

</asp:Content>
