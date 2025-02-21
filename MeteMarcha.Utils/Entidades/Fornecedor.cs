using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using MeteMarcha.Utils.Database;

namespace MeteMarcha.Utils.Entidades
{
    public class Fornecedor : EntidadeBase1<Fornecedor>
    {
        // Nome da tabela no banco de dados
        protected override string TableName => "FORNECEDOR";

        // Campos da tabela (exceto o ID, que já é herdado)
        protected override List<string> Fields => new List<string>()
        {
            "NOME",
            "CNPJ",
            "CONTATO"
        };

        // Propriedades específicas da classe Fornecedor
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Contato { get; set; }

        // Método para preencher o objeto Fornecedor a partir de um MySqlDataReader
        protected override Fornecedor Fill(MySqlDataReader reader)
        {
            return new Fornecedor
            {
                ID = reader.GetInt64("ID"), // Use GetInt64 para o ID
                Nome = reader.GetString("NOME"),
                Cnpj = reader.GetString("CNPJ"),
                Contato = reader.GetString("CONTATO")
            };
        }

        // Método para preencher os parâmetros do comando SQL
        protected override void FillParameters(MySqlParameterCollection parameters)
        {
            parameters.Add(new MySqlParameter("pNOME", Nome));
            parameters.Add(new MySqlParameter("pCNPJ", Cnpj));
            parameters.Add(new MySqlParameter("pCONTATO", Contato));
            // Não adicione o pID aqui, pois ele já é adicionado no método Update da classe base
        }
    }
}