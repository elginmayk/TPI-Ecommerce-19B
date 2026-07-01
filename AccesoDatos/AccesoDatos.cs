using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class Acceso
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public Acceso()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;
            conexion = new SqlConnection(connectionString);
            comando = new SqlCommand();
            //conexion = new SqlConnection("Server=.\\SQLEXPRESS;Database=TPI_ECOMMERCE;Integrated Security=True;TrustServerCertificate=True");
            //conexion = new SqlConnection("server=.\\SQLEXPRESS; database=TPI_ECOMMERCE; integrated security=true");
            conexion = new SqlConnection("Server=localhost,1433;Database=TPI_ECOMMERCE;User Id=sa;Password=Maycol-123456;TrustServerCertificate=True");
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void agregarParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }

        public int ejecutarAccionScalar()
        {
            comando.Connection = conexion;
            conexion.Open();
            int id = (int)comando.ExecuteScalar();
            return id;
        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
    }
}