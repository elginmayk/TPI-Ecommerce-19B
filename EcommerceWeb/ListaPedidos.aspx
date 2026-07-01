<%@ Page Title="Pedidos"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ListaPedidos.aspx.cs"
    Inherits="EcommerceWeb.ListaPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mt-4">Gestión de Pedidos</h2>

    <asp:GridView ID="gvPedidos" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered mt-3"
        OnRowCommand="gvPedidos_RowCommand">

        <Columns>

            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
            <asp:BoundField DataField="Total" HeaderText="Total" />

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label runat="server"
                        Text='<%# Eval("Estado") %>'
                        CssClass='<%# Eval("Estado").ToString() == "Entregado" ? "text-success fw-bold" : "text-warning fw-bold" %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acción">
                <ItemTemplate>
                    <asp:Button runat="server"
                        Text="✅ Entregar"
                        CommandName="Entregar"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-success btn-sm"
                        Visible='<%# Eval("Estado").ToString() != "Entregado" %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>