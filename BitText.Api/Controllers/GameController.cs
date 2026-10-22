using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.Extensions.ObjectPool;
using System.Runtime.CompilerServices;
using BitText.Shared.models;

namespace BitText.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private Random rnd = new Random();


        private char SortearLetra()
        {
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
            return letra;
        }

        // Memoria na chache onde ira salvar as informções importante
        // para jogar
        public GameController(IMemoryCache cache)
        {
            _cache = cache;
        }



        [HttpPost("iniciar")]
        public IActionResult iniciar([FromBody] ConfigJogadores config)
        {
            Guid idUnico = Guid.NewGuid();
            char letra = SortearLetra();


            SessaoDoJogo sessao = new SessaoDoJogo();
            sessao.Vidas = config.Vidas;
            sessao.LetraCorreta = letra;
            if (config.TemposSegundos.HasValue) sessao.Tempo = DateTime.Now.AddSeconds(config.TemposSegundos.Value);
            else sessao.Tempo = null;
            sessao.Pontuacao = 0;
            _cache.Set(idUnico, sessao, TimeSpan.FromMinutes(5));
            var resposta = new RetornoNovaLetra(idUnico, letra);
            return Ok(resposta);
        }
        [HttpPost("validar")]
        public IActionResult validaçãoResposta([FromBody] RespostaJogador jogador)
        {

            if (_cache.TryGetValue(jogador.Ticket, out SessaoDoJogo? sessao))
            {
                RespostaCorreta correta = new RespostaCorreta();
                if (sessao.Tempo.HasValue && DateTime.Now > sessao.Tempo.Value)
                {
                    correta.GameOver = true;
                    correta.VidasRestantes = sessao.Vidas;
                    correta.PontuacaoAtual = sessao.Pontuacao;
                    _cache.Remove(jogador.Ticket);
                    return Ok(correta);
                }
                else
                {
                    string binarioReal = Convert.ToString(sessao.LetraCorreta, 2).PadLeft(8, '0');

                    if (binarioReal.Equals(jogador.InputJogador))
                    {
                        correta.Acertou = true;
                        sessao.Pontuacao++;

                    }
                    else
                    {
                        correta.Acertou = false;
                        correta.LetraBinarioCorreta = binarioReal;
                        sessao.Vidas = Math.Max(0, sessao.Vidas - 1);

                        if (sessao.Vidas == 0)
                        {
                            _cache.Remove(jogador.Ticket);
                            correta.GameOver = true;
                        }
                    }
                    if (correta.GameOver == false)
                    {
                        char novaLetra = SortearLetra();
                        correta.ProximaLetra = novaLetra;
                        sessao.LetraCorreta = novaLetra;
                        _cache.Set(jogador.Ticket, sessao, TimeSpan.FromMinutes(5));
                    }
                    correta.VidasRestantes = sessao.Vidas;
                    correta.PontuacaoAtual = sessao.Pontuacao;

                    return Ok(correta);
                }
            }

            else
            {
                return BadRequest("Ticket inválido");
            }
        }
    }
}