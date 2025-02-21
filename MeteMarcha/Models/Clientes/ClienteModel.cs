
using MeteMarcha.Utils.Entidades;

namespace MeteMarcha.Models.Clientes
{
    public class ClienteModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Situacao { get; set; }
  
        public ClienteModel()
        {

        }

        public ClienteModel(Cliente cliente)
        {
            ID = (int)cliente.ID;
            Nome = cliente.Nome;
            Situacao = cliente.Situacao;
        }

       
        public Cliente GetEntidade()
        {
            return new Cliente()
            {
                ID = ID,
                Nome = Nome,
                Situacao = Situacao,
            };
        }
    }
}
