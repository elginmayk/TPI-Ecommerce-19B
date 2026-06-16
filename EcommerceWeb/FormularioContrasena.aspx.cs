using Microsoft.Ajax.Utilities;
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

            if (!txtPassword.Text.IsNullOrWhiteSpace() || !txtEmail.Text.IsNullOrWhiteSpace())
                negocio.ModificarPassword(txtEmail.Text, txtPassword.Text);
            else
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Ingrese una contraseña por favor";
            }
        }
    }
}