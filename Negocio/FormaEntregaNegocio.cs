using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FormaEntregaNegocio
    {
        public List<FormaEntrega> listar()
        {
            List<FormaEntrega> lista = new List<FormaEntrega>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("SELECT IdFormaEntrega, Nombre, Estado FROM FORMAS_ENTREGA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaEntrega aux = new FormaEntrega();
                    aux.Id = (int)datos.Lector["IdFormaEntrega"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (string)datos.Lector["Estado"];


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
        public int Agregar(FormaEntrega nuevo)
        {

            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO FORMAS_ENTREGA " +
                    "(Nombre, Estado) " +
                    "OUTPUT INSERTED.IdFormaEntrega " +
                    "VALUES (@Nombre, @Estado)"
                );

                datos.agregarParametro("@Nombre", nuevo.Nombre);
                datos.agregarParametro("@Estado", "Preparando Pedido");

                return datos.ejecutarAccionScalar();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(FormaEntrega Entrega)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE FORMAS_ENTREGA SET " +
                    "Nombre = @Nombre " +
                    "WHERE IdFormaEntrega = @Id");

                datos.agregarParametro("@Nombre", Entrega.Nombre);
                datos.agregarParametro("@Id", Entrega.Id);

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
                datos.setearConsulta("DELETE FROM FORMAS_ENTREGA WHERE IdFormaEntrega = @Id");
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

