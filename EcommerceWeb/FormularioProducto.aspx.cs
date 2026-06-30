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
    public partial class FormularioProducto : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarProducto(id);
                }
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            ddlCategoria.DataSource = negocio.listar();
            ddlCategoria.DataTextField = "Nombre";   // lo que se muestra
            ddlCategoria.DataValueField = "Id";      // lo que se guarda
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
        }

        private void CargarProducto(int id)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = negocio.listarTodos().Find(p => p.Id == id);

            if (producto != null)
            {
                txtNombre.Text = producto.Nombre;
                txtDescripcion.Text = producto.Descripcion;
                txtPrecio.Text = producto.Precio.ToString();
                txtStock.Text = producto.Stock.ToString();
                txtUrlImagen.Text = producto.ImagenUrl;
                chkEstado.Checked = producto.Estado;
                ddlCategoria.SelectedValue = producto.Categoria.Id.ToString();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                Producto nuevo = new Producto();
                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Descripcion = txtDescripcion.Text.Trim();
                nuevo.Precio = decimal.Parse(txtPrecio.Text.Trim());
                nuevo.Stock = int.Parse(txtStock.Text.Trim());
                nuevo.ImagenUrl = txtUrlImagen.Text.Trim();
                nuevo.Estado = chkEstado.Checked;
                nuevo.Categoria = new Categoria();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);

                ProductoNegocio negocio = new ProductoNegocio();
                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(nuevo);
                }
                else
                {
                    negocio.Agregar(nuevo);
                }

                Response.Redirect("ListaProductos.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al guardar: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblError.Text = "El nombre es obligatorio.";
                lblError.Visible = true;
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out _))
            {
                lblError.Text = "El precio debe ser un número válido.";
                lblError.Visible = true;
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtStock.Text) || !int.TryParse(txtStock.Text, out _))
            {
                lblError.Text = "El stock debe ser un número entero.";
                lblError.Visible = true;
                return false;
            }
            if (ddlCategoria.SelectedValue == "0")
            {
                lblError.Text = "Debe seleccionar una categoría.";
                lblError.Visible = true;
                return false;
            }

            lblError.Visible = false;
            return true;
        }
    }
}