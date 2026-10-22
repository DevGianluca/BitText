using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitText.Shared.models
{
    public class RespostaJogador
    {
        public Guid Ticket { get; set; }
        public string? InputJogador { get; set; }

    }
}