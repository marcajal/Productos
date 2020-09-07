using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Productos.Models;
using Productos.ViewModel;

namespace Productos.Repository
{
    public interface IProdRepository
    {
        Task<List<ProdViewModel>> GetProductos();
        Task<ProdViewModel> GetProducto(int id);
        Task<int> AddProducto(ProdViewModel prodVM);
        Task<int> DeleteProducto(int id);
        Task<int> DeleteMarca(int id);
        Task<int> UpdateProducto(ProdViewModel producto);
        Task UpdateMarca(Marca marca);
    }
}
