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
    public partial class FormularioCategoria : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    // modo edición
                    litTitulo.Text = "Editar Categoría";
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarCategoria(id);
                }
                else
                {
                    // modo alta
                    litTitulo.Text = "Nueva Categoría";
                }
            }
        }

        private void CargarCategoria(int id)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = negocio.listar().Find(c => c.Id == id);

            if (categoria != null)
            {
                txtNombre.Text = categoria.Nombre;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                Categoria categoria = new Categoria();
                categoria.Nombre = txtNombre.Text.Trim();

                CategoriaNegocio negocio = new CategoriaNegocio();

                if (Request.QueryString["id"] != null)
                {
                    // modificar
                    categoria.Id = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(categoria);
                }
                else
                {
                    // agregar
                    negocio.Agregar(categoria);
                }

                Response.Redirect("ListaCategorias.aspx");
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

            lblError.Visible = false;
            return true;
        }
    }
}