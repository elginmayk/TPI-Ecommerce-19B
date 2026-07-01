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
                
     datos.setearConsulta("SELECT P.IdProducto,P.Nombre,P.Descripcion,P.Precio,P.Stock,P.Estado,P.UrlImagen,P.IdCategoria,C.Nombre AS CategoriaNombre, " +
    "ISNULL(R.Promedio,0) AS PromedioResena, ISNULL(R.Cantidad,0) AS CantidadResenas " +
    "FROM PRODUCTOS P " +
    "INNER JOIN CATEGORIAS C ON P.IdCategoria = C.IdCategoria " +
    "LEFT JOIN (SELECT IdProducto, AVG(CAST(Puntuacion AS DECIMAL(3,2))) AS Promedio, COUNT(*) AS Cantidad " +
    "FROM RESENAS GROUP BY IdProducto) R ON R.IdProducto = P.IdProducto " +
    "WHERE P.Estado = 1 ORDER BY C.Nombre");

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
                    aux.PromedioResena = Convert.ToDecimal(datos.Lector["PromedioResena"]);
                    aux.CantidadResenas = Convert.ToInt32(datos.Lector["CantidadResenas"]);
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


        public List<Producto> listarPorIds(List<int> ids)
        {
            List<Producto> lista = new List<Producto>();

            if (ids == null || ids.Count == 0)
                return lista;

            Acceso datos = new Acceso();

            try
            {
                string cadenaIds = string.Join(",", ids.Distinct()); // se separan los ID en un string separados por ,

                datos.setearConsulta($"SELECT P.IdProducto, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.Estado, P.UrlImagen, P.IdCategoria, C.Nombre AS CategoriaNombre FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON P.IdCategoria = C.IdCategoria WHERE P.Estado = 1 AND P.IdProducto IN ({cadenaIds})");

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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
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
                    "SELECT P.IdProducto, P.Nombre, P.Descripcion, P.Precio, P.Stock, P.Estado, P.UrlImagen, P.IdCategoria, C.Nombre AS CategoriaNombre, " +
                    "ISNULL(R.Promedio,0) AS PromedioResena, ISNULL(R.Cantidad,0) AS CantidadResenas " +
                    "FROM PRODUCTOS P " +
                    "INNER JOIN CATEGORIAS C ON P.IdCategoria = C.IdCategoria " +
                    "LEFT JOIN (SELECT IdProducto, AVG(CAST(Puntuacion AS DECIMAL(3,2))) AS Promedio, COUNT(*) AS Cantidad " +
                    "FROM RESENAS GROUP BY IdProducto) R ON R.IdProducto = P.IdProducto " +
                    "WHERE P.IdProducto = @Id");
                datos.agregarParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    p = new Producto();
                    p.Id = (int)datos.Lector["IdProducto"];
                    p.Nombre = datos.Lector["Nombre"].ToString();
                    p.Precio = (decimal)datos.Lector["Precio"];
                    p.ImagenUrl = datos.Lector["UrlImagen"] != DBNull.Value ? datos.Lector["UrlImagen"].ToString() : "";
                    p.CategoriaNombre = (string)datos.Lector["CategoriaNombre"];
                    p.Categoria = new Categoria { Id = (int)datos.Lector["IdCategoria"] };
                    p.PromedioResena = Convert.ToDecimal(datos.Lector["PromedioResena"]);
                    p.CantidadResenas = Convert.ToInt32(datos.Lector["CantidadResenas"]);

                }
            }
            finally
            {
                datos.cerrarConexion();
            }
            return p;
        }

        public List<IGrouping<string, Producto>> buscar(string nombre, int idCategoria)
        {
            if (!string.IsNullOrEmpty(nombre) && idCategoria > 0)
            {
                // busqueda por nombre Y categoria
                CategoriaNegocio catNegocio = new CategoriaNegocio();
                Categoria cat = catNegocio.listar().FirstOrDefault(c => c.Id == idCategoria);
                string nombreCat = cat != null ? cat.Nombre : "";
                var lista = listar();
                return lista
                    .Where(p => p.Estado && p.Stock > 0
                        && p.Nombre.Contains(nombre)
                        && p.CategoriaNombre == nombreCat)
                    .GroupBy(p => p.CategoriaNombre)
                    .ToList();
            }
            else if (idCategoria > 0)
            {
                CategoriaNegocio catNegocio = new CategoriaNegocio();
                Categoria cat = catNegocio.listar().FirstOrDefault(c => c.Id == idCategoria);
                string nombreCat = cat != null ? cat.Nombre : "";
                return ListarCategoria(nombreCat);
            }
            else if (!string.IsNullOrEmpty(nombre))
            {
                var lista = listar();
                return lista
                    .Where(p => p.Estado && p.Stock > 0 && p.Nombre.Contains(nombre))
                    .GroupBy(p => p.CategoriaNombre)
                    .ToList();
            }
            else
            {
                return listarAgrupados();
            }
        }


        public void DescontarStock(int idProducto)
        {
            Acceso datos = new Acceso();

            try
            {
                datos.setearConsulta(
                    "UPDATE PRODUCTOS " +
                    "SET Stock = Stock - 1 " +
                    "WHERE IdProducto = @IdProducto AND Stock > 0");

                datos.agregarParametro("@IdProducto", idProducto);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }
}