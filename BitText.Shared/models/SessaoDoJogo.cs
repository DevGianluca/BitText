using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitText.Shared.models
{
    public class SessaoDoJogo
    {
        public int Vidas { get; set; }
        public int Pontuacao { get; set; }
        public char LetraCorreta { get; set; }
        public DateTime? Tempo { get; set; }

    }
}