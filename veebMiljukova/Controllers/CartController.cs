using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebMiljukova.Models;
using System.Threading.Tasks;

namespace veebMiljukova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // Добавление товара в корзину
        [HttpPost("add-to-cart/{userId}/{toodeId}")]
        public async Task<IActionResult> AddToCart(int userId, int toodeId)
        {
            var userCart = await _context.Ostukorvid.Include(o => o.Tooted)
                .FirstOrDefaultAsync(o => o.KasutajaId == userId);

            if (userCart == null)
            {
                userCart = new Cart { KasutajaId = userId };
                _context.Ostukorvid.Add(userCart);
            }

            var product = await _context.Tooted.FindAsync(toodeId);
            if (product == null) return NotFound("Товар не найден");

            // Проверка на дублирование товара в корзине
            if (!userCart.Tooted.Any(p => p.Id == toodeId))
            {
                userCart.Tooted.Add(product);
                await _context.SaveChangesAsync();
                return Ok("Товар добавлен в корзину");
            }
            else
            {
                return BadRequest("Товар уже в корзине");
            }
        }

        // Просмотр товаров в корзине
        [HttpGet("view-cart/{userId}")]
        public async Task<IActionResult> ViewCart(int userId)
        {
            var cart = await _context.Ostukorvid.Include(o => o.Tooted)
                .FirstOrDefaultAsync(o => o.KasutajaId == userId);

            if (cart == null) return NotFound("Корзина не найдена");

            return Ok(cart.Tooted);
        }

        // Удаление товара из корзины
        [HttpDelete("remove-from-cart/{userId}/{toodeId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int toodeId)
        {
            var userCart = await _context.Ostukorvid.Include(o => o.Tooted)
                .FirstOrDefaultAsync(o => o.KasutajaId == userId);

            if (userCart == null) return NotFound("Корзина не найдена");

            var product = userCart.Tooted.FirstOrDefault(p => p.Id == toodeId);
            if (product == null) return NotFound("Товар не найден в корзине");

            userCart.Tooted.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Товар удален из корзины");
        }
    }
}
