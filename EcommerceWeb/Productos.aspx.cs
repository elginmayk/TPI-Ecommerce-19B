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
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                ddlCategoria.DataSource = categoriaNegocio.listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Todas las categorías", "0"));

                string idCat = Request.QueryString["idCat"];
                string Categoria = Request.QueryString["Categoria"];

                if (!string.IsNullOrEmpty(idCat))
                {
                    CategoriaNegocio catNegocio = new CategoriaNegocio();
                    string nombreCategoria = catNegocio.listar()
                        .FirstOrDefault(c => c.Id == int.Parse(idCat))?.Nombre;
                    if (!string.IsNullOrEmpty(nombreCategoria))
                        CargarProductos(nombreCategoria);
                    else
                        CargarProductos();
                }
                else if (!string.IsNullOrEmpty(Categoria))
                    CargarProductos(Categoria);
                else
                    CargarProductos();
            }
            else
            {
                // En PostBack recargamos el dropdown pero mantenemos el valor seleccionado
                string valorSeleccionado = Request.Form[ddlCategoria.UniqueID];
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                ddlCategoria.DataSource = categoriaNegocio.listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Todas las categorías", "0"));
                if (!string.IsNullOrEmpty(valorSeleccionado))
                    ddlCategoria.SelectedValue = valorSeleccionado;
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

            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = negocio.listarTodos()
                                       .FirstOrDefault(x => x.Id == idProducto);

            if (producto == null)
                return;

            if (producto.Stock <= 0)
            {
                return;
            }

            List<int> carrito = Session["carrito"] as List<int>;

            if (carrito == null)
                carrito = new List<int>();

            carrito.Add(idProducto);

            Session["carrito"] = carrito;

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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text.Trim();
            int idCategoria = int.Parse(ddlCategoria.SelectedValue);
            string orden = ddlOrden.SelectedValue;

            ProductoNegocio negocio = new ProductoNegocio();
            var productos = negocio.buscar(busqueda, idCategoria);

            switch (orden)
            {
                case "nombre_asc":
                    productos = productos
                        .Select(g => g.OrderBy(p => p.Nombre)
                            .GroupBy(p => p.CategoriaNombre).First())
                        .ToList();
                    break;
                case "nombre_desc":
                    productos = productos
                        .Select(g => g.OrderByDescending(p => p.Nombre)
                            .GroupBy(p => p.CategoriaNombre).First())
                        .ToList();
                    break;
                case "precio_asc":
                    productos = productos.Select(g => g.OrderBy(p => p.Precio)
                        .GroupBy(p => p.CategoriaNombre).First()).ToList();
                    break;
                case "precio_desc":
                    productos = productos.Select(g => g.OrderByDescending(p => p.Precio)
                        .GroupBy(p => p.CategoriaNombre).First()).ToList();
                    break;
            }

            rptCategorias.DataSource = productos;
            rptCategorias.DataBind();
        }
    }
}