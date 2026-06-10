<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="EcommerceWeb.Productos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="titulo-seccion">🛍 Productos</h2>

    <asp:Repeater ID="rptCategorias" runat="server">

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

                            </div>

                        </ItemTemplate>

                    </asp:Repeater>

                </div>

            </div>

        </ItemTemplate>

    </asp:Repeater>

</asp:Content>
