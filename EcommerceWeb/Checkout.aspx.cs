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

            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> productos = new List<Producto>();

            foreach (int id in idsCarrito)
            {
                Producto p = negocio.obtenerPorId(id);
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
            FormaPagoNegocio negocio = new FormaPagoNegocio();
            ddlFormaPago.DataSource = negocio.listar();
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
                Direccion direccion = new Direccion();
                direccion.Usuario = usuario;
                direccion.Calle = txtCalle.Text.Trim();
                direccion.Numero = txtNumero.Text.Trim();
                direccion.Localidad = txtLocalidad.Text.Trim();
                direccion.CodigoPostal = txtCodigoPostal.Text.Trim();
                direccion.Observaciones = txtObservaciones.Text.Trim();

                AccesoDatos.Acceso datos = new AccesoDatos.Acceso();
                datos.setearConsulta(
                    "INSERT INTO DIRECCIONES (Calle, Numero, Localidad, CodigoPostal, Observaciones, IdUsuario) " +
                    "OUTPUT INSERTED.IdDireccion " +
                    "VALUES (@Calle, @Numero, @Localidad, @CP, @Observaciones, @Usuario)");
                datos.agregarParametro("@Calle", direccion.Calle);
                datos.agregarParametro("@Numero", direccion.Numero);
                datos.agregarParametro("@Localidad", direccion.Localidad);
                datos.agregarParametro("@CP", direccion.CodigoPostal);
                datos.agregarParametro("@Observaciones", direccion.Observaciones);
                datos.agregarParametro("@Usuario", usuario.Id);
                idDireccion = datos.ejecutarAccionScalar();
                datos.cerrarConexion();
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

            PedidoNegocio negocio = new PedidoNegocio();
            int idPedido = negocio.Agregar(pedido);

            // Agregar detalles
            DetallePedidoNegocio detNegocio = new DetallePedidoNegocio();
            foreach (int idProducto in idsCarrito)
            {
                DetallePedido detalle = new DetallePedido();
                detalle.Pedido = new Pedido { Id = idPedido };
                detalle.Producto = new Producto { Id = idProducto };
                detalle.Cantidad = 1;
                detNegocio.Agregar(detalle);
            }

            Session["carrito"] = null;
            Response.Redirect("Confirmacion.aspx?id=" + idPedido);
        }
    }
}