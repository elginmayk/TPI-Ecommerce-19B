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
    public partial class ListaFormasEntrega : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
            {
                CargarFormasEntrega();
            }
        }

        private void CargarFormasEntrega()
        {
            FormaEntregaNegocio negocio = new FormaEntregaNegocio();
            gvFormasEntrega.DataSource = negocio.listar();
            gvFormasEntrega.DataBind();
        }

        protected void gvFormasEntrega_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                try
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    FormaEntregaNegocio negocio = new FormaEntregaNegocio();
                    negocio.Eliminar(id);

                    lblMensaje.Text = "Forma de entrega eliminada correctamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    CargarFormasEntrega();
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