using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_integrador2
{
  public class Bebidas
    {
        public string Nome { get; set; }
        public string Tamanho { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }

        
        public double Subtotal => Valor * Quantidade;

        public Bebidas(string nome, string tamanho, double valor, int quantidade) 
        {
            Nome = nome;
            Tamanho = tamanho;
            Valor = valor;
            Quantidade = quantidade;    
        }
    }
}
