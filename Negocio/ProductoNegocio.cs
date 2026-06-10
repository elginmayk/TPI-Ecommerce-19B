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
                datos.setearConsulta("SELECT P.IdProducto,P.Nombre,P.Descripcion,P.Precio,P.Stock,P.Estado,P.UrlImagen,C.Nombre AS CategoriaNombre FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON P.IdCategoria = C.IdCategoria WHERE Estado = 1 AND Stock > 0 ORDER BY C.Nombre;");
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

        public List<IGrouping<string, Producto>> listarAgrupados()
        {
            var lista = listar();

            return lista
                .Where(p => p.Estado && p.Stock > 0)
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
                    "(Nombre, Descripcion, Precio, Stock, Estado, UrlImagen) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Estado, @UrlImagen)"
                );

                datos.agregarParametro("@Nombre", nuevo.Nombre);
                datos.agregarParametro("@Descripcion", nuevo.Descripcion);
                datos.agregarParametro("@Precio", nuevo.Precio);
                datos.agregarParametro("@Stock", nuevo.Stock);
                datos.agregarParametro("@Estado", nuevo.Estado);

                datos.agregarParametro("@UrlImagen", nuevo.ImagenUrl);

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
                    "UrlImagen = @UrlImagen" +
                    "WHERE Id = @Id"
                );

                datos.agregarParametro("@Nombre", Producto.Nombre);
                datos.agregarParametro("@Descripcion", Producto.Descripcion);
                datos.agregarParametro("@Precio", Producto.Precio);
                datos.agregarParametro("@Stock", Producto.Stock);
                datos.agregarParametro("@Estado", Producto.Estado);
                datos.agregarParametro("@UrlImagen", Producto.ImagenUrl);

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
                datos.setearConsulta("DELETE FROM PRODUCTOS WHERE Id = @Id");
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