using MeteMarcha.Utils.Entidades;

public class ServicoModel
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public int Cliente_Id { get; set; }
    public int Peca_Id { get; set; }
    public string ClienteNome { get; set; }  // Novo campo
    public string PecaNome { get; set; }     // Novo campo
    public decimal PecaValor { get; set; }   // Novo campo, valor da peça

    // Propriedades de navegação (opcional, caso use Entity Framework)
    public Cliente Cliente { get; set; }
    public Peca Peca { get; set; }

    public ServicoModel()
    {
    }

    public ServicoModel(Servico servico)
    {
        ID = (int)servico.ID;
        Nome = servico.Nome;
        Cliente_Id = servico.Cliente_Id;
        Peca_Id = servico.Peca_Id;

        // Carregando o Cliente e Peca com seus respectivos valores
        Cliente = new Cliente().Get(Cliente_Id);
        Peca = new Peca().Get(Peca_Id);
        ClienteNome = Cliente?.Nome;  // Carregar nome do cliente
        PecaNome = Peca?.Nome;        // Carregar nome da peça
        PecaValor = Peca?.Valor ?? 0; // Carregar o valor da peça
    }

    public Servico GetEntidade()
    {
        var servico = new Servico
        {
            ID = ID,
            Nome = Nome,
            Cliente_Id = Cliente_Id,
            Peca_Id = Peca_Id,
        };

        // Atribuir o valor da peça ao serviço
        servico.PecaValor = PecaValor;

        return servico;
    }
}
