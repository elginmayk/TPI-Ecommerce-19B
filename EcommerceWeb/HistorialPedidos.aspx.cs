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
    public partial class HistorialPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            PedidoNegocio negocio = new PedidoNegocio();

            List<Pedido> pedidos = negocio.listar()
                .Where(p => p.Usuario.Id == usuario.Id)
                .OrderByDescending(p => p.Fecha)
                .ToList();

            if (pedidos.Count == 0)
            {
                lblSinPedidos.Visible = true;
                rptPedidos.Visible = false;
            }
            else
            {
                rptPedidos.DataSource = pedidos;
                rptPedidos.DataBind();
            }
        }
    }
}