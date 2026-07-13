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
    }
}
