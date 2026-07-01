using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ResenaNegocio
    {
        // Trae todas las reseñas de un producto, con nombre del usuario que la dejó.
        public List<Resena> ListarPorProducto(int idProducto)
        {
            List<Resena> lista = new List<Resena>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "SELECT R.IdResena, R.Puntuacion, R.Comentario, R.Fecha, " +
                    "U.IdUsuario, U.Nombre, U.Apellido " +
                    "FROM RESENAS R " +
                    "INNER JOIN USUARIOS U ON R.IdUsuario = U.IdUsuario " +
                    "WHERE R.IdProducto = @IdProducto " +
                    "ORDER BY R.Fecha DESC");
                datos.agregarParametro("@IdProducto", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Resena aux = new Resena();
                    aux.Id = (int)datos.Lector["IdResena"];
                    aux.Puntuacion = (int)datos.Lector["Puntuacion"];
                    aux.Comentario = datos.Lector["Comentario"] != DBNull.Value
                        ? datos.Lector["Comentario"].ToString() : "";
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Busca si ESTE usuario ya reseñó el producto (para precargar el formulario en modo edición).
        public Resena ObtenerPorUsuarioYProducto(int idUsuario, int idProducto)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "SELECT IdResena, Puntuacion, Comentario, Fecha " +
                    "FROM RESENAS WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto");
                datos.agregarParametro("@IdUsuario", idUsuario);
                datos.agregarParametro("@IdProducto", idProducto);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Resena r = new Resena();
                    r.Id = (int)datos.Lector["IdResena"];
                    r.Puntuacion = (int)datos.Lector["Puntuacion"];
                    r.Comentario = datos.Lector["Comentario"] != DBNull.Value
                        ? datos.Lector["Comentario"].ToString() : "";
                    r.Fecha = (DateTime)datos.Lector["Fecha"];
                    return r;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Inserta la reseña, o la actualiza si el usuario ya había reseñado ese producto.
        public void Guardar(Resena resena)
        {
            Resena existente = ObtenerPorUsuarioYProducto(resena.Usuario.Id, resena.Producto.Id);

            if (existente != null)
                Actualizar(existente.Id, resena);
            else
                Agregar(resena);
        }

        private void Agregar(Resena nueva)
        {
            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO RESENAS (IdProducto, IdUsuario, Puntuacion, Comentario, Fecha) " +
                    "VALUES (@IdProducto, @IdUsuario, @Puntuacion, @Comentario, @Fecha)"
                );

                datos.agregarParametro("@IdProducto", nueva.Producto.Id);
                datos.agregarParametro("@IdUsuario", nueva.Usuario.Id);
                datos.agregarParametro("@Puntuacion", nueva.Puntuacion);
                datos.agregarParametro("@Comentario", (object)nueva.Comentario ?? DBNull.Value);
                datos.agregarParametro("@Fecha", DateTime.Now);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        private void Actualizar(int idResena, Resena resena)
        {
            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "UPDATE RESENAS SET " +
                    "Puntuacion = @Puntuacion, " +
                    "Comentario = @Comentario, " +
                    "Fecha = @Fecha " +
                    "WHERE IdResena = @Id"
                );

                datos.agregarParametro("@Puntuacion", resena.Puntuacion);
                datos.agregarParametro("@Comentario", (object)resena.Comentario ?? DBNull.Value);
                datos.agregarParametro("@Fecha", DateTime.Now);
                datos.agregarParametro("@Id", idResena);

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
                datos.setearConsulta("DELETE FROM RESENAS WHERE IdResena = @Id");
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
