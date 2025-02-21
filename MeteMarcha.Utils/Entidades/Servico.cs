using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MeteMarcha.Utils.Entidades
{
    public class Servico : EntidadeBase1<Servico>
    {
        protected override string TableName => "SERVICO";
        protected override List<string> Fields => new List<string>()
{
    "NOME",
    "CLIENTE_ID",
    "PECA_ID",
    "PECA_VALOR"  // Adicionado o campo PecaValor
};



        public string Nome { get; set; }
        public int Cliente_Id { get; set; }
        public int Peca_Id { get; set; }
        public decimal PecaValor { get; set; }

        protected override Servico Fill(MySqlDataReader reader)
        {
            return new Servico
            {
                ID = reader.GetInt64("ID"),
                Nome = reader.GetString("NOME"),
                Cliente_Id = reader.IsDBNull(reader.GetOrdinal("CLIENTE_ID")) ? 0 : reader.GetInt32("CLIENTE_ID"),
                Peca_Id = reader.IsDBNull(reader.GetOrdinal("PECA_ID")) ? 0 : reader.GetInt32("PECA_ID"),
                PecaValor = reader.IsDBNull(reader.GetOrdinal("PECA_VALOR")) ? 0 : reader.GetDecimal("PECA_VALOR")

            };
        }


        protected override void FillParameters(MySqlParameterCollection parameters)
        {
            parameters.Add(new MySqlParameter("@pNOME", Nome ?? string.Empty));  // Certifique-se de passar todos os campos corretamente
            parameters.Add(new MySqlParameter("@pCLIENTE_ID", Cliente_Id));
            parameters.Add(new MySqlParameter("@pPECA_ID", Peca_Id));
            parameters.Add(new MySqlParameter("@pPECA_VALOR", PecaValor));  // Adicionado PecaValor

        }



    }
}
