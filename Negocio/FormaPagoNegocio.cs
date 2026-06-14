using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FormaPagoNegocio
    {
        public List<FormaPago> listar()
        {
            List<FormaPago> lista = new List<FormaPago>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("SELECT IdFormaPago, Nombre, Estado FROM FORMAS_PAGO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaPago aux = new FormaPago();
                    aux.Id = (int)datos.Lector["IdFormaPago"];
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
        public int Agregar(FormaPago nuevo)
        {

            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO FORMAS_PAGO " +
                    "(Nombre, Estado) " +
                    "OUTPUT INSERTED.IdFormaPago " +
                    "VALUES (@Nombre, @Estado)"
                );

                datos.agregarParametro("@Nombre", nuevo.Nombre);
                datos.agregarParametro("@Estado", "Falta pago");

                return datos.ejecutarAccionScalar();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(FormaPago Pago)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE FORMAS_PAGO SET " +
                    "Nombre = @Nombre " +
                    "WHERE IdFormaPago = @Id");

                datos.agregarParametro("@Nombre", Pago.Nombre);
                datos.agregarParametro("@Id", Pago.Id);

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
                datos.setearConsulta("DELETE FROM FORMAS_PAGO WHERE IdFormaPago = @Id");
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

