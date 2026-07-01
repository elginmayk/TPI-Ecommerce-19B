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
    public partial class ListaPedidos : System.Web.UI.Page
    {
        PedidoNegocio negocio = new PedidoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cargarPedidos();
            }



        }


        private void cargarPedidos()
        {
            List<Pedido> lista = negocio.listar();
            gvPedidos.DataSource = lista;
            gvPedidos.DataBind();
        }


        protected void gvPedidos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Entregar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                negocio.MarcarEntregado(id); 

                cargarPedidos(); // refresca la grilla
            }



        }
    } }