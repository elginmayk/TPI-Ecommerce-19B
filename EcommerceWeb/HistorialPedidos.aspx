<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialPedidos.aspx.cs" Inherits="EcommerceWeb.HistorialPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container mt-4">
    <h2 class="mb-4">📋 Mis Pedidos</h2>

    <asp:Label ID="lblSinPedidos" runat="server" Visible="false"
        CssClass="alert alert-info d-block"
        Text="No tenés pedidos realizados aún." />

    <asp:Repeater ID="rptPedidos" runat="server"
        OnItemDataBound="rptPedidos_ItemDataBound">

        <ItemTemplate>

            <div class="card mb-4 shadow-lg border-0">

                <%-- CABECERA --%>
                <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">

                    <div>
                        <h5 class="mb-0">
                            📦 Pedido #<%# Eval("Id") %>
                        </h5>
                        <small>
                            Fecha: <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                        </small>
                    </div>

                    <asp:Literal runat="server"
                        Mode="PassThrough"
                        Text='<%# ObtenerEstadoUsuario(Eval("Estado").ToString()) %>'>
                    </asp:Literal>

                </div>

                <%-- CUERPO --%>
                <div class="card-body">

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <strong>💳 Forma de pago:</strong>
                            <%# Eval("FormaPago.Nombre") %>
                        </div>

                        <div class="col-md-6">
                            <strong>🚚 Forma de entrega:</strong>
                            <%# Eval("FormaEntrega.Nombre") %>
                        </div>
                    </div>

                    <hr />

                    <%-- PRODUCTOS --%>
                    <asp:Repeater ID="rptDetalles" runat="server">
                        <ItemTemplate>

                            <div class="card border-0 shadow-sm mb-3">
                                <div class="card-body">

                                    <div class="row align-items-center">

                                        <%-- IMAGEN --%>
                                        <div class="col-md-2 text-center">
                                            <img src='<%# Eval("Producto.ImagenUrl") %>'
                                                class="img-fluid rounded"
                                                style="max-height:100px; object-fit:cover;"
                                                alt="Producto" />
                                        </div>

                                        <%-- DATOS --%>
                                        <div class="col-md-6">
                                            <h5 class="mb-1">
                                                <%# Eval("Producto.Nombre") %>
                                            </h5>

                                            <small class="text-muted">
                                                <%# Eval("Producto.Descripcion") %>
                                            </small>
                                        </div>

            -->
                                        <div class="col-md-2 text-center">
                                            <span class="badge bg-secondary fs-6">
                                                x <%# Eval("Cantidad") %>
                                            </span>
                                        </div>

                                        <%-- PRECIO --%>
                                        <div class="col-md-2 text-end">
                                            <span class="fw-bold text-success fs-5">
                                                $ <%# Eval("Producto.Precio", "{0:N2}") %>
                                            </span>
                                        </div>

                                    </div>

                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>

                    <%-- TOTAL --%>
                    <div class="text-end mt-4">
                        <span class="fs-4 fw-bold text-success">
                            Total: $ <%# Eval("Total", "{0:N2}") %>
                        </span>
                    </div>

                </div>

            </div>

        </ItemTemplate>

    </asp:Repeater>
</div>
</asp:Content>
