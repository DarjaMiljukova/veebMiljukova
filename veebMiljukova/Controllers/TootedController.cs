﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using veebMiljukova.Models;

namespace veebMiljukova.Controllers;
[ApiController]
[Route("[controller]")]
public class TootedController : ControllerBase // Peab pärima ControllerBase
{
    private static List<Toode> _tooted = new List<Toode>{
        new Toode(1,"Koola", 1.5, true),
        new Toode(2,"Fanta", 1.0, false),
        new Toode(3,"Sprite", 1.7, true),
        new Toode(4,"Vichy", 2.0, true),
        new Toode(5,"Vitamin well", 2.5, true)
    };

    // https://localhost:7052/tooted
    [HttpGet]
    public List<Toode> Get()
    {
        return _tooted;
    }

    // API otspunkt, mis kustutab korraga kõik tooted
    [HttpGet("kustuta-koik")]
    public List<Toode> KustutaKoik()
    {
        _tooted.Clear();
        return _tooted; // Tagastab tühja nimekirja
    }

    // API otspunkt, mis muudab kõikide toodete aktiivsuse väära peale
    [HttpGet("muuda-koik-inaktiivseks")]
    public List<Toode> MuudaKoikInaktiivseks()
    {
        foreach (var toode in _tooted)
        {
            toode.IsActive = false;
        }
        return _tooted; // Tagastab uuendatud nimekirja
    }

    // API otspunkt, mis tagastab ühe toote vastavalt järjekorranumbrile
    [HttpGet("toode/{index}")]
    public ActionResult<Toode> GetToodeByIndex(int index)
    {
        if (index < 0 || index >= _tooted.Count)
        {
            return NotFound("Toode ei leitud!"); // Kasutab NotFound() meetodit
        }
        return _tooted[index];
    }

    // API otspunkt, mis tagastab kõige suurema hinnaga toote
    [HttpGet("kalleim-toode")]
    public Toode GetKalleimToode()
    {
        Toode kalleimToode = _tooted.OrderByDescending(t => t.Price).FirstOrDefault();
        return kalleimToode;
    }

    // Varasemad otspunktid
    [HttpDelete("kustuta/{index}")]
    public List<Toode> Delete(int index)
    {
        _tooted.RemoveAt(index);
        return _tooted;
    }

    [HttpGet("kustuta2/{index}")]
    public string Delete2(int index)
    {
        _tooted.RemoveAt(index);
        return "Kustutatud!";
    }

    [HttpPost("lisa/{id}/{nimi}/{hind}/{aktiivne}")]
    public List<Toode> Add(int id, string nimi, double hind, bool aktiivne)
    {
        Toode toode = new Toode(id, nimi, hind, aktiivne);
        _tooted.Add(toode);
        return _tooted;
    }

    //[HttpGet("lisa")]
    //public List<Toode> Add2([FromQuery] int id, [FromQuery] string nimi, [FromQuery] double hind, [FromQuery] bool aktiivne)
    //{
    //    Toode toode = new Toode(id, nimi, hind, aktiivne);
    //    _tooted.Add(toode);
    //    return _tooted;
    //}
    //[HttpPost("lisa")]
    //public List<Toode> Add([FromBody] Toode toode)
    //{
    //    _tooted.Add(toode);
    //    return _tooted;
    //}

    [HttpGet("hind-dollaritesse/{kurss}")]
    public List<Toode> Dollaritesse(double kurss)
    {
        for (int i = 0; i < _tooted.Count; i++)
        {
            _tooted[i].Price = _tooted[i].Price * kurss;
        }
        return _tooted;
    }

    [HttpGet("hind-dollaritesse2/{kurss}")]
    public List<Toode> Dollaritesse2(double kurss)
    {
        foreach (var t in _tooted)
        {
            t.Price = t.Price * kurss;
        }
        return _tooted;
    }
}
