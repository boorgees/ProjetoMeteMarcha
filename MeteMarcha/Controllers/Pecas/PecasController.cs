using Microsoft.AspNetCore.Mvc;
using MeteMarcha.Models.Pecas;

namespace MeteMarcha.Controllers.Pecas
{
    public class PecasController : Controller
    {
        public List<PecaModel> _Pecas = new List<PecaModel>() {
                new PecaModel() {ID = 1, Nome = "Biela", Preco = "50"},
                new PecaModel() {ID = 2, Nome = "Bucha", Preco = "70"},
                new PecaModel() {ID = 3, Nome = "Amortecedor", Preco = "200"},
            };

        public IActionResult Index()
        {
            var model = new PecasModel() { Pecas = _Pecas };
            return View(model);
        }

        public IActionResult Record(long id)
        {
            var pecaAtual = _Pecas.FirstOrDefault(peca => peca.ID == id);

            return View(pecaAtual);

        }
    }
}
