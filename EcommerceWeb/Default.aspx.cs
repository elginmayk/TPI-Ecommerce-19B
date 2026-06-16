using AccesoDatos;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;



namespace EcommerceWeb
{
    public partial class _Default : System.Web.UI.Page
    {
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

            rptProductos.DataSource = negocio.listarDestacados();
            rptProductos.DataBind();
        }

        protected void AgregarCarrito_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {}
    }
}