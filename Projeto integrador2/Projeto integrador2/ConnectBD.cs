using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Projeto_integrador2
{
    internal class ConnectBD
    {
        public static MySqlConnection? Conexao { get; private set; }

        public void Conectar()
        {
            //Abrir conexão
            try
            {
                if (Conexao == null || Conexao.State == System.Data.ConnectionState.Closed)
                {
                    Conexao = new MySqlConnection("server = localhost; user = root; password = 123456789; database = cafeteria");
                    Conexao.Open();
                }
            }
            catch (Exception ex)
            {
                Conexao = null;
                Console.WriteLine(ex.ToString());
                throw; // repassa o erro para quem chamou poder avisar o usuário
            }
        }

        public void InsertAdm(string nome, string senha, string email)
        {
            string sql = "INSERT INTO adm (nome, senha, email) VALUES (@nome, @senha, @email)";

            MySqlCommand cmd = new MySqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@senha", senha);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();//Usado para comandos que não retornam resultados(INSERT, UPDATE, DELETE).
        }

        public void InsertProduto(string produto, int quantidade, string tamanho, double preco)
        {
            string sql = "INSERT INTO produtos (produto, quantidade, tamanho, preco) VALUES (@produto, @quantidade, @tamanho, @preco)";

            MySqlCommand cmd = new MySqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("@produto", produto);
            cmd.Parameters.AddWithValue("@quantidade", quantidade);
            cmd.Parameters.AddWithValue("@tamanho", tamanho);
            cmd.Parameters.AddWithValue("@preco", preco);
            cmd.ExecuteNonQuery();//Usado para comandos que não retornam resultados(INSERT, UPDATE, DELETE).
        }

        // ---------- Métodos do Controle de Estoque ----------

        // Busca todos os produtos do banco e devolve como lista de objetos Produto,
        // prontos para virar a fonte de dados (ItemsSource) do DataGrid.
        public List<Produto> ListarProdutos()
        {
            var lista = new List<Produto>();

            string sql = "SELECT id, produto, tamanho, preco, quantidade FROM produtos ORDER BY produto";
            MySqlCommand cmd = new MySqlCommand(sql, Conexao);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Produto
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.IsDBNull(reader.GetOrdinal("produto")) ? "" : reader.GetString("produto"),
                        Tamanho = reader.IsDBNull(reader.GetOrdinal("tamanho")) ? "" : reader.GetString("tamanho"),
                        Preco = reader.IsDBNull(reader.GetOrdinal("preco")) ? 0 : reader.GetDouble("preco"),
                        Quantidade = reader.IsDBNull(reader.GetOrdinal("quantidade")) ? 0 : reader.GetInt32("quantidade")
                    });
                }
            }

            return lista;
        }

        // Atualiza somente a quantidade em estoque de um produto (usado ao editar
        // a célula "Quantidade" direto no DataGrid).
        public void AtualizarQuantidade(int id, int novaQuantidade)
        {
            string sql = "UPDATE produtos SET quantidade = @quantidade WHERE id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("@quantidade", novaQuantidade);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // Atualiza todos os dados de um produto já cadastrado (nome, tamanho, preço e quantidade).
        // Usado pelo botão "Atualizar Produto" da tela de Controle de Estoque.
        public void AtualizarProduto(int id, string produto, int quantidade, string tamanho, double preco)
        {
            string sql = "UPDATE produtos SET produto = @produto, quantidade = @quantidade, " +
                         "tamanho = @tamanho, preco = @preco WHERE id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("@produto", produto);
            cmd.Parameters.AddWithValue("@quantidade", quantidade);
            cmd.Parameters.AddWithValue("@tamanho", tamanho);
            cmd.Parameters.AddWithValue("@preco", preco);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // Remove um produto do estoque.
        public void ExcluirProduto(int id)
        {
            string sql = "DELETE FROM produtos WHERE id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // ---------- Métodos do Controle de Vendas ----------

        // Grava uma venda finalizada: primeiro o cabeçalho (data/hora, forma de pagamento,
        // total) na tabela vendas, depois cada item comprado na tabela venda_itens.
        // Devolve o id gerado para a venda.
        public int InserirVenda(string formaPagamento, double total, List<Bebidas> itens)
        {
            string sqlVenda = "INSERT INTO vendas (data_hora, forma_pagamento, total) " +
                               "VALUES (@dataHora, @formaPagamento, @total)";

            MySqlCommand cmdVenda = new MySqlCommand(sqlVenda, Conexao);
            cmdVenda.Parameters.AddWithValue("@dataHora", DateTime.Now);
            cmdVenda.Parameters.AddWithValue("@formaPagamento", formaPagamento);
            cmdVenda.Parameters.AddWithValue("@total", total);
            cmdVenda.ExecuteNonQuery();

            int vendaId = (int)cmdVenda.LastInsertedId;

            string sqlItem = "INSERT INTO venda_itens (venda_id, produto, tamanho, valor_unitario, quantidade, subtotal) " +
                              "VALUES (@vendaId, @produto, @tamanho, @valor, @quantidade, @subtotal)";

            foreach (var item in itens)
            {
                MySqlCommand cmdItem = new MySqlCommand(sqlItem, Conexao);
                cmdItem.Parameters.AddWithValue("@vendaId", vendaId);
                cmdItem.Parameters.AddWithValue("@produto", item.Nome);
                cmdItem.Parameters.AddWithValue("@tamanho", item.Tamanho);
                cmdItem.Parameters.AddWithValue("@valor", item.Valor);
                cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                cmdItem.Parameters.AddWithValue("@subtotal", item.Subtotal);
                cmdItem.ExecuteNonQuery();
            }

            return vendaId;
        }

        // Busca o cabeçalho de todas as vendas já realizadas (mais recente primeiro),
        // para preencher a lista principal da tela de Controle de Vendas.
        public List<Venda> ListarVendas()
        {
            var lista = new List<Venda>();

            string sql = "SELECT id, data_hora, forma_pagamento, total FROM vendas ORDER BY data_hora DESC";
            MySqlCommand cmd = new MySqlCommand(sql, Conexao);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Venda
                    {
                        Id = reader.GetInt32("id"),
                        DataHora = reader.GetDateTime("data_hora"),
                        FormaPagamento = reader.IsDBNull(reader.GetOrdinal("forma_pagamento")) ? "" : reader.GetString("forma_pagamento"),
                        Total = reader.IsDBNull(reader.GetOrdinal("total")) ? 0 : reader.GetDouble("total")
                    });
                }
            }

            return lista;
        }

        // Busca os itens de uma venda específica (os mesmos dados exibidos na NotaFiscal:
        // nome, tamanho, valor unitário, quantidade e subtotal de cada produto vendido).
        public List<VendaItem> ListarItensDaVenda(int vendaId)
        {
            var lista = new List<VendaItem>();

            string sql = "SELECT id, venda_id, produto, tamanho, valor_unitario, quantidade, subtotal " +
                         "FROM venda_itens WHERE venda_id = @vendaId";

            MySqlCommand cmd = new MySqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("@vendaId", vendaId);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new VendaItem
                    {
                        Id = reader.GetInt32("id"),
                        VendaId = reader.GetInt32("venda_id"),
                        Nome = reader.IsDBNull(reader.GetOrdinal("produto")) ? "" : reader.GetString("produto"),
                        Tamanho = reader.IsDBNull(reader.GetOrdinal("tamanho")) ? "" : reader.GetString("tamanho"),
                        Valor = reader.IsDBNull(reader.GetOrdinal("valor_unitario")) ? 0 : reader.GetDouble("valor_unitario"),
                        Quantidade = reader.IsDBNull(reader.GetOrdinal("quantidade")) ? 0 : reader.GetInt32("quantidade"),
                        Subtotal = reader.IsDBNull(reader.GetOrdinal("subtotal")) ? 0 : reader.GetDouble("subtotal")
                    });
                }
            }

            return lista;
        }
    }
}
