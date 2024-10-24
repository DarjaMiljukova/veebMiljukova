using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebMiljukova.Models;

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

        [HttpPost("add-to-cart/{userId}/{toodeId}")]
        public async Task<IActionResult> AddToCart(int userId, int toodeId)
        {
            var userCart = await _context.Ostukorvid.Include(o => o.Tooted)
                .FirstOrDefaultAsync(o => o.KasutajaId == userId);

            if (userCart == null)
            {
                userCart = new Ostukorv { KasutajaId = userId };
                _context.Ostukorvid.Add(userCart);
            }

            var product = await _context.Tooted.FindAsync(toodeId);
            if (product == null) return NotFound("Товар не найден");

            userCart.Tooted.Add(product);
            await _context.SaveChangesAsync();

            return Ok(userCart);
        }

        [HttpGet("view-cart/{userId}")]
        public async Task<IActionResult> ViewCart(int userId)
        {
            var cart = await _context.Ostukorvid.Include(o => o.Tooted)
                .FirstOrDefaultAsync(o => o.KasutajaId == userId);

            if (cart == null) return NotFound("Корзина не найдена");

            return Ok(cart.Tooted);
        }
    }
}
