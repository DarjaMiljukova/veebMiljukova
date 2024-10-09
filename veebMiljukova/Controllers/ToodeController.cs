using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using veebMiljukova.Models;

namespace veebMiljukova.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToodeController : ControllerBase
    {
        private static Toode _toode = new Toode(1, "Koola", 1.5, true);

        // GET: toode
        [HttpGet]
        public Toode GetToode()
        {
            return _toode;
        }

        // GET: toode/muuda-aktiivsust
        [HttpGet("muuda-aktiivsust")]
        public Toode MuudaAktiivsust()
        {
            _toode.IsActive = !_toode.IsActive; 
            return _toode;
        }

        // GET: toode/muuda-nime/{uusNimi}
        [HttpGet("muuda-nime/{uusNimi}")]
        public Toode MuudaNime(string uusNimi)
        {
            _toode.Name = uusNimi; 
            return _toode;
        }

        // GET: toode/muuda-hinda-kordajaga/{kordaja}
        [HttpGet("muuda-hinda-kordajaga/{kordaja}")]
        public Toode MuudaHindaKordajaga(double kordaja)
        {
            _toode.Price *= kordaja; 
            return _toode;
        }
    }
}
