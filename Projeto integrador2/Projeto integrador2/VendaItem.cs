namespace Projeto_integrador2
{
    // Representa uma linha da tabela "venda_itens" (um item dentro de uma venda).
    // As propriedades têm os mesmos nomes usados em Bebidas/NotaFiscal (Nome, Tamanho,
    // Valor, Quantidade, Subtotal) para que o DataGrid da tela de Controle de Vendas
    // mostre exatamente os mesmos dados que a tela NotaFiscal mostrou na hora da compra.
    public class VendaItem
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public string Nome { get; set; }
        public string Tamanho { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public double Subtotal { get; set; }
    }
}
