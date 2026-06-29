using Microsoft.Ajax.Utilities;
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
    public partial class FormularioContrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void btnConfirmar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Ingrese una contraseña por favor";
                return;
            }

            Usuario usuario = negocio.obtenerPorEmail(txtEmail.Text);
            if (usuario == null)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "No existe una cuenta con ese email.";
                return;
            }

            negocio.ModificarPassword(txtEmail.Text, txtPassword.Text);
            Response.Redirect("~/Login.aspx");
        }
    }
}