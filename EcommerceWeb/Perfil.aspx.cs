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
                        txtDireccion.Text = dir.Calle;
                        txtNumero.Text = dir.Numero;
                        txtCiudad.Text = dir.Localidad;
                        txtCP.Text = dir.CodigoPostal;
                    }
                    pnlMensaje.Visible = false;
                    switch(Request.QueryString["success"])
                    {
                        case "-3":  lblMensajeTexto.Text = "Por favor completá todos los campos de dirección.";
                                    pnlMensaje.CssClass = "alert alert-danger alert-dismissible fade show"; // Color ROJO
                                    pnlMensaje.Visible = true;
                                    break;
                        case "-2":  lblMensajeTexto.Text = "Por favor completá nombre, apellido y email.";
                                    pnlMensaje.CssClass = "alert alert-danger alert-dismissible fade show";
                                    pnlMensaje.Visible = true;
                                    break;
                        case "-1":  lblMensajeTexto.Text = "Las contraseñas no coinciden";
                                    pnlMensaje.CssClass = "alert alert-danger alert-dismissible fade show";
                                    pnlMensaje.Visible = true;
                                    break;
                        case "1":   lblMensajeTexto.Text = "Datos actualizados correctamente";
                                    pnlMensaje.CssClass = "alert alert-success alert-dismissible fade show";
                                    pnlMensaje.Visible = true;
                                    break;
                        case "2":   lblMensajeTexto.Text = "Contraseña actualizada";
                                    pnlMensaje.CssClass = "alert alert-success alert-dismissible fade show";
                                    pnlMensaje.Visible = true;
                                    break;
                        case "3":   lblMensajeTexto.Text = "Datos guardados en base de datos";
                                    pnlMensaje.CssClass = "alert alert-success alert-dismissible fade show";
                                    pnlMensaje.Visible = true;
                                    break;
                        case "4":   lblMensajeTexto.Text = "Dirección guardada correctamente";
                                    pnlMensaje.CssClass = "alert alert-success alert-dismissible fade show";
                                    pnlMensaje.Visible = true;
                                    break;
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

            //Response.Write("<script>alert('Datos actualizados correctamente');</script>");
            Response.Redirect("Perfil.aspx?success=1");
        }



        protected void btnCambiarPass_Click(object sender, EventArgs e)
        {
            if (txtPassNueva.Text == txtConfirmarPass.Text)
            {
                //Response.Write("<script>alert('Contraseña actualizada');</script>");
                Response.Redirect("Perfil.aspx?success=2");
            }
            else
            {
                //Response.Write("<script>alert('Las contraseñas no coinciden');</script>");
                Response.Redirect("Perfil.aspx?success=-1");
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
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                //Response.Write("<script>alert('Por favor completá nombre, apellido y email.');</script>");
                Response.Redirect("Perfil.aspx?success=-2");
                return;
            }

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

            //Response.Write("<script>alert('Datos guardados en base de datos');</script>");
            Response.Redirect("Perfil.aspx?success=3");

        }


        protected void btnEditarDireccion_Click(object sender, EventArgs e)
        {
            txtDireccion.ReadOnly = false;
            txtNumero.ReadOnly = false;
            txtCiudad.ReadOnly = false;
            txtCP.ReadOnly = false;

            btnEditarDireccion.Visible = false;
            btnGuardarDireccion.Visible = true;

        }

        protected void btnGuardarDireccion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) ||
                string.IsNullOrWhiteSpace(txtCiudad.Text) ||
                string.IsNullOrWhiteSpace(txtCP.Text))
            {
                //Response.Write("<script>alert('Por favor completá todos los campos de dirección.');</script>");
                Response.Redirect("Perfil.aspx?success=-3");
                return;
            }

            Usuario user = (Usuario)Session["usuario"];
            DireccioNegocio negocioDir = new DireccioNegocio();
            Direccion dirExistente = negocioDir.obtenerPorUsuario(user.Id);

            if (dirExistente != null)
            {
                dirExistente.Calle = txtDireccion.Text;
                dirExistente.Numero = txtNumero.Text;
                dirExistente.Localidad = txtCiudad.Text;
                dirExistente.CodigoPostal = txtCP.Text;
                dirExistente.Observaciones = "";
                negocioDir.Modificar(dirExistente);
            }
            else
            {
                Direccion nuevaDir = new Direccion();
                nuevaDir.Calle = txtDireccion.Text;
                nuevaDir.Numero = txtNumero.Text;
                nuevaDir.Localidad = txtCiudad.Text;
                nuevaDir.CodigoPostal = txtCP.Text;
                nuevaDir.Observaciones = "";
                nuevaDir.Usuario = user;
                negocioDir.Agregar(nuevaDir);
            }

            txtDireccion.ReadOnly = true;
            txtNumero.ReadOnly = true;
            txtCiudad.ReadOnly = true;
            txtCP.ReadOnly = true;

            btnEditarDireccion.Visible = true;
            btnGuardarDireccion.Visible = false;

            //Response.Write("<script>alert('Dirección guardada correctamente');</script>");
            Response.Redirect("Perfil.aspx?success=4");
        }
    }
}