using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using veebMiljukova.Models;

namespace veebMiljukova.Controllers;
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

    // https://localhost:7052/kasutajad
    [HttpGet]
    public List<Kasutaja> Get()
    {
        return _kasutajad;
    }

    // API otspunkt, mis kustutab kõik kasutajad
    [HttpGet("kustuta-koik")]
    public List<Kasutaja> KustutaKoik()
    {
        _kasutajad.Clear();
        return _kasutajad; // Tagastab tühja nimekirja
    }

    // API otspunkt, mis tagastab ühe kasutaja vastavalt järjekorranumbrile 
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

    // API otspunkt, mis lisab kasutaja
    [HttpPost("lisa")]
    public List<Kasutaja> Add([FromBody] Kasutaja kasutaja)
    {
        _kasutajad.Add(kasutaja);
        return _kasutajad;
    }

    // API otspunkt, mis eemaldab kasutaja järjekорранумбри järgi 
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

    // API otspunkt, mis muudab kasutaja andmeid 
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
}
