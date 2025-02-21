using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MeteMarcha.Utils.Database;


namespace MeteMarcha.Utils.Entidades
{
    public class Peca : EntidadeBase1<Peca>
    {
        protected override string TableName => "PECA";
        protected override List<string> Fields => new List<string>()
        {
            "NOME",
            "PRECO",
        };

        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public long Cliente_Id { get; internal set; }
        public long Peca_Id { get; internal set; }
        public int Valor { get; set; }

        protected override Peca Fill(MySqlDataReader reader)
        {
            return new Peca
            {
                ID = reader.GetInt64("ID"), // Use GetInt64 para o ID
                Nome = reader.GetString("NOME"),
                Preco = reader.GetDecimal("PRECO"),
            };
        }

        protected override void FillParameters(MySqlParameterCollection parameters)
        {
            parameters.Add(new MySqlParameter("pNOME", Nome));
            parameters.Add(new MySqlParameter("pPreco", Preco));
        }
    }
}
