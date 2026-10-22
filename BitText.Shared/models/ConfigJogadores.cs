using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitText.Shared.models
{
    public class ConfigJogadores
    {
        public int Vidas { get; set; }
        public int? TemposSegundos { get; set; }
        public ConfigJogadores(int? temposSegundos)
        {
            TemposSegundos = temposSegundos;
        }

        

    }
}