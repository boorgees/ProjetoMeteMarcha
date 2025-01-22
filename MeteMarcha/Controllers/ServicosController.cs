using MeteMarcha.Models.Servico;
using Microsoft.AspNetCore.Mvc;

namespace MeteMarcha.Controllers
{
    public class ServicosController : Controller
    {
        public List<ServicoModel> _Servicos = new List<ServicoModel>()
        {
            new ServicoModel() {ID = 1, Nome = "Troca de Óleo"},
            new ServicoModel() {ID = 2, Nome = "Troca de Pneus"},
            new ServicoModel() {ID = 3, Nome = "Troca de Suspensão"},

        }
                
            ;

        public IActionResult Index()
        {
            var model = new ServicosModel() { Servicos = _Servicos };
            return View(model);
        }
        public IActionResult Record(long id)
        {
            var servicosAtual = _Servicos.FirstOrDefault(servico => servico.ID == id);

            return View(servicosAtual);
        }
    }
}
