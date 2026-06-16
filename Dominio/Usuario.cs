using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum Nivel
    {
        VISITANTE = 0,
        ADMINISTRADOR = 1,
        USUARIO = 2,
        //VENDEDOR = 3
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public int Rol { get; set; }
        public Nivel Nivel { get; set; }

        public Usuario()
        {}
        public Usuario(string usuario, string password) 
        {
            Email = usuario;
            Password = password;
        }
    }
}
