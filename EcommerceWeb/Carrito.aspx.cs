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
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            List<int> idsCarrito = Session["carrito"] as List<int>;

            if (idsCarrito == null || idsCarrito.Count == 0)
            {
                // carrito vacío
                return;
            }

            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> productos = new List<Producto>();

            foreach (int id in idsCarrito)
            {
                Producto p = negocio.obtenerPorId(id);
                if (p != null)
                    productos.Add(p);
            }

            rptCarrito.DataSource = productos;
            rptCarrito.DataBind();

            decimal total = productos.Sum(p => p.Precio);
            lblTotal.Text = "$ " + total.ToString("N2");
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idProducto = int.Parse(e.CommandArgument.ToString());
                List<int> carrito = Session["carrito"] as List<int>;
                if (carrito != null)
                {
                    carrito.RemoveAll(x => x == idProducto);
                    Session["carrito"] = carrito;
                }
                Response.Redirect("Carrito.aspx");
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }

    }
}