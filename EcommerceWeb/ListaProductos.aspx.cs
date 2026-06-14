using Dominio;
using EcommerceWeb;
using Negocio;
using System;
using System.Collections.Generic;

namespace EcommerceWeb
{
    public partial class ListaProductos : System.Web.UI.Page
    {
        // Esta lista debe ser pública para que el .aspx la vea
        public List<Producto> ListaProducto { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }
        private void CargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            try
            {
                ListaProducto = negocio.listarTodos();
                dgvArticulos.DataSource = ListaProducto;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar productos: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void dgvArticulos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                try
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    ProductoNegocio negocio = new ProductoNegocio();
                    negocio.Eliminar(id);

                    lblMensaje.Text = "Producto eliminado correctamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    CargarProductos();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar: " + ex.Message;
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Visible = true;
                }
            }
        }
    }
}