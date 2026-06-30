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
    public partial class ListaFormasPago : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFormasPago();
            }
        }

        private void CargarFormasPago()
        {
            FormaPagoNegocio negocio = new FormaPagoNegocio();
            gvFormasPago.DataSource = negocio.listar();
            gvFormasPago.DataBind();
        }

        protected void gvFormasPago_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                try
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    FormaPagoNegocio negocio = new FormaPagoNegocio();
                    negocio.Eliminar(id);

                    lblMensaje.Text = "Forma de pago eliminada correctamente.";
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Visible = true;

                    CargarFormasPago();
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