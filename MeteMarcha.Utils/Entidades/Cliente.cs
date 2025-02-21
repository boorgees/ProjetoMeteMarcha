using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using MeteMarcha.Utils.Database;

namespace MeteMarcha.Utils.Entidades
{
    public class Cliente : EntidadeBase1<Cliente>
    {
        // Nome da tabela no banco de dados
        protected override string TableName => "CLIENTE";

        // Campos da tabela (exceto o ID, que já é herdado)
        protected override List<string> Fields => new List<string>()
        {
            "NOME",
            "SITUACAO"
        };

        // Propriedades específicas da classe Cliente

        public string Nome { get; set; }
        public string Situacao { get; set; }

        // Método para preencher o objeto Cliente a partir de um MySqlDataReader
        protected override Cliente Fill(MySqlDataReader reader)
        {
            return new Cliente
            {
                ID = reader.GetInt64("ID"), // Use GetInt64 para o ID
                Nome = reader.GetString("NOME"),
                Situacao = reader.GetString("SITUACAO")
            };
        }

        // Método para preencher os parâmetros do comando SQL
        protected override void FillParameters(MySqlParameterCollection parameters)
        {
            parameters.Add(new MySqlParameter("pNOME", Nome));
            parameters.Add(new MySqlParameter("pSITUACAO", Situacao));
            // Não adicione o pID aqui, pois ele já é adicionado no método Update da classe base
        }
    }
}