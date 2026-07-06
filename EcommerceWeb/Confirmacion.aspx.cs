using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using Dominio;
using Negocio;

namespace EcommerceWeb
{
    public partial class Confirmacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                lblNroPedido.Text = "#" + id;

                int idPedido = int.Parse(id);

                // Mercado Pago agrega estos parámetros a la URL cuando vuelve de pagar
                string estadoMP = Request.QueryString["status"] ?? Request.QueryString["collection_status"];
                bool vieneDeMercadoPagoAprobado = estadoMP == "approved";

                if (vieneDeMercadoPagoAprobado && Session["mailEnviado_" + idPedido] == null)
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    PedidoNegocio pedNegocio = new PedidoNegocio();
                    Pedido pedido = pedNegocio.obtenerPorId(idPedido);

                    if (pedido != null)
                    {
                        EnviarMailConfirmacion(usuario, idPedido, pedido.Total);
                        Session["mailEnviado_" + idPedido] = true;
                    }
                }
            }
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
                System.Diagnostics.Debug.WriteLine("Error al enviar mail: " + ex.Message);
            }
        }
    }
}