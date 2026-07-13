using System;

namespace Projeto_integrador2
{
    // Representa uma linha da tabela "vendas" (o cabeçalho de uma venda finalizada).
    // Usada para preencher a lista de vendas na tela de Controle de Vendas.
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string FormaPagamento { get; set; }
        public double Total { get; set; }
    }
}
