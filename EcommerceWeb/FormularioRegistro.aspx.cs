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
    public partial class FormularioRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.Nombre = txtNombre.Text.Trim();
                nuevoUsuario.Apellido = txtApellido.Text.Trim();
                nuevoUsuario.Email = txtEmail.Text.Trim();
                nuevoUsuario.Telefono = txtTelefono.Text.Trim();

                //Se fuerza a que cualquier registro público sea rol USUARIO.
                nuevoUsuario.Rol = (int)Nivel.USUARIO;
                nuevoUsuario.Password = txtPassword.Text.Trim();

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Agregar(nuevoUsuario);

                Response.Redirect("~/Login.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al registrarse: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblError.Text = "Nombre y Apellido son obligatorios.";
                lblError.Visible = true;
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                lblError.Text = "Ingrese un email válido.";
                lblError.Visible = true;
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "La contraseña es obligatoria.";
                lblError.Visible = true;
                return false;
            }

            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                lblError.Text = "Las constraseñas no coinciden";
                lblError.Visible = true;
                return false;
            }

            return true;
        }
    }
}