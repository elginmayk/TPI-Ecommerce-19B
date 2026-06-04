using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public Producto Producto { get; set; } // Relación con Producto
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
