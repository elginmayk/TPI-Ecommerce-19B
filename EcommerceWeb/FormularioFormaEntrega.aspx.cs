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
    public partial class FormularioFormaEntrega : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    litTitulo.Text = "Editar Forma de Entrega";
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarFormaEntrega(id);
                }
                else
                {
                    litTitulo.Text = "Nueva Forma de Entrega";
                }
            }
        }

        private void CargarFormaEntrega(int id)
        {
            FormaEntregaNegocio negocio = new FormaEntregaNegocio();
            FormaEntrega entrega = negocio.listar().Find(f => f.Id == id);

            if (entrega != null)
            {
                txtNombre.Text = entrega.Nombre;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                FormaEntrega entrega = new FormaEntrega();
                entrega.Nombre = txtNombre.Text.Trim();

                FormaEntregaNegocio negocio = new FormaEntregaNegocio();

                if (Request.QueryString["id"] != null)
                {
                    entrega.Id = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(entrega);
                }
                else
                {
                    negocio.Agregar(entrega);
                }

                Response.Redirect("ListaFormasEntrega.aspx");
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