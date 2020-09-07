using Microsoft.EntityFrameworkCore;
using Productos.Models;
using Productos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Productos.Repository
{
    public class ProdRepository : IProdRepository
    {
        ProductosContext db;
        public ProdRepository (ProductosContext _db)
        {
            db = _db;
        }

        public async Task<List<ProdViewModel>> GetProductos()
        {
            if (db != null)
            {
                return await (from p in db.Producto
                              from m in db.Marca
                              where p.IdMarca == m.Id
                              select new ProdViewModel
                              {
                                  Id = p.Id,
                                  Nombre = p.Nombre,
                                  Marca = m.Nombre,
                                  Costo = p.Costo,
                                  Precio = p.Precio

                              }).ToListAsync();
            }

            return null;
        }

        private int existeProducto(int id)
        {
            return (from p in db.Producto
                        where p.Id == id
                        select p.Id).FirstOrDefault();
        }

        public async Task<ProdViewModel> GetProducto(int id)
        {
            if (db != null)
            {
                return await (from p in db.Producto
                              join m in db.Marca on p.IdMarca equals m.Id
                              where p.Id == id
                              select new ProdViewModel
                              {
                                  Id = p.Id,
                                  Nombre = p.Nombre,
                                  Marca = m.Nombre,
                                  Costo = p.Costo,
                                  Precio = p.Precio

                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public int GetMarca(string nomnbreMarca)
        {
            if (db != null)
            {
                return (from m in db.Marca
                              where m.Nombre == nomnbreMarca
                        select m.Id).FirstOrDefault();
            }

            return -1;
        }

        public async Task<int> AddProducto(ProdViewModel prodVM)
        {
            if (db != null)
            {
                //SI EXISTE RETORNO EL ID DE LA MARCA EXISTENTE
                var returnIdMarca = GetMarca(prodVM.Marca);
                int randNum;

                if (returnIdMarca == -1 || returnIdMarca == 0)
                {
                    Random rand = new Random();
                    randNum = rand.Next(1, 10000);
                    prodVM.IdMarca = randNum;

                    var marcaNew = new Marca()
                    {
                        Id = prodVM.IdMarca,
                        Nombre = prodVM.Marca.Trim()
                    };

                    returnIdMarca = AddMarca(marcaNew);
                }


                var prod = new Producto()
                {
                    //Id = prodVM.Id,
                    Nombre = prodVM.Nombre,
                    IdMarca = returnIdMarca,
                    Costo = prodVM.Costo,
                    Precio = prodVM.Precio
                };

                await db.Producto.AddAsync(prod);
                await db.SaveChangesAsync();

                return prod.Id;
            }

            return 0;
        }

        public int AddMarca(Marca marca)
        {
            if (db != null)
            {
                db.Marca.Add(marca);
                db.SaveChanges();

                return marca.Id;
            }

            return 0;
        }

        public async Task<int> DeleteProducto(int id)
        {
            int result = 0;

            if (db != null)
            {
                
                var producto = await db.Producto.FirstOrDefaultAsync(x => x.Id == id);

                if (producto != null)
                {
                    
                    db.Producto.Remove(producto);

                    
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<int> DeleteMarca(int id)
        {
            int result = 0;

            if (db != null)
            {

                var marca = await db.Marca.FirstOrDefaultAsync(x => x.Id == id);

                if (marca != null)
                {

                    db.Marca.Remove(marca);


                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<int> UpdateProducto(ProdViewModel prodVM)
        {
            if (db != null)
            {

                var existe = existeProducto(prodVM.Id);

                if (existe == 0) return -1;

                //SI EXISTE RETORNO EL ID DE LA MARCA EXISTENTE
                var returnIdMarca = GetMarca(prodVM.Marca);
                int randNum;

                if (returnIdMarca == -1 || returnIdMarca == 0)
                {
                    Random rand = new Random();
                    randNum = rand.Next(1, 10000);
                    prodVM.IdMarca = randNum;

                    var marcaNew = new Marca()
                    {
                        Id = prodVM.IdMarca,
                        Nombre = prodVM.Marca.Trim()
                    };

                    returnIdMarca = AddMarca(marcaNew);
                }

              

                var prod = new Producto()
                {
                    Id =     prodVM.Id,
                    Nombre = prodVM.Nombre,
                    IdMarca = returnIdMarca,
                    Costo = prodVM.Costo,
                    Precio = prodVM.Precio
                };

                db.Producto.Update(prod);
                await db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task UpdateMarca(Marca marca)
        {
            if (db != null)
            {

                db.Marca.Update(marca);


                await db.SaveChangesAsync();
            }
        }
    }
}
