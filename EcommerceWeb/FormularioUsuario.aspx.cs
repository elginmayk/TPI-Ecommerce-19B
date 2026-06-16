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
    public partial class FormularioUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlRol.Visible = false;
                lblRol.Visible = false;

                if (Request.QueryString["id"] != null)
                {
                    litTitulo.Text = "Editar Usuario";
                    //lblPasswordHelp.Visible = true; // modo edición: aviso de "opcional"

                    int id = int.Parse(Request.QueryString["id"]);
                    CargarUsuario(id);
                }
                else
                {
                    litTitulo.Text = "Nuevo Usuario";
                    //lblPasswordHelp.Visible = false; // modo alta: contraseña obligatoria
                }
            }
        }

        private void CargarUsuario(int id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.listar().Find(u => u.Id == id);

            if (usuario != null)
            {
                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
                txtEmail.Text = usuario.Email;
                txtTelefono.Text = usuario.Telefono;
                ddlRol.SelectedValue = usuario.Rol.ToString();
                // txtPassword queda vacío a propósito
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                if (Request.QueryString["id"] != null)
                {
                    // modo edición
                    int id = int.Parse(Request.QueryString["id"]);

                    Usuario usuario = new Usuario();
                    usuario.Id = id;
                    usuario.Nombre = txtNombre.Text.Trim();
                    usuario.Apellido = txtApellido.Text.Trim();
                    usuario.Telefono = txtTelefono.Text.Trim();

                    negocio.Modificar(usuario);
                    if (usuario.Nivel == Nivel.ADMINISTRADOR)
                        negocio.ModificarRol(id, int.Parse(ddlRol.SelectedValue));

                    //if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    //{
                    //    negocio.ModificarPassword(id, txtPassword.Text.Trim());
                    //}
                }
                else
                {
                    // modo alta
                    Usuario usuario = new Usuario();
                    usuario.Nombre = txtNombre.Text.Trim();
                    usuario.Apellido = txtApellido.Text.Trim();
                    usuario.Email = txtEmail.Text.Trim();
                    usuario.Telefono = txtTelefono.Text.Trim();
                    if (usuario.Nivel == Nivel.ADMINISTRADOR)
                        usuario.Rol = int.Parse(ddlRol.SelectedValue);
                    //usuario.Password = txtPassword.Text.Trim();

                    negocio.Agregar(usuario);
                }

                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al guardar: " + ex.Message;
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

            // En alta, la contraseña es obligatoria
            //if (Request.QueryString["id"] == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            //{
            //    lblError.Text = "La contraseña es obligatoria.";
            //    lblError.Visible = true;
            //    return false;
            //}

            lblError.Visible = false;
            return true;
        }
    }
}