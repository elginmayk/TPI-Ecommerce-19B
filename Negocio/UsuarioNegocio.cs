using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, Email, Pass, Telefono, Rol FROM USUARIOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Password = (string)datos.Lector["Pass"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Rol = (int)datos.Lector["Rol"];


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
        public int Agregar(Usuario nuevo)
        {

            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO USUARIOS " +
                    "(Nombre, Apellido, Email, Pass, Telefono, Rol) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@Nombre, @Apellido, @Email, @Pass, @Telefono, @Rol)"
                );

                datos.agregarParametro("@Nombre", nuevo.Nombre);
                datos.agregarParametro("@Apellido", nuevo.Apellido);
                datos.agregarParametro("@Email", nuevo.Email);
                datos.agregarParametro("@Pass", nuevo.Password);
                datos.agregarParametro("@Telefono", nuevo.Telefono);
                datos.agregarParametro("@Rol", nuevo.Rol);

                return datos.ejecutarAccionScalar();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(Usuario Usuario)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE USUARIOS SET " +
                    "Nombre = @Nombre, " +
                    "Apellido = @Apellido, " +
                    "Telefono = @Telefono, " +
                    "WHERE Id = @Id"
                );

                datos.agregarParametro("@Nombre", Usuario.Nombre);
                datos.agregarParametro("@Apellido", Usuario.Apellido);
                datos.agregarParametro("@Telefono", Usuario.Telefono);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Usuario Usuario, string Password)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE USUARIOS SET " +
                    "Password = @Pass" +
                    "WHERE Id = @Id");

                datos.agregarParametro("@Pass", Password);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(Usuario Usuario, int Rol)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE USUARIOS SET " +
                    "Rol = @Rol" +
                    "WHERE Id = @Id");

                datos.agregarParametro("@Rol", Rol);

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
                datos.setearConsulta("DELETE FROM USUARIOS WHERE Id = @Id");
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
