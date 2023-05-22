using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiTienda.Models;

namespace RestApiTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly TiendaRopaContext _dbcontext;

        public ProductoController(TiendaRopaContext _context)
        {
            _dbcontext = _context;
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Producto newProducto)
        {
            try
            {
                _dbcontext.Productos.Add(newProducto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto newProducto)
        {
            Producto producto = _dbcontext.Productos.Find(newProducto.Id);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                producto.Talla = newProducto.Talla is null ? producto.Talla : newProducto.Talla;
                producto.Color = newProducto.Color is null ? producto.Color : newProducto.Color;
                producto.Precio = newProducto.Precio is null ? producto.Precio : newProducto.Precio;
                producto.Descripcion = newProducto.Descripcion is null ? producto.Descripcion : newProducto.Descripcion;

                _dbcontext.Productos.Update(producto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto)
        {
            Producto producto = _dbcontext.Productos.Find(idProducto);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                _dbcontext.Productos.Remove(producto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("Obtener")]
        public IActionResult Obtener()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                productos = _dbcontext.Productos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = productos });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = productos });
            }
        }

        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public IActionResult ObtenerConId(int idProducto)
        {
            Producto producto = _dbcontext.Productos.Find(idProducto);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                producto = _dbcontext.Productos.Where(prod => prod.Id == idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = producto });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = producto });
            }
        }               
    }   
}
