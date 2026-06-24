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
                datos.setearConsulta("SELECT P.Id, P.Fecha, P.Total, P.Estado, " +
                                     "U.Id AS P.IdUsuario, U.Nombre AS Nombre, U.Apellido AS Apellido, U.Email AS Email, U.Telefono AS Telefono, " +
                                     "FP.Id AS P.IdFormaPago, FP.Nombre AS FormaPago, " +
                                     "FE.Id AS P.IdFormaEntrega, FE.Nombre AS FormaEntrega, " +
                                     "D.Id AS P.IdDireccion, D.Calle AS Calle, D.Numero AS Numero, D.Localidad AS Localidad, D.CodigoPostal AS CP, D.Observaciones AS Observaciones FROM Pedidos P" +
                                     "INNER JOIN USUARIO U ON P.IdUsuario = U.Id" +
                                     "INNER JOIN FORMASPAGO FP ON P.IdFormaPago = FP.Id" +
                                     "INNER JOIN FORMASENTREGA FE ON P.IdFormaEntrega = FE.Id" +
                                     "INNER JOIN DIRECCIONES D ON P.IdDireccion = D.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Total = (Decimal)datos.Lector["Total"];
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

                    aux.Direccion = new Direccion();
                    aux.Direccion.Id = (int)datos.Lector["IdDireccion"];
                    aux.Direccion.Calle = (string)datos.Lector["Calle"];
                    aux.Direccion.Numero = (string)datos.Lector["Numero"];
                    aux.Direccion.Localidad = (string)datos.Lector["Localidad"];
                    aux.Direccion.CodigoPostal = (string)datos.Lector["CP"];
                    aux.Direccion.Observaciones = (string)datos.Lector["Observaciones"];


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
                    "Estado = @Estado" +
                    "FormaPago = @FormaPago" +
                    "FormaEntrega = @FormaEntrega" +
                    "Direccion = @Direccion" +
                    "WHERE Id = @Id");

                datos.agregarParametro("@Estado", Pedido.Estado);
                datos.agregarParametro("@FormaPago", Pedido.FormaPago);
                datos.agregarParametro("@FormaEntrega", Pedido.FormaEntrega);
                datos.agregarParametro("@Direccion", Pedido.Direccion);

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
                datos.setearConsulta("DELETE FROM PEDIDOS WHERE Id = @Id");
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