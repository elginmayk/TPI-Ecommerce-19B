using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWeb
{
    public partial class Confirmacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                lblNroPedido.Text = "#" + id;
            }

        }
    }
}