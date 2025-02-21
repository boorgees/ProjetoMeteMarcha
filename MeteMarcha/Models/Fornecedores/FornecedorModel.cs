using MeteMarcha.Utils.Entidades;

namespace MeteMarcha.Models.Fornecedores
{
    public class FornecedorModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Contato { get; set; }

        public FornecedorModel()
        {
            
        }

        public FornecedorModel(Fornecedor fornecedor)
        {
            ID = (int)fornecedor.ID;
            Nome = fornecedor.Nome;
            Cnpj = fornecedor.Cnpj;
            Contato = fornecedor.Contato;
        }

        public Fornecedor GetEntidade()
        {
            return new Fornecedor()
            {
                ID = ID,
                Nome = Nome,
                Cnpj = Cnpj,
                Contato = Contato
            };
        }
    }
}