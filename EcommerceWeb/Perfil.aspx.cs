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
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            
if (!IsPostBack)
    {
        if (Session["usuario"] != null)
        {
            Usuario user = (Usuario)Session["usuario"];

            txtNombre.Text = user.Nombre;
            txtApellido.Text = user.Apellido;
            txtEmail.Text = user.Email;
            txtTelefono.Text = user.Telefono;

            // DIRECCIÓN
            DireccioNegocio negocioDir = new DireccioNegocio();
            Direccion dir = negocioDir.obtenerPorUsuario(user.Id);

            if (dir != null)
            {
                txtDireccion.Text = dir.Calle + " " + dir.Numero;
                txtCiudad.Text = dir.Localidad;
                txtCP.Text = dir.CodigoPostal;
            }
        }
        else
        {
            Response.Redirect("Login.aspx"); 
        }
    }



        }


        protected void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string email = txtEmail.Text;
            string telefono = txtTelefono.Text;

            Response.Write("<script>alert('Datos actualizados correctamente');</script>");
        }



        protected void btnCambiarPass_Click(object sender, EventArgs e)
        {
            if (txtPassNueva.Text == txtConfirmarPass.Text)
            {
                Response.Write("<script>alert('Contraseña actualizada');</script>");
            }
            else
            {
                Response.Write("<script>alert('Las contraseñas no coinciden');</script>");
            }
        }


        protected void btnEditarDatos_Click(object sender, EventArgs e)
        {
            txtNombre.ReadOnly = false;
            txtApellido.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtTelefono.ReadOnly = false;

            btnEditarDatos.Visible = false;
            btnGuardarDatos.Visible = true;
        }


        protected void btnGuardarDatos_Click(object sender, EventArgs e)
        {

            Usuario user = (Usuario)Session["usuario"];

            user.Nombre = txtNombre.Text;
            user.Apellido = txtApellido.Text;
            user.Email = txtEmail.Text;
            user.Telefono = txtTelefono.Text;

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.actualizar(user);

            txtNombre.ReadOnly = true;
            txtApellido.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtTelefono.ReadOnly = true;

            btnEditarDatos.Visible = true;
            btnGuardarDatos.Visible = false;

            Response.Write("<script>alert('Datos guardados en base de datos');</script>");

        }


        protected void btnEditarDireccion_Click(object sender, EventArgs e)
        {
            txtDireccion.ReadOnly = false;
            txtCiudad.ReadOnly = false;
       
            txtCP.ReadOnly = false;

            btnEditarDireccion.Visible = false;
            btnGuardarDireccion.Visible = true;
        }

        protected void btnGuardarDireccion_Click(object sender, EventArgs e)
        {
            txtDireccion.ReadOnly = true;
            txtCiudad.ReadOnly = true;
    
            txtCP.ReadOnly = true;

            btnEditarDireccion.Visible = true;
            btnGuardarDireccion.Visible = false;

            Response.Write("<script>alert('Dirección actualizada');</script>");
        }


       


    }
}