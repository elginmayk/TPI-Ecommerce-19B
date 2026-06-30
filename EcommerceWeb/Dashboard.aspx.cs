using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWeb
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null || ((Usuario)Session["usuario"]).Nivel != Nivel.ADMINISTRADOR)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDashboard();
            }
        }

        private void CargarDashboard()
        {
            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            List<Pedido> pedidos = pedidoNegocio.listar();

            // Totales
            decimal totalVendido = pedidos.Sum(p => p.Total);
            int cantidadPedidos = pedidos.Count;
            decimal ticketPromedio = cantidadPedidos > 0 ? totalVendido / cantidadPedidos : 0;

            lblTotalVendido.Text = "$ " + totalVendido.ToString("N2");
            lblCantidadPedidos.Text = cantidadPedidos.ToString();
            lblTicketPromedio.Text = "$ " + ticketPromedio.ToString("N2");

            // Productos más vendidos
            DetallePedidoNegocio detalleNegocio = new DetallePedidoNegocio();
            List<DetallePedido> detalles = detalleNegocio.listar();

            var topProductos = detalles
                .GroupBy(d => d.Producto.Nombre)
                .Select(g => new { Nombre = g.Key, Cantidad = g.Sum(d => d.Cantidad) })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToList();

            rptTopProductos.DataSource = topProductos;
            rptTopProductos.DataBind();
        }
    }
}