using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        // Esto ayuda a que en los desplegables se vea el nombre y no el código
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
