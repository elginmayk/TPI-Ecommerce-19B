using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario;

            try
            {
                usuario = new Usuario(txtEmail.Text, txtPassword.Text);
                if(negocio.Login(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMensajeError.Text = "Usuario o contraseña incorrectos";
                    lblMensajeError.Visible = true;
                }
            }
            //catch (Exception ex)
            catch(Exception)
            {
                //throw ex;
                lblMensajeError.Text = "Usuario o contraseña incorrectos";
                lblMensajeError.Visible = true;
            }
        }
    }
}