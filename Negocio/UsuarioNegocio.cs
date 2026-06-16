using AccesoDatos;
using Dominio;
using System;
using System.Collections;
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
                datos.setearConsulta("SELECT IdUsuario, Nombre, Apellido, Email, Pass, Telefono, Rol FROM USUARIOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["IdUsuario"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Password = (string)datos.Lector["Pass"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Rol = Convert.ToInt32(datos.Lector["Rol"]);
                    aux.Nivel = (Nivel)aux.Rol;

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

        public bool Login(Usuario usuario)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("SELECT IdUsuario, Email, Pass, Rol, Nombre FROM USUARIOS WHERE Email = @usuario AND Pass = @password");
                datos.agregarParametro("@usuario", usuario.Email);
                datos.agregarParametro("@password", usuario.Password);

                datos.ejecutarLectura();
                
                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["IdUsuario"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Rol = Convert.ToInt32(datos.Lector["Rol"]);
                    usuario.Nivel = (Nivel)usuario.Rol;
                    return true;
                }

                return false;
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
                    "OUTPUT INSERTED.IdUsuario " +
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
                    "Telefono = @Telefono " +
                    "WHERE IdUsuario = @Id"
                );

                datos.agregarParametro("@Nombre", Usuario.Nombre);
                datos.agregarParametro("@Apellido", Usuario.Apellido);
                datos.agregarParametro("@Telefono", Usuario.Telefono);
                datos.agregarParametro("@Id", Usuario.Id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ModificarPassword(string Email, string Password)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE USUARIOS SET " +
                    "Pass = @Pass" +
                    "WHERE Email = @Email");

                datos.agregarParametro("@Pass", Password);
                datos.agregarParametro("@Email", Email);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ModificarRol(int id, int Rol)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE USUARIOS SET " +
                    "Rol = @Rol " +
                    "WHERE IdUsuario = @Id");

                datos.agregarParametro("@Rol", Rol);
                datos.agregarParametro("@Id", id);

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
                datos.setearConsulta("DELETE FROM USUARIOS WHERE IdUsuario = @Id");
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
