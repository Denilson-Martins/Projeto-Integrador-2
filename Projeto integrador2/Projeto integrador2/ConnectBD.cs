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
                if (Conexao == null)
                {
                    Conexao = new MySqlConnection("server = localhost; user = root; password = 123456789; database = cafeteria");
                    Conexao.Open();
                }
            }
            catch (Exception ex)
            {
                Conexao = null;
                Console.WriteLine(ex.ToString());
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
    }
}
