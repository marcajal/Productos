using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productos.ViewModel
{
    public class ProdViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public int IdMarca { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
    }
}
