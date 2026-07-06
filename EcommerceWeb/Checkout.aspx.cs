using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;

namespace EcommerceWeb
{
    public partial class Checkout : System.Web.UI.Page
    {
        private const decimal COSTO_ENVIO = 2000m;
        private const decimal TASA_INTERES_6_CUOTAS = 0.15m;
        private const decimal TASA_INTERES_12_CUOTAS = 0.35m;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx?returnUrl=Checkout.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarProductos();
                CargarFormasPago();
            }

        }

        private void CargarProductos()
        {
            List<int> idsCarrito = Session["carrito"] as List<int>;
            if (idsCarrito == null || idsCarrito.Count == 0)
            {
                Response.Redirect("Carrito.aspx");
                return;
            }

            ProductoNegocio prodNegocio = new ProductoNegocio();
            List<Producto> productos = new List<Producto>();

            foreach (int id in idsCarrito)
            {
                Producto p = prodNegocio.obtenerPorId(id);
                if (p != null)
                    productos.Add(p);
            }

            rptProductos.DataSource = productos;
            rptProductos.DataBind();

            decimal subtotal = productos.Sum(p => p.Precio);
            lblSubtotal.Text = "$ " + subtotal.ToString("N2");
            lblTotal.Text = "$ " + subtotal.ToString("N2");
            Session["subtotal"] = subtotal;
        }

        private void CargarFormasPago()
        {
            FormaPagoNegocio fpNegocio = new FormaPagoNegocio();
            ddlFormaPago.DataSource = fpNegocio.listar();
            ddlFormaPago.DataTextField = "Nombre";
            ddlFormaPago.DataValueField = "Id";
            ddlFormaPago.DataBind();
        }

        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string formaPago = ddlFormaPago.SelectedItem.Text;

            pnlTarjeta.Visible = false;
            pnlCuotas.Visible = false;
            pnlTransferencia.Visible = false;
            pnlMercadoPago.Visible = false;
            pnlEfectivo.Visible = false;

            switch (formaPago)
            {
                case "Efectivo":
                    pnlEfectivo.Visible = true;
                    break;

                case "Tarjeta de débito":
                    pnlTarjeta.Visible = true;
                    break;

                case "Tarjeta de crédito":
                    pnlTarjeta.Visible = true;
                    pnlCuotas.Visible = true;
                    break;

                case "Transferencia bancaria":
                    pnlTransferencia.Visible = true;
                    imgQrAlias.ImageUrl = "https://api.qrserver.com/v1/create-qr-code/?size=160x160&data="
                        + Server.UrlEncode("alias:thedibastore.mp");
                    break;

                case "Mercado Pago":
                    pnlMercadoPago.Visible = true;
                    break;
            }

            rfvNumeroTarjeta.Enabled = pnlTarjeta.Visible;
            rfvTitular.Enabled = pnlTarjeta.Visible;
            rfvVencimiento.Enabled = pnlTarjeta.Visible;
            rfvCodSeguridad.Enabled = pnlTarjeta.Visible;

            RecalcularTotal();
        }

        protected void rblCuotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularTotal();
        }

        private void RecalcularTotal()
        {
            decimal subtotal = (decimal)Session["subtotal"];
            decimal envio = rbEnvio.Checked ? COSTO_ENVIO : 0;
            decimal baseCalculo = subtotal + envio;

            lblEnvio.Text = "$ " + envio.ToString("N2");

            bool esCredito = ddlFormaPago.SelectedItem != null
                && ddlFormaPago.SelectedItem.Text == "Tarjeta de crédito";

            if (esCredito && pnlCuotas.Visible)
            {
                int cuotas = int.Parse(rblCuotas.SelectedValue);

                decimal tasaInteres = 0;
                if (cuotas == 6) tasaInteres = TASA_INTERES_6_CUOTAS;
                else if (cuotas == 12) tasaInteres = TASA_INTERES_12_CUOTAS;

                decimal totalConInteres = baseCalculo * (1 + tasaInteres);
                decimal valorCuota = totalConInteres / cuotas;

                Session["totalFinal"] = totalConInteres;

                lblDetalleCuotas.Text = tasaInteres == 0
                    ? $"{cuotas} cuota{(cuotas > 1 ? "s" : "")} de $ {valorCuota:N2} — Total: $ {totalConInteres:N2} (sin interés)"
                    : $"{cuotas} cuotas de $ {valorCuota:N2} — Total: $ {totalConInteres:N2} (interés del {tasaInteres:P0})";

                lblTotal.Text = "$ " + totalConInteres.ToString("N2");
            }
            else
            {
                Session["totalFinal"] = baseCalculo;
                lblTotal.Text = "$ " + baseCalculo.ToString("N2");
            }
        }

        protected void rbEntrega_Changed(object sender, EventArgs e)
        {
            if (rbEnvio.Checked)
            {
                pnlDireccion.Visible = true;
                pnlRetiro.Visible = false;
            }
            else
            {
                pnlDireccion.Visible = false;
                pnlRetiro.Visible = true;
            }

            RecalcularTotal();
        }

        private void EnviarMailConfirmacion(Usuario usuario, int idPedido, decimal total)
        {
            try
            {
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
                string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress(smtpUser, "The Diba Store");
                mensaje.To.Add(usuario.Email);
                mensaje.Subject = $"Confirmación de compra #{idPedido} - The Diba Store";
                mensaje.IsBodyHtml = true;
                mensaje.Body = $@"
                    <h2>¡Gracias por tu compra, {usuario.Nombre}!</h2>
                    <p>Tu pedido <strong>#{idPedido}</strong> fue registrado correctamente.</p>
                    <p><strong>Total:</strong> $ {total:N2}</p>
                    <p>Te vamos a avisar cuando esté en camino.</p>
                    <p>— The Diba Store</p>";

                using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mensaje);
                }
            }
            catch (Exception ex)
            {
                // Si falla el mail no queremos que se caiga toda la compra,
                // el pedido ya se guardó en la base correctamente.
                System.Diagnostics.Debug.WriteLine("Error al enviar mail: " + ex.Message);
            }
        }

        private string CrearPreferenciaMercadoPago(int idPedido, decimal total)
        {
            MercadoPagoConfig.AccessToken = ConfigurationManager.AppSettings["MercadoPagoAccessToken"];

            string urlBase = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath.TrimEnd('/');

            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = $"Pedido #{idPedido} - The Diba Store",
                        Quantity = 1,
                        CurrencyId = "ARS",
                        UnitPrice = total
                    }
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = urlBase + "/Confirmacion.aspx?id=" + idPedido,
                    Failure = urlBase + "/Checkout.aspx",
                    Pending = urlBase + "/Confirmacion.aspx?id=" + idPedido
                }
            };

            var client = new PreferenceClient();
            Preference preference = client.Create(request);

            return preference.SandboxInitPoint;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (!rbEnvio.Checked && !rbRetiro.Checked)
            {
                lblError.Text = "Por favor seleccioná una forma de entrega.";
                lblError.Visible = true;
                return;
            }

            if (rbEnvio.Checked && (string.IsNullOrWhiteSpace(txtCalle.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) ||
                string.IsNullOrWhiteSpace(txtLocalidad.Text) ||
                string.IsNullOrWhiteSpace(txtCodigoPostal.Text)))
            {
                lblError.Text = "Por favor completá todos los campos de dirección.";
                lblError.Visible = true;
                return;
            }

            Usuario usuario = (Usuario)Session["usuario"];
            List<int> idsCarrito = Session["carrito"] as List<int>;

            // Guardar dirección si eligió envío
            int? idDireccion = null;
            if (rbEnvio.Checked)
            {
                DireccioNegocio dirNegocio = new DireccioNegocio();
                Direccion direccion = new Direccion();
                direccion.Usuario = usuario;
                direccion.Calle = txtCalle.Text.Trim();
                direccion.Numero = txtNumero.Text.Trim();
                direccion.Localidad = txtLocalidad.Text.Trim();
                direccion.CodigoPostal = txtCodigoPostal.Text.Trim();
                direccion.Observaciones = txtObservaciones.Text.Trim();

                idDireccion = dirNegocio.Agregar(direccion);
            }

            // Crear pedido
            Pedido pedido = new Pedido();
            pedido.Usuario = usuario;
            pedido.Total = Session["totalFinal"] != null
                ? (decimal)Session["totalFinal"]
                : (decimal)Session["subtotal"];
            pedido.Estado = "Pendiente";
            pedido.Fecha = DateTime.Now;
            pedido.FormaPago = new FormaPago { Id = int.Parse(ddlFormaPago.SelectedValue) };
            pedido.FormaEntrega = new FormaEntrega { Id = rbEnvio.Checked ? 1 : 2 };
            if (idDireccion.HasValue)
                pedido.Direccion = new Direccion { Id = idDireccion.Value };

            PedidoNegocio pedNegocio = new PedidoNegocio();
            int idPedido = pedNegocio.Agregar(pedido);


            // Agregar detalles y descontar stock
            DetallePedidoNegocio detNegocio = new DetallePedidoNegocio();
            ProductoNegocio prodNegocio = new ProductoNegocio();

            foreach (int idProducto in idsCarrito)
            {
                DetallePedido detalle = new DetallePedido();
                detalle.Pedido = new Pedido { Id = idPedido };
                detalle.Producto = new Producto { Id = idProducto };
                detalle.Cantidad = 1;

                detNegocio.Agregar(detalle);

                // Descontar stock
                prodNegocio.DescontarStock(idProducto);
            }


            Session["carrito"] = null;

            bool esMercadoPago = ddlFormaPago.SelectedItem.Text == "Mercado Pago";

            // Para las demás formas de pago, "Confirmar compra" ya es el paso final -> mandamos el mail ahora.
            // Para Mercado Pago, todavía falta que la persona pague afuera de nuestro sitio,
            // así que el mail se manda recién en Confirmacion.aspx si el pago vuelve aprobado.
            if (!esMercadoPago)
            {
                EnviarMailConfirmacion(usuario, idPedido, pedido.Total);
            }

            string linkMercadoPago = null;

            if (esMercadoPago)
            {
                try
                {
                    linkMercadoPago = CrearPreferenciaMercadoPago(idPedido, pedido.Total);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al crear preferencia de Mercado Pago: " + ex.Message);
                }
            }

            if (!string.IsNullOrEmpty(linkMercadoPago))
            {
                Response.Redirect(linkMercadoPago);
            }
            else
            {
                Response.Redirect("Confirmacion.aspx?id=" + idPedido);
            }
        }

        protected void btnUsarMiDireccion_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            DireccioNegocio negocioDir = new DireccioNegocio();
            Direccion dir = negocioDir.obtenerPorUsuario(usuario.Id);

            if (dir != null)
            {
                txtCalle.Text = dir.Calle;
                txtNumero.Text = dir.Numero;
                txtLocalidad.Text = dir.Localidad;
                txtCodigoPostal.Text = dir.CodigoPostal;
                txtObservaciones.Text = dir.Observaciones;

                txtCalle.ReadOnly = true;
                txtNumero.ReadOnly = true;
                txtLocalidad.ReadOnly = true;
                txtCodigoPostal.ReadOnly = true;
                txtObservaciones.ReadOnly = true;
            }
            else
            {
                lblError.Text = "No tenés una dirección guardada en tu perfil.";
                lblError.Visible = true;
            }
        }

        protected void btnOtraDireccion_Click(object sender, EventArgs e)
        {
            txtCalle.Text = "";
            txtNumero.Text = "";
            txtLocalidad.Text = "";
            txtCodigoPostal.Text = "";
            txtObservaciones.Text = "";

            txtCalle.ReadOnly = false;
            txtNumero.ReadOnly = false;
            txtLocalidad.ReadOnly = false;
            txtCodigoPostal.ReadOnly = false;
            txtObservaciones.ReadOnly = false;

            lblError.Visible = false;
        }
    }
}