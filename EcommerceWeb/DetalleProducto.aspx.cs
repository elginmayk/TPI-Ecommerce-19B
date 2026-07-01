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
    public partial class DetalleProducto : System.Web.UI.Page
    {
        private int IdProducto
        {
            get { return int.Parse(Request.QueryString["id"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out _))
            {
                MostrarProductoNoEncontrado();
                return;
            }

            if (!IsPostBack)
            {
                CargarProducto();
                CargarResenas();
                PrepararFormularioResena();
            }
        }

        private void CargarProducto()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto p = negocio.obtenerPorId(IdProducto);

            if (p == null)
            {
                MostrarProductoNoEncontrado();
                return;
            }

            litNombre.Text = p.Nombre;
            litCategoria.Text = p.CategoriaNombre;
            litDescripcion.Text = p.Descripcion;
            litPrecio.Text = "$ " + p.Precio.ToString("N2");
            imgProducto.ImageUrl = p.ImagenUrl;

            litPromedio.Text = p.CantidadResenas > 0
                ? p.PromedioResena.ToString("N1")
                : "Sin puntuar";
            litCantidadResenas.Text = p.CantidadResenas > 0
                ? $" ({p.CantidadResenas} reseña{(p.CantidadResenas == 1 ? "" : "s")})"
                : "";
        }

        private void CargarResenas()
        {
            ResenaNegocio resenaNegocio = new ResenaNegocio();
            List<Resena> resenas = resenaNegocio.ListarPorProducto(IdProducto);

            if (resenas.Count == 0)
            {
                lblSinResenas.Visible = true;
                rptResenas.Visible = false;
            }
            else
            {
                rptResenas.DataSource = resenas;
                rptResenas.DataBind();
            }
        }

        private void PrepararFormularioResena()
        {
            if (Session["usuario"] == null)
            {
                pnlFormResena.Visible = false;
                pnlLoginRequerido.Visible = true;
                return;
            }

            // Si el usuario ya reseñó este producto, precargamos su reseña para que la edite.
            Usuario usuario = (Usuario)Session["usuario"];
            ResenaNegocio resenaNegocio = new ResenaNegocio();
            Resena propia = resenaNegocio.ObtenerPorUsuarioYProducto(usuario.Id, IdProducto);

            if (propia != null)
            {
                ddlPuntuacion.SelectedValue = propia.Puntuacion.ToString();
                txtComentario.Text = propia.Comentario;
                litTituloForm.Text = "Editar tu reseña";
                btnGuardarResena.Text = "Actualizar reseña";
            }
        }

        private void MostrarProductoNoEncontrado()
        {
            pnlProducto.Visible = false;
            lblProductoNoEncontrado.Visible = true;
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            List<int> carrito = Session["carrito"] as List<int> ?? new List<int>();
            carrito.Add(IdProducto);
            Session["carrito"] = carrito;
            Response.Redirect("Carrito.aspx");
        }

        protected void btnGuardarResena_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx?returnUrl=DetalleProducto.aspx?id=" + IdProducto);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComentario.Text))
            {
                lblErrorResena.Text = "Escribí un comentario antes de publicar tu reseña.";
                lblErrorResena.Visible = true;
                return;
            }

            Usuario usuario = (Usuario)Session["usuario"];

            Resena resena = new Resena();
            resena.Usuario = new Usuario { Id = usuario.Id };
            resena.Producto = new Producto { Id = IdProducto };
            resena.Puntuacion = int.Parse(ddlPuntuacion.SelectedValue);
            resena.Comentario = txtComentario.Text.Trim();

            ResenaNegocio resenaNegocio = new ResenaNegocio();
            resenaNegocio.Guardar(resena);

            // Recargamos para mostrar la reseña ya publicada y el promedio actualizado.
            Response.Redirect("DetalleProducto.aspx?id=" + IdProducto);
        }
    }
}