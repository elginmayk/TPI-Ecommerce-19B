using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> listar()
        {
            List<Pedido> lista = new List<Pedido>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "SELECT P.IdPedido, P.Fecha, P.Total, P.Estado, " +
                    "U.IdUsuario, U.Nombre, U.Apellido, U.Email, U.Telefono, " +
                    "FP.IdFormaPago, FP.Nombre AS FormaPago, " +
                    "FE.IdFormaEntrega, FE.Nombre AS FormaEntrega, " +
                    "D.IdDireccion, D.Calle, D.Numero, D.Localidad, D.CodigoPostal, D.Observaciones " +
                    "FROM PEDIDOS P " +
                    "INNER JOIN USUARIOS U ON P.IdUsuario = U.IdUsuario " +
                    "INNER JOIN FORMAS_PAGO FP ON P.IdFormaPago = FP.IdFormaPago " +
                    "INNER JOIN FORMAS_ENTREGA FE ON P.IdFormaEntrega = FE.IdFormaEntrega " +
                    "LEFT JOIN DIRECCIONES D ON P.IdDireccion = D.IdDireccion");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)datos.Lector["IdPedido"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Total = (decimal)datos.Lector["Total"];
                    aux.Estado = (string)datos.Lector["Estado"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];
                    aux.Usuario.Telefono = (string)datos.Lector["Telefono"];

                    aux.FormaPago = new FormaPago();
                    aux.FormaPago.Id = (int)datos.Lector["IdFormaPago"];
                    aux.FormaPago.Nombre = (string)datos.Lector["FormaPago"];

                    aux.FormaEntrega = new FormaEntrega();
                    aux.FormaEntrega.Id = (int)datos.Lector["IdFormaEntrega"];
                    aux.FormaEntrega.Nombre = (string)datos.Lector["FormaEntrega"];

                    if (datos.Lector["IdDireccion"] != DBNull.Value)
                    {
                        aux.Direccion = new Direccion();
                        aux.Direccion.Id = (int)datos.Lector["IdDireccion"];
                        aux.Direccion.Calle = (string)datos.Lector["Calle"];
                        aux.Direccion.Numero = (string)datos.Lector["Numero"];
                        aux.Direccion.Localidad = (string)datos.Lector["Localidad"];
                        aux.Direccion.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                        aux.Direccion.Observaciones = datos.Lector["Observaciones"] != DBNull.Value
                            ? datos.Lector["Observaciones"].ToString() : "";
                    }

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

        public int Agregar(Pedido nuevo)
        {

            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO PEDIDOS " +
                    "(Fecha, Total, Estado, IdUsuario, IdFormaPago, IdFormaEntrega, IdDireccion) " +
                    "OUTPUT INSERTED.IdPedido " +
                    "VALUES (@Fecha, @Total, @Estado, @Usuario, @FormaPago, @FormaEntrega, @Direccion)"
                );

                datos.agregarParametro("@Fecha", nuevo.Fecha);
                datos.agregarParametro("@Total", nuevo.Total);
                datos.agregarParametro("@Estado", nuevo.Estado);
                datos.agregarParametro("@Usuario", nuevo.Usuario.Id);
                datos.agregarParametro("@FormaPago", nuevo.FormaPago.Id);
                datos.agregarParametro("@FormaEntrega", nuevo.FormaEntrega.Id);
                datos.agregarParametro("@Direccion", nuevo.Direccion != null ? (object)nuevo.Direccion.Id : DBNull.Value);

                return datos.ejecutarAccionScalar();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(Pedido Pedido)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                            "UPDATE PEDIDOS SET " +
                            "Estado = @Estado, " +
                            "IdFormaPago = @FormaPago, " +
                            "IdFormaEntrega = @FormaEntrega, " +
                            "IdDireccion = @Direccion " +
                            "WHERE IdPedido = @Id");

                datos.agregarParametro("@Estado", Pedido.Estado);
                datos.agregarParametro("@FormaPago", Pedido.FormaPago.Id);
                datos.agregarParametro("@FormaEntrega", Pedido.FormaEntrega.Id);
                datos.agregarParametro("@Direccion", Pedido.Direccion != null ? (object)Pedido.Direccion.Id : DBNull.Value);
                datos.agregarParametro("@Id", Pedido.Id);

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
                datos.setearConsulta("DELETE FROM PEDIDOS WHERE IdPedido = @Id");
                datos.agregarParametro("@Id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void MarcarEntregado(int id)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("UPDATE PEDIDOS SET Estado = @estado WHERE IdPedido = @id");
                datos.agregarParametro("@estado", "Entregado");
                datos.agregarParametro("@id", id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }




}