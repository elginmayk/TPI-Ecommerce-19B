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
                datos.setearConsulta("SELECT Id, Nombre, Descripcion, Precio, Stock, Estado, UrlImagen FROM PRODUCTOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.Estado = (bool)datos.Lector["Estado"];
                    aux.ImagenUrl = (string)datos.Lector["UrlImagen"];

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