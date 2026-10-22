using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitText.Shared.models
{
    public class RetornoNovaLetra
    {
        public Guid Ticket { get; set; }
        public char LetraSorteada { get; set; }
        public RetornoNovaLetra(Guid ticket, char letraSorteada)
        {
            this.Ticket = ticket;
            this.LetraSorteada = letraSorteada;
        }
    }
}