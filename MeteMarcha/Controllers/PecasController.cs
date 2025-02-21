   using Microsoft.AspNetCore.Mvc;
using MeteMarcha.Models.Pecas;
using MeteMarcha.Models.Clientes;
using MeteMarcha.Utils.Entidades;

namespace MeteMarcha.Controllers
{
    public class PecasController : Controller
    {
        public IActionResult Index()
        {
            var model = new PecasModel();
            model.Pecas = new List<PecaModel>();

            var pecas = new Peca().GetAll();


            model.Pecas = pecas.Select(pecaEntidade => new PecaModel(pecaEntidade)).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete1(long id)
        {
            var peca = new Peca().Get(id);

            if (peca == null)
            {
                return NotFound("Cliente não encontrado!");
            }

            peca.Delete();

            return RedirectToAction("Index");
        }




        public IActionResult Record(long? id)
        {
            var model = new PecaModel();

            if (id.HasValue)
            {
                model = new PecaModel(new Peca().Get(id.Value));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Record(PecaModel model, string type)
        {
            Peca peca = model.GetEntidade();
            if (type == "save")
            {
                if (peca.ID > 0)
                {
                    peca.Update();
                }
                else
                {
                    peca.Create();
                }
            }
            else if (type == "delete")
            {
                peca.Delete();
            }
            else
            {
                return BadRequest("Requisição inválida!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet("api/v1/Pecas")]
        public IActionResult Get()
        {
            var result = new Peca().GetAll().Select(peca => new PecaModel(peca));

            return Ok(result);
        }

        [HttpPost("api/v1/Peca")]
        public IActionResult Post([FromBody] PecaModel peca)
        {
            var pecaEntidade = peca.GetEntidade();
            pecaEntidade.Create();

            return Ok("Peça cadastrada!");
        }

        [HttpPut("api/v1/Peca/{id}")]
        public IActionResult Put(long id, [FromBody] PecaModel pecaAtualizado)
        {
            var pecaDB = new Peca().Get(id);

            //if (clienteDB.Nome != clienteAtualizado.Nome)
            //{
            //    clienteDB.Nome = clienteAtualizado.Nome;
            //}

            pecaDB.Nome = pecaAtualizado.Nome ?? pecaDB.Nome;
            pecaDB.Preco = pecaAtualizado.Preco ?? pecaDB.Preco;


            pecaDB.Update();

            return Ok("Cliente atualizado!");
        }

        [HttpDelete("api/v1/Pecas/{id}")]
        public IActionResult Delete(long id)
        {
            var peca = new Peca().Get(id);
            peca.Delete();

            return Ok("Cliente deletado!");
        }
    }
}
