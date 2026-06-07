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
                datos.setearConsulta("SELECT Id, Nombre, Estado FROM FORMAS_PAGO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaPago aux = new FormaPago();
                    aux.Id = (int)datos.Lector["Id"];
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
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@Nombre, @Estado)"
                );

                datos.agregarParametro("@Nombre", nuevo.Nombre);
                datos.agregarParametro("@Estado", nuevo.Estado);

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
                    "Nombre = @Nombre" +
                    "WHERE Id = @Id");

                datos.agregarParametro("@Nombre", Pago.Nombre);

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
                datos.setearConsulta("DELETE FROM FORMAS_PAGO WHERE Id = @Id");
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

