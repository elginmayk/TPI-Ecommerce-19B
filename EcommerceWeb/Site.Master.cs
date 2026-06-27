using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWeb
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMenuCategorias();
                ConfigurarNavbarUsuario();
            }
        }

        private void CargarMenuCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            rptCategorias.DataSource = negocio.listarNav();
            rptCategorias.DataBind();
        }

        private void ConfigurarNavbarUsuario()
        {
            menuAdministracion.Visible = false;
            btnIngresar.InnerText = "Login";
            btnIngresar.HRef = "~/Login.aspx";

            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];

                btnIngresar.Visible = false;
                btnIngresar.HRef = "#";

                lblSaludo.Visible = true;
                lblSaludo.InnerText = "Hola, " + usuario.Nombre;
                menuUsuario.Visible = true;

                if (usuario.Nivel == Nivel.ADMINISTRADOR)
                {
                    menuAdministracion.Visible = true;
                }
            }
            else
            {
                btnIngresar.Visible = true;
                lblSaludo.Visible = false;
                menuAdministracion.Visible = false;
            }

            List<int> carrito = Session["carrito"] as List<int>;
            if (carrito != null && carrito.Count > 0)
            {
                lblCantidadCarrito.Text = carrito.Count.ToString();
                lblCantidadCarrito.Visible = true;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Session.Abandon();

            Response.Redirect("~/Default.aspx");
        }
    }
}