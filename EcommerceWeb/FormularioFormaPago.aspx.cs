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
    public partial class FormularioFormaPago : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    litTitulo.Text = "Editar Forma de Pago";
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarFormaPago(id);
                }
                else
                {
                    litTitulo.Text = "Nueva Forma de Pago";
                }
            }
        }

        private void CargarFormaPago(int id)
        {
            FormaPagoNegocio negocio = new FormaPagoNegocio();
            FormaPago pago = negocio.listar().Find(f => f.Id == id);

            if (pago != null)
            {
                txtNombre.Text = pago.Nombre;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                FormaPago pago = new FormaPago();
                pago.Nombre = txtNombre.Text.Trim();

                FormaPagoNegocio negocio = new FormaPagoNegocio();

                if (Request.QueryString["id"] != null)
                {
                    pago.Id = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(pago);
                }
                else
                {
                    negocio.Agregar(pago);
                }

                Response.Redirect("ListaFormasPago.aspx");
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