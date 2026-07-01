<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="EcommerceWeb.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
    <h2 class="mb-4">🛒 Finalizar Compra</h2>

    <div class="row">
        <!-- COLUMNA IZQUIERDA -->
        <div class="col-md-8">

            <!-- RESUMEN DE PRODUCTOS -->
            <div class="card mb-4">
                <div class="card-header bg-dark text-white">
                    <h5 class="mb-0">📦 Resumen del pedido</h5>
            </div>
                <div class="card-body">
                    <asp:Repeater ID="rptProductos" runat="server">
                        <HeaderTemplate>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Producto</th>
                                        <th>Precio</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("Nombre") %></td>
                                        <td>$ <%# Eval("Precio", "{0:N2}") %></td>
                                    </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- ENTREGA -->
            <div class="card mb-4">
                <div class="card-header bg-dark text-white">
                    <h5 class="mb-0">🚚 Entrega</h5>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                       <div class="form-check mb-3 p-3 border rounded">
                           <asp:RadioButton ID="rbEnvio" runat="server" 
                               GroupName="entrega" 
                               CssClass="form-check-input"
                               AutoPostBack="true"
                               OnCheckedChanged="rbEntrega_Changed" />
                           <label class="form-check-label ms-1">
                               <strong>📦 Envío a domicilio</strong><br />
                               <small class="text-muted">Costo de envío: $ 2.000,00</small>
                           </label>
                       </div>
                       <div class="form-check p-3 border rounded">
                           <asp:RadioButton ID="rbRetiro" runat="server" 
                               GroupName="entrega" 
                               CssClass="form-check-input"
                               AutoPostBack="true"
                               OnCheckedChanged="rbEntrega_Changed" />
                           <label class="form-check-label ms-1">
                               <strong>🏪 Retiro en punto</strong><br />
                               <small class="text-muted">Sin costo adicional</small>
                           </label>
                       </div>
                   </div>

                    <!-- DIRECCION (solo si elige envio) -->
                    <asp:Panel ID="pnlDireccion" runat="server" Visible="false">
                        <!-- BOTONES USAR DIRECCIÓN -->
                        <div class="d-flex gap-2 mb-3">
                            <asp:Button ID="btnUsarMiDireccion" runat="server" 
                                Text="📍 Usar mi dirección guardada"
                                CssClass="btn btn-outline-primary btn-sm"
                                OnClick="btnUsarMiDireccion_Click" />
                            <asp:Button ID="btnOtraDireccion" runat="server"
                                Text="✏️ Usar otra dirección"
                                CssClass="btn btn-outline-secondary btn-sm"
                                OnClick="btnOtraDireccion_Click" />
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Calle</label>
                                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" placeholder="Ej: Av. Corrientes" />
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Número</label>
                                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" placeholder="Ej: 1234" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Localidad</label>
                                <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: San Fernando" />
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Código Postal</label>
                                <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Ej: 1642" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 mb-3">
                                <label class="form-label">Observaciones (opcional)</label>
                                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" 
                                    placeholder="Ej: Entre calles, piso, timbre..." TextMode="MultiLine" Rows="2" />
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- RETIRO EN PUNTO -->
                    <asp:Panel ID="pnlRetiro" runat="server" Visible="false">
                        <div class="alert alert-info">
                            <strong>📍 Dirección del local:</strong><br />
                            Av. Corrientes 1234, Buenos Aires<br />
                            Lunes a Viernes de 9 a 18hs
                        </div>
                    </asp:Panel>
                </div>
            </div>

            <!-- FORMA DE PAGO -->
            <div class="card mb-4">
                <div class="card-header bg-dark text-white">
                    <h5 class="mb-0">💳 Forma de pago</h5>
                </div>
                <div class="card-body">
                    <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-select" />
                </div>
            </div>

        </div>

        <!-- COLUMNA DERECHA - TOTAL -->
        <div class="col-md-4">
            <div class="card sticky-top" style="top: 20px;">
                <div class="card-header bg-dark text-white">
                    <h5 class="mb-0">💰 Total</h5>
                </div>
                <div class="card-body">
                    <div class="d-flex justify-content-between mb-2">
                        <span>Subtotal:</span>
                        <strong><asp:Label ID="lblSubtotal" runat="server" /></strong>
                    </div>
                    <div class="d-flex justify-content-between mb-2">
                        <span>Envío:</span>
                        <strong><asp:Label ID="lblEnvio" runat="server" Text="$ 0,00" /></strong>
                    </div>
                    <hr />
                    <div class="d-flex justify-content-between mb-3">
                        <span><strong>Total:</strong></span>
                        <strong class="text-success fs-5">
                            <asp:Label ID="lblTotal" runat="server" />
                        </strong>
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
                    <asp:Button ID="btnConfirmar" runat="server" 
                        Text="✅ Confirmar compra"
                        CssClass="btn btn-success w-100"
                        OnClick="btnConfirmar_Click" />
                    <a href="Carrito.aspx" class="btn btn-outline-secondary w-100 mt-2">
                        ← Volver al carrito
                    </a>
                </div>
            </div>
        </div>
      </div>
</div>
</asp:Content>