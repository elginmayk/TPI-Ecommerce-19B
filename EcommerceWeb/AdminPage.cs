using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio;

namespace EcommerceWeb
{
    public class AdminPage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["usuario"] == null || ((Usuario)Session["usuario"]).Nivel != Nivel.ADMINISTRADOR)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}