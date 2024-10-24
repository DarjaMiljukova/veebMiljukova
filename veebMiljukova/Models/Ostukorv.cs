using System.Collections.Generic;

namespace veebMiljukova.Models
{
    public class Ostukorv
    {
        public int Id { get; set; }
        public int KasutajaId { get; set; }
        public List<Toode> Tooted { get; set; } = new List<Toode>();
    }
}
