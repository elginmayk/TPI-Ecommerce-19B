using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta("SELECT P.IdProducto,P.Nombre,P.Descripcion,P.Precio,P.Stock,P.Estado,P.UrlImagen,P.IdCategoria,C.Nombre AS CategoriaNombre FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON P.IdCategoria = C.IdCategoria WHERE P.Estado = 1 AND P.Stock > 0 ORDER BY C.Nombre");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id = (int)datos.Lector["IdProducto"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? datos.Lector["Descripcion"].ToString() : "";
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.Estado = (bool)datos.Lector["Estado"];
                    aux.ImagenUrl = datos.Lector["UrlImagen"] != DBNull.Value ? datos.Lector["UrlImagen"].ToString() : "";
                    aux.CategoriaNombre = (string)datos.Lector["CategoriaNombre"];
                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Producto> listarTodos()
        {
            List<Producto> lista = new List<Producto>();
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "SELECT P.IdProducto, P.Nombre, P.Descripcion, P.Precio, P.Stock, " +
                    "P.Estado, P.UrlImagen, P.IdCategoria, C.Nombre AS CategoriaNombre " +
                    "FROM PRODUCTOS P " +
                    "INNER JOIN CATEGORIAS C ON P.IdCategoria = C.IdCategoria " +
                    "ORDER BY C.Nombre"
                );
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["IdProducto"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? datos.Lector["Descripcion"].ToString() : "";
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.Estado = (bool)datos.Lector["Estado"];
                    aux.ImagenUrl = datos.Lector["UrlImagen"] != DBNull.Value ? datos.Lector["UrlImagen"].ToString() : "";
                    aux.CategoriaNombre = (string)datos.Lector["CategoriaNombre"];
                    aux.Categoria = new Categoria { Id = (int)datos.Lector["IdCategoria"] };
                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<IGrouping<string, Producto>>  listarAgrupados()
        {
            var lista = listar();

            return lista
                .Where(p => p.Estado && p.Stock > 0)
                .GroupBy(p => p.CategoriaNombre)
                .ToList();
        }

        public List<IGrouping<string, Producto>> ListarCategoria(string Categoria)
        {
            var lista = listar();

            return lista
                .Where(p => p.Estado && p.Stock > 0 && p.CategoriaNombre == Categoria)
                .GroupBy(p => p.CategoriaNombre)
                .ToList();
        }

        public List<Producto> listarDestacados()
        {
            var lista = listar();

            return lista
                .Where(p => p.Estado && p.Stock > 0)
                .Take(6) // solo 6 productos
                .ToList();
        }





        public int Agregar(Producto nuevo)
        {

            Acceso datos = new Acceso();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO PRODUCTOS " +
                    "(Nombre, Descripcion, Precio, Stock, Estado, UrlImagen, IdCategoria) " +
                    "OUTPUT INSERTED.IdProducto " +
                    "VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Estado, @UrlImagen, @IdCategoria)"
                );

                datos.agregarParametro("@Nombre", nuevo.Nombre);
                datos.agregarParametro("@Descripcion", nuevo.Descripcion);
                datos.agregarParametro("@Precio", nuevo.Precio);
                datos.agregarParametro("@Stock", nuevo.Stock);
                datos.agregarParametro("@Estado", nuevo.Estado);

                datos.agregarParametro("@UrlImagen", nuevo.ImagenUrl);
                datos.agregarParametro("@IdCategoria", nuevo.Categoria.Id);

                return datos.ejecutarAccionScalar();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(Producto Producto)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE PRODUCTOS SET " +
                    "Nombre = @Nombre, " +
                    "Descripcion = @Descripcion, " +
                    "Precio = @Precio, " +
                    "Stock = @Stock, " +
                    "Estado = @Estado, " +
                    "UrlImagen = @UrlImagen, " +
                    "IdCategoria = @IdCategoria " +
                    "WHERE IdProducto = @Id"
                );

                datos.agregarParametro("@Nombre", Producto.Nombre);
                datos.agregarParametro("@Descripcion", Producto.Descripcion);
                datos.agregarParametro("@Precio", Producto.Precio);
                datos.agregarParametro("@Stock", Producto.Stock);
                datos.agregarParametro("@Estado", Producto.Estado);
                datos.agregarParametro("@UrlImagen", Producto.ImagenUrl);
                datos.agregarParametro("@IdCategoria", Producto.Categoria.Id);
                datos.agregarParametro("@Id", Producto.Id);

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
                datos.setearConsulta("DELETE FROM PRODUCTOS WHERE IdProducto = @Id");
                datos.agregarParametro("@Id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Producto obtenerPorId(int id)
        {
            Acceso datos = new Acceso();
            Producto p = null;

            try
            {
                datos.setearConsulta(
                    "SELECT IdProducto, Nombre, Precio, UrlImagen, IdCategoria " +
                    "FROM PRODUCTOS WHERE IdProducto = @Id");
                datos.agregarParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    p = new Producto();
                    p.Id = (int)datos.Lector["IdProducto"];
                    p.Nombre = datos.Lector["Nombre"].ToString();
                    p.Precio = (decimal)datos.Lector["Precio"];
                    p.ImagenUrl = datos.Lector["UrlImagen"] != DBNull.Value ? datos.Lector["UrlImagen"].ToString() : "";
                    p.Categoria = new Categoria { Id = (int)datos.Lector["IdCategoria"] };
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
            return p;
        }
    }
}