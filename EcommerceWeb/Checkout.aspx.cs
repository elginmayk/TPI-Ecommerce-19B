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
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx?returnUrl=Checkout.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarProductos();
                CargarFormasPago();
            }

        }

        private void CargarProductos()
        {
            List<int> idsCarrito = Session["carrito"] as List<int>;
            if (idsCarrito == null || idsCarrito.Count == 0)
            {
                Response.Redirect("Carrito.aspx");
                return;
            }

            ProductoNegocio prodNegocio = new ProductoNegocio();
            List<Producto> productos = new List<Producto>();

            foreach (int id in idsCarrito)
            {
                Producto p = prodNegocio.obtenerPorId(id);
                if (p != null)
                    productos.Add(p);
            }

            rptProductos.DataSource = productos;
            rptProductos.DataBind();

            decimal subtotal = productos.Sum(p => p.Precio);
            lblSubtotal.Text = "$ " + subtotal.ToString("N2");
            lblTotal.Text = "$ " + subtotal.ToString("N2");
            Session["subtotal"] = subtotal;
        }

        private void CargarFormasPago()
        {
            FormaPagoNegocio fpNegocio = new FormaPagoNegocio();
            ddlFormaPago.DataSource = fpNegocio.listar();
            ddlFormaPago.DataTextField = "Nombre";
            ddlFormaPago.DataValueField = "Id";
            ddlFormaPago.DataBind();
        }

        protected void rbEntrega_Changed(object sender, EventArgs e)
        {
            if (rbEnvio.Checked)
            {
                pnlDireccion.Visible = true;
                pnlRetiro.Visible = false;
                lblEnvio.Text = "$ 2.000,00";
                decimal subtotal = (decimal)Session["subtotal"];
                lblTotal.Text = "$ " + (subtotal + 2000).ToString("N2");
            }
            else
            {
                pnlDireccion.Visible = false;
                pnlRetiro.Visible = true;
                lblEnvio.Text = "$ 0,00";
                decimal subtotal = (decimal)Session["subtotal"];
                lblTotal.Text = "$ " + subtotal.ToString("N2");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (!rbEnvio.Checked && !rbRetiro.Checked)
            {
                lblError.Text = "Por favor seleccioná una forma de entrega.";
                lblError.Visible = true;
                return;
            }

            if (rbEnvio.Checked && (string.IsNullOrWhiteSpace(txtCalle.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) ||
                string.IsNullOrWhiteSpace(txtLocalidad.Text) ||
                string.IsNullOrWhiteSpace(txtCodigoPostal.Text)))
            {
                lblError.Text = "Por favor completá todos los campos de dirección.";
                lblError.Visible = true;
                return;
            }

            Usuario usuario = (Usuario)Session["usuario"];
            List<int> idsCarrito = Session["carrito"] as List<int>;

            // Guardar dirección si eligió envío
            int? idDireccion = null;
            if (rbEnvio.Checked)
            {
                DireccioNegocio dirNegocio = new DireccioNegocio();
                Direccion direccion = new Direccion();
                direccion.Usuario = usuario;
                direccion.Calle = txtCalle.Text.Trim();
                direccion.Numero = txtNumero.Text.Trim();
                direccion.Localidad = txtLocalidad.Text.Trim();
                direccion.CodigoPostal = txtCodigoPostal.Text.Trim();
                direccion.Observaciones = txtObservaciones.Text.Trim();

                idDireccion = dirNegocio.Agregar(direccion);
            }

            // Crear pedido
            Pedido pedido = new Pedido();
            pedido.Usuario = usuario;
            pedido.Total = (decimal)Session["subtotal"];
            pedido.Estado = "Pendiente";
            pedido.Fecha = DateTime.Now;
            pedido.FormaPago = new FormaPago { Id = int.Parse(ddlFormaPago.SelectedValue) };
            pedido.FormaEntrega = new FormaEntrega { Id = rbEnvio.Checked ? 1 : 2 };
            if (idDireccion.HasValue)
                pedido.Direccion = new Direccion { Id = idDireccion.Value };

            PedidoNegocio pedNegocio = new PedidoNegocio();
            int idPedido = pedNegocio.Agregar(pedido);


            // Agregar detalles y descontar stock
            DetallePedidoNegocio detNegocio = new DetallePedidoNegocio();
            ProductoNegocio prodNegocio = new ProductoNegocio();

            foreach (int idProducto in idsCarrito)
            {
                DetallePedido detalle = new DetallePedido();
                detalle.Pedido = new Pedido { Id = idPedido };
                detalle.Producto = new Producto { Id = idProducto };
                detalle.Cantidad = 1;

                detNegocio.Agregar(detalle);

                // Descontar stock
                prodNegocio.DescontarStock(idProducto);
            }


            Session["carrito"] = null;
            Response.Redirect("Confirmacion.aspx?id=" + idPedido);
        }
        protected void btnUsarMiDireccion_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            DireccioNegocio negocioDir = new DireccioNegocio();
            Direccion dir = negocioDir.obtenerPorUsuario(usuario.Id);

            if (dir != null)
            {
                txtCalle.Text = dir.Calle;
                txtNumero.Text = dir.Numero;
                txtLocalidad.Text = dir.Localidad;
                txtCodigoPostal.Text = dir.CodigoPostal;
                txtObservaciones.Text = dir.Observaciones;

                txtCalle.ReadOnly = true;
                txtNumero.ReadOnly = true;
                txtLocalidad.ReadOnly = true;
                txtCodigoPostal.ReadOnly = true;
                txtObservaciones.ReadOnly = true;
            }
            else
            {
                lblError.Text = "No tenés una dirección guardada en tu perfil.";
                lblError.Visible = true;
            }
        }

        protected void btnOtraDireccion_Click(object sender, EventArgs e)
        {
            txtCalle.Text = "";
            txtNumero.Text = "";
            txtLocalidad.Text = "";
            txtCodigoPostal.Text = "";
            txtObservaciones.Text = "";

            txtCalle.ReadOnly = false;
            txtNumero.ReadOnly = false;
            txtLocalidad.ReadOnly = false;
            txtCodigoPostal.ReadOnly = false;
            txtObservaciones.ReadOnly = false;

            lblError.Visible = false;
        }
    }
}