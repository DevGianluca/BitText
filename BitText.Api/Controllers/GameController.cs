using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace BitText.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private Random rnd = new Random();



        public GameController(IMemoryCache cache)
        {
            _cache = cache;
        }



        [HttpGet("nova-letra")]
        public IActionResult TrocarLetra()
        {
            Guid idUnico = Guid.NewGuid();
            char letra;
            int qualLetra = rnd.Next(0, 2);

            if (qualLetra == 0)
            {
                letra = (char)rnd.Next(65, 91);
            }

            else
            {
                letra = (char)rnd.Next(97, 123);
            }
            _cache.Set(idUnico, letra, TimeSpan.FromMinutes(5));
            var resposta = new RetornoNovaLetra(idUnico,letra);

            return Ok(resposta);
        }


    }
    public class RetornoNovaLetra
    {
        public Guid ticket { get; set;}
        public char letraSorteada { get; set;}
        public RetornoNovaLetra(Guid ticket , char letraSorteada)
        {
            this.ticket = ticket;
            this.letraSorteada = letraSorteada;
        }
    }
}