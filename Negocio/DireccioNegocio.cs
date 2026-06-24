using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DireccioNegocio
    {
        public List<Direccion> listar()
        {
            List<Direccion> lista = new List<Direccion>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("SELECT D.Id, D.Calle, D.Numero, D.Localidad, D.CodigoPostal, D.Observaciones, " +
                                     "U.Id AS IdUsuario FROM DIRECCIONES D" +
                                     "INNER JOIN USUARIO U ON D.IdUsuario = U.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Direccion aux = new Direccion();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Calle = (string)datos.Lector["Calle"];
                    aux.Numero = (string)datos.Lector["Numero"];
                    aux.Localidad = (string)datos.Lector["Localidad"];
                    aux.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                    aux.Observaciones = (string)datos.Lector["Observaciones"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];


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
        public int Agregar(Direccion nuevo)
        {

            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO DIRECCIONES " +
                    "(Calle, Numero, Localidad, CodigoPostal, Observaciones, IdUsuario) " +
                    "OUTPUT INSERTED.IdDireccion " +
                    "VALUES (@Calle, @Numero, @Localidad, @CP, @Observaciones, @Usuario)"
                );

                datos.agregarParametro("@Calle", nuevo.Calle);
                datos.agregarParametro("@Numero", nuevo.Numero);
                datos.agregarParametro("@Localidad", nuevo.Localidad);
                datos.agregarParametro("@CP", nuevo.CodigoPostal);
                datos.agregarParametro("@Observaciones", nuevo.Observaciones);
                datos.agregarParametro("@Usuario", nuevo.Usuario.Id);

                return datos.ejecutarAccionScalar();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(Direccion Direccion)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE DIRECCIONES SET " +
                    "Calle = @Calle" +
                    "Numero = @Numero" +
                    "Localidad = @Localidad" +
                    "CP = @CP" +
                    "Observaciones = @Observaciones" +
                    "WHERE Id = @Id");

                datos.agregarParametro("@Calle", Direccion.Calle);
                datos.agregarParametro("@Numero", Direccion.Numero);
                datos.agregarParametro("@Localidad", Direccion.Localidad);
                datos.agregarParametro("@CP", Direccion.CodigoPostal);
                datos.agregarParametro("@Observaciones", Direccion.Observaciones);

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
                datos.setearConsulta("DELETE FROM DIRECCIONES WHERE Id = @Id");
                datos.agregarParametro("@Id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Direccion obtenerPorUsuario(int idUsuario)
        {
            Direccion dir = null;
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("SELECT Calle, Numero, Localidad, CodigoPostal FROM DIRECCIONES WHERE IdUsuario = @IdUsuario");

                datos.agregarParametro("@IdUsuario", idUsuario);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    dir = new Direccion();

                    dir.Calle = (string)datos.Lector["Calle"];
                    dir.Numero = (string)datos.Lector["Numero"];
                    dir.Localidad = (string)datos.Lector["Localidad"];
                    dir.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return dir;
        }


    }
}