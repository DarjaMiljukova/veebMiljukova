﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace veebMiljukova.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrimitiividController : ControllerBase
    {
        // GET: primitiivid/hello-world
        [HttpGet("hello-world")]
        public string HelloWorld()
        {
            return "Hello world at " + DateTime.Now;
        }

        // GET: primitiivid/hello-variable/mari
        [HttpGet("hello-variable/{nimi}")]
        public string HelloVariable(string nimi)
        {
            return "Hello " + nimi;
        }

        // GET: primitiivid/add/5/6
        [HttpGet("add/{nr1}/{nr2}")]
        public int AddNumbers(int nr1, int nr2)
        {
            return nr1 + nr2;
        }

        // GET: primitiivid/multiply/5/6
        [HttpGet("multiply/{nr1}/{nr2}")]
        public int Multiply(int nr1, int nr2)
        {
            return nr1 * nr2;
        }

        // GET: primitiivid/do-logs/5
        [HttpGet("do-logs/{arv}")]
        public void DoLogs(int arv)
        {
            for (int i = 0; i < arv; i++)
            {
                Console.WriteLine("See on logi nr " + i);
            }
        }

        // GET: primitiivid/random-number/10/20
        [HttpGet("random-number/{min}/{max}")]
        public int GetRandomNumber(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max + 1);
        }

        // GET: primitiivid/vanus/1990
        [HttpGet("vanus/{synniaasta}")]
        public string GetVanus(int synniaasta)
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;

            int vanus = currentYear - synniaasta;

            bool synnipaevOnJubaOlnud = currentMonth > 10 || (currentMonth == 10 && currentDay >= 9); 

            if (!synnipaevOnJubaOlnud)
            {
                vanus--; 
            }

            return "Oled " + vanus + " aastat vana.";
        }
    }
}
