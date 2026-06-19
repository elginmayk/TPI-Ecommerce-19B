using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string Categoria = Request.QueryString["Categoria"];

                if (!string.IsNullOrEmpty(Categoria))
                {
                    CargarProductos(Categoria);
                }
                else
                {
                    CargarProductos();
                }
            }

        }



        private void CargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            rptCategorias.DataSource = negocio.listarAgrupados();
            rptCategorias.DataBind();
        }

        private void CargarProductos(string categoria)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            rptCategorias.DataSource = negocio.ListarCategoria(categoria);
            rptCategorias.DataBind();
        }

        protected void AgregarCarrito_Click(object sender, CommandEventArgs e)
        {
            int idProducto = int.Parse(e.CommandArgument.ToString());

            // Obtener o crear el carrito en Session
            List<int> carrito = Session["carrito"] as List<int>;
            if (carrito == null)
            {
                carrito = new List<int>();
            }

            // Agregar el producto
            carrito.Add(idProducto);
            Session["carrito"] = carrito;

            // Redirigir al carrito
            Response.Redirect("Carrito.aspx");
        }

        protected void rptCategorias_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptProductos = (Repeater)e.Item.FindControl("rptProductos");
                if (rptProductos != null)
                {
                    rptProductos.ItemCommand += new RepeaterCommandEventHandler(AgregarCarrito_Click);
                }
            }
        }
    }
}