using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_integrador2
{
    // Representa uma linha da tabela "produtos" do banco.
    // Usada para preencher o DataGrid da tela de Controle de Estoque.
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tamanho { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
