using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetallePedidoNegocio
    {
        public List<DetallePedido> listar()
        {
            List<DetallePedido> lista = new List<DetallePedido>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(@"
                    SELECT 
                    DP.IdDetallePedido,
                    DP.Cantidad, 
                    DP.IdPedido, 
                    PR.IdProducto, 
                    PR.Nombre, 
                    PR.Descripcion, 
                    PR.Precio,
                    PR.UrlImagen
                    FROM DETALLE_PEDIDOS DP 
                    INNER JOIN PEDIDOS PE
                    ON DP.IdPedido = PE.IdPedido 
                    INNER JOIN PRODUCTOS PR 
                    ON DP.IdProducto = PR.IdProducto");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallePedido aux = new DetallePedido();
                    aux.Id = (int)datos.Lector["IdDetallePedido"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];

                    aux.Pedido = new Pedido();
                    aux.Pedido.Id = (int)datos.Lector["IdPedido"];

                    aux.Producto = new Producto();
                    aux.Producto.Id = (int)datos.Lector["IdProducto"];
                    aux.Producto.Nombre = (string)datos.Lector["Nombre"];
                    aux.Producto.Descripcion = datos.Lector["Descripcion"] != DBNull.Value
                        ? datos.Lector["Descripcion"].ToString() : "";
                    aux.Producto.Precio = (decimal)datos.Lector["Precio"];
                    aux.Producto.ImagenUrl = datos.Lector["UrlImagen"].ToString();

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<DetallePedido> listarPorPedido(int idPedido)
        {
            return listar().Where(x => x.Pedido.Id == idPedido).ToList();
        }

        public int Agregar(DetallePedido nuevo)
        {

            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO DETALLE_PEDIDOS " +
                    "(Cantidad, IdPedido, IdProducto) " +
                    "OUTPUT INSERTED.IdDetallePedido " +
                    "VALUES (@Cantidad, @Pedido, @Producto)"
                );

                datos.agregarParametro("@Cantidad", nuevo.Cantidad);
                datos.agregarParametro("@Pedido", nuevo.Pedido.Id);
                datos.agregarParametro("@Producto", nuevo.Producto.Id);

                return datos.ejecutarAccionScalar();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(DetallePedido Detalle)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE DETALLE_PEDIDOS SET " +
                    "Cantidad = @Cantidad, " +
                    "Pedido = @Pedido, " +
                    "Producto = @Producto, " +
                    "WHERE IdDetallePedido = @Id");

                datos.agregarParametro("@Cantidad", Detalle.Cantidad);
                datos.agregarParametro("@Pedido", Detalle.Pedido.Id);
                datos.agregarParametro("@Producto", Detalle.Producto.Id);
                datos.agregarParametro("@Id", Detalle.Id);


                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Eliminar(int id)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("DELETE FROM DETALLE_PEDIDOS WHERE IdDetallePedido = @Id");
                datos.agregarParametro("@Id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}