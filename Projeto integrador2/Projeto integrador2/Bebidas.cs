using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_integrador2
{
  public  class Bebidas
    {
        public string Nome {  get; set; }
        public string Tamanho { get; set; }
        public double Valor { get; set; }

        public Bebidas(string nome, string tamanho, double valor) 
        {
            Nome = nome;
            Tamanho = tamanho;
            Valor = valor;
        }
    }
}
