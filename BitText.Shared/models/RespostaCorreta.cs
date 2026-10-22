using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitText.Shared.models
{
    public class RespostaCorreta
    {
        public bool Acertou { get; set; }
        public string? LetraBinarioCorreta { get; set; }
        public int VidasRestantes { get; set; }
        public int PontuacaoAtual { get; set; }
        public bool GameOver { get; set; }
        
        public char? ProximaLetra { get; set; }

    }
}