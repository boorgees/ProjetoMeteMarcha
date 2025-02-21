using MeteMarcha.Utils.Entidades;

namespace MeteMarcha.Models.Pecas
{
    public class PecaModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal? Preco { get; set; }
        
        

        public PecaModel()
        {
        }

        public PecaModel(Peca peca)
        {
            ID = (int)peca.ID;
            Nome = peca.Nome;
            Preco = peca.Preco;
         


        }


        public Peca GetEntidade()
        {
            return new Peca()
            {
                ID = ID,
                Nome = Nome,
                Preco = (decimal)Preco,
             

            };
        }
    }
}