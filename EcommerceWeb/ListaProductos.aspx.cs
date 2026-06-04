using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace EcommerceWeb
{
    public partial class ListaProductos : System.Web.UI.Page
    {
        // Esta lista debe ser pública para que el .aspx la vea
        public List<Producto> ListaProducto { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            try
            {
                // Traemos los datos de la DB
                ListaProducto = negocio.listar();

                // Enlazamos los datos al GridView que tenés en el diseño
                dgvArticulos.DataSource = ListaProducto;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                // Si falla, podrías guardar el error en una sesión
                Session.Add("error", ex.ToString());
            }
        }
    }
}