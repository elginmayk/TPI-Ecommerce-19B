<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EcommerceWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h2 style="margin-bottom:20px;">Productos</h2>

<asp:Repeater ID="rptCategorias" runat="server">
    <ItemTemplate>

        <!-- 🔥 NOMBRE DE CATEGORIA -->
        <h3><%# Eval("Key") %></h3>

        <asp:Repeater ID="rptProductos" runat="server" 
            DataSource='<%# Container.DataItem %>'>
            
            <ItemTemplate>

                <div style="border:1px solid #ccc; padding:10px; width:200px; display:inline-block; margin:10px; text-align:center;">
                    
                    <img src="<%# Eval("ImagenUrl") %>" style="width:100%; height:150px;" />

                    <h4><%# Eval("Nombre") %></h4>

                    <p>$ <%# Eval("Precio", "{0:N2}") %></p>

                </div>

            </ItemTemplate>

        </asp:Repeater>

    </ItemTemplate>
</asp:Repeater>


 


    </main>

</asp:Content>
