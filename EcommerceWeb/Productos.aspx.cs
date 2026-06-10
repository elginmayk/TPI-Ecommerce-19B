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
                CargarProductos();
            }

        }



        private void CargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();

            rptCategorias.DataSource = negocio.listarAgrupados();
            rptCategorias.DataBind();
        }


    }
}