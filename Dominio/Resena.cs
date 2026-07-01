using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Resena
    {
        public int Id { get; set; }
        public Producto Producto { get; set; }
        public Usuario Usuario { get; set; }
        public int Puntuacion { get; set; } // 1 a 5
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
