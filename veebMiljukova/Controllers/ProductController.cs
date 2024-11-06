using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebMiljukova.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace veebMiljukova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // Метод для добавления нового товара
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] Toode newProduct)
        {
            if (newProduct == null || string.IsNullOrEmpty(newProduct.Name) || newProduct.Price <= 0)
            {
                return BadRequest("Неправильные данные товара.");
            }

            _context.Tooted.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok("Товар успешно добавлен.");
        }

        // Метод для получения списка всех товаров
        [HttpGet("get-products")]
        public async Task<ActionResult<IEnumerable<Toode>>> GetProducts()
        {
            var products = await _context.Tooted.ToListAsync();
            return Ok(products);
        }
    }
}
