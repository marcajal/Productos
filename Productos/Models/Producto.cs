using System;
using System.Collections.Generic;

namespace Productos.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdMarca { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
    }
}
