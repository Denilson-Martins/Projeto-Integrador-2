using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Projeto_integrador2
{
    internal class ConnectBD
    {

        public string StrConex;
        MySqlConnection con = new MySqlConnection();

        public ConnectBD()

        {

            StrConex = "Server=LocalHost;Database=Cafeteria;Uid=root;P2d=";

        }

        public void Conectar()
        {

        }

        public void Insert(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader reader = cmd.ExecuteReader();
        }

    }
}
