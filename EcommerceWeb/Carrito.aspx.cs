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
                lblVacio.Visible = true;
                btnFinalizar.Visible = false;
                return;
            }

            ProductoNegocio negocio = new ProductoNegocio();
            var agrupado = idsCarrito
                .GroupBy(id => id)
                .Select(g => {
                    Producto p = negocio.obtenerPorId(g.Key);
                    return new
                    {
                        IdProducto = p.Id,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        Cantidad = g.Count(),
                        Subtotal = p.Precio * g.Count()
                    };
                }).ToList();

            rptCarrito.DataSource = agrupado;
            rptCarrito.DataBind();

            decimal total = agrupado.Sum(x => x.Subtotal);
            lblTotal.Text = "$ " + total.ToString("N2");
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idProducto = int.Parse(e.CommandArgument.ToString());
            List<int> carrito = Session["carrito"] as List<int>;

            if (e.CommandName == "Eliminar")
            {
                if (carrito != null)
                {
                    carrito.RemoveAll(x => x == idProducto);
                    Session["carrito"] = carrito;
                }
            }
            else if (e.CommandName == "Restar")
            {
                if (carrito != null)
                {
                    int index = carrito.IndexOf(idProducto);
                    if (index >= 0)
                        carrito.RemoveAt(index);
                    Session["carrito"] = carrito;
                }
            }
            else if (e.CommandName == "Sumar")
            {
                if (carrito != null)
                {
                    carrito.Add(idProducto);
                    Session["carrito"] = carrito;
                }
            }

            Response.Redirect("Carrito.aspx");
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }

    }
}