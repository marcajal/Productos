using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.Models;
using Productos.Repository;
using Productos.ViewModel;

namespace Productos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        IProdRepository prodRepository;

        public ProductosController(IProdRepository _prodRepository)
        {
            prodRepository = _prodRepository;
        }

        //// GET: api/Productos
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var productos = await prodRepository.GetProductos();
                if (productos == null)
                {
                    return NotFound();
                }

                return Ok(productos);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        //// GET: api/Productos/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            
            try
            {
                var producto = await prodRepository.GetProducto(id);

                if (producto == null)
                {
                    return NotFound();
                }

                return Ok(producto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProducto([FromBody] ProdViewModel producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                        var id = await prodRepository.AddProducto(producto);
                    if (id > 0)
                    {
                        return Ok(id);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProducto([FromBody] ProdViewModel producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await prodRepository.UpdateProducto(producto);

                    if (result == -1) return NotFound();
                    else return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteProducto(int id)
        {
            int result = 0;


            try
            {
                result = await prodRepository.DeleteProducto(id); 
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
