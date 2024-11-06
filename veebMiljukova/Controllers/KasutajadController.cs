using Microsoft.AspNetCore.Mvc;
using veebMiljukova.Models;
using System.Linq;

namespace veebMiljukova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KasutajadController : ControllerBase
    {
        private static List<Kasutaja> _kasutajad = new List<Kasutaja>
        {
            new Kasutaja(1, "Lisadevil", "1234lisa", "Lisa", "Paulk"),
            new Kasutaja(2, "Madler", "adler@234", "Mark", "Adler"),
            new Kasutaja(3, "Moon", "lillyhk", "Lilly", "Hikkimo")
        };

        private readonly AppDbContext _context;

        public KasutajadController(AppDbContext context)
        {
            _context = context;
        }

        // Оставляем остальные методы без изменений

        [HttpGet]
        public List<Kasutaja> Get()
        {
            return _kasutajad;
        }

        [HttpGet("kustuta-koik")]
        public List<Kasutaja> KustutaKoik()
        {
            _kasutajad.Clear();
            return _kasutajad;
        }

        [HttpGet("kasutaja/{index}")]
        public ActionResult<Kasutaja> GetKasutajaByIndex(int index)
        {
            int adjustedIndex = index - 1;

            if (adjustedIndex < 0 || adjustedIndex >= _kasutajad.Count)
            {
                return NotFound("Kasutaja ei leitud!");
            }
            return _kasutajad[adjustedIndex];
        }

        [HttpPost("lisa")]
        public List<Kasutaja> Add([FromBody] Kasutaja kasutaja)
        {
            _kasutajad.Add(kasutaja);
            return _kasutajad;
        }

        [HttpGet("kustuta/{index}")]
        public List<Kasutaja> Delete(int index)
        {
            int adjustedIndex = index - 1;

            if (adjustedIndex < 0 || adjustedIndex >= _kasutajad.Count)
            {
                return _kasutajad;
            }

            _kasutajad.RemoveAt(adjustedIndex);
            return _kasutajad;
        }

        [HttpPost("muuda/{index}")]
        public ActionResult<List<Kasutaja>> Muuda(int index, [FromBody] Kasutaja uusKasutaja)
        {
            int adjustedIndex = index - 1;

            if (adjustedIndex < 0 || adjustedIndex >= _kasutajad.Count)
            {
                return NotFound("Kasutaja ei leitud!");
            }

            _kasutajad[adjustedIndex] = uusKasutaja;
            return _kasutajad;
        }

        // Обновленный метод для регистрации с использованием базы данных
        [HttpPost("register")]
        public IActionResult Register([FromBody] Kasutaja newUser)
        {
            var existingUser = _context.Kasutajad.FirstOrDefault(u => u.Kasutajanimi == newUser.Kasutajanimi);

            if (existingUser != null)
            {
                return Conflict("Пользователь с таким именем уже существует.");
            }

            _context.Kasutajad.Add(newUser);
            _context.SaveChanges(); // Сохраняем нового пользователя в базе данных
            return Ok("Регистрация прошла успешно");
        }

        // Обновленный метод для авторизации с использованием базы данных
        [HttpPost("login")]
        public IActionResult Login([FromBody] Kasutaja loginUser)
        {
            var user = _context.Kasutajad.FirstOrDefault(u =>
                u.Kasutajanimi == loginUser.Kasutajanimi && u.Parool == loginUser.Parool);

            if (user == null)
            {
                return Unauthorized("Неверное имя пользователя или пароль.");
            }

            return Ok(new { userId = user.Id, Message = "Вход успешен" });
        }
    }
}
