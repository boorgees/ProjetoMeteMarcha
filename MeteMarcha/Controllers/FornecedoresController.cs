using Microsoft.AspNetCore.Mvc;
using MeteMarcha.Utils.Entidades;
using MeteMarcha.Models.Fornecedores;


namespace MeteMarcha.Controllers
{
    public class FornecedoresController : Controller
    {
        public IActionResult Index()
        {
            var model = new FornecedoresModel();
            model.Fornecedores = new List<FornecedorModel>();

            var fornecedores = new Fornecedor().GetAll();

            model.Fornecedores = fornecedores.Select(fornecedorEntidade => new FornecedorModel(fornecedorEntidade)).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(long id)
        {
            try
            {
                // Buscar o fornecedor
                var fornecedor = new Fornecedor().Get(id);

                // Verificar se o fornecedor existe
                if (fornecedor == null)
                {
                    return NotFound("Fornecedor não encontrado!");
                }

                // Tentar deletar o fornecedor
                fornecedor.Delete();

                // Se a exclusão for bem-sucedida, redireciona para a lista de fornecedores
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Se ocorrer algum erro, pode ser um erro devido a peças relacionadas
                TempData["ErrorMessage"] = "Impossível deletar porque ainda há peças relacionadas!";

                // Redireciona para a página de fornecedores com a mensagem de erro
                return RedirectToAction("Index");
            }
        }


        public IActionResult Record(long? id)
        {
            var model = new FornecedorModel();

            if (id.HasValue)
            {
                model = new FornecedorModel(new Fornecedor().Get(id.Value));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Record(FornecedorModel model, string type)
        {
            Fornecedor fornecedor = model.GetEntidade();
            if (type == "save")
            {
                if (fornecedor.ID > 0)
                {
                    fornecedor.Update();
                }
                else
                {
                    fornecedor.Create();
                }
            }
            else if (type == "delete")
            {
                fornecedor.Delete();
            }
            else
            {
                return BadRequest("Requisição inválida!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet("api/v1/Fornecedores")]
        public IActionResult Get()
        {
            var result = new Fornecedor().GetAll().Select(fornecedor => new FornecedorModel(fornecedor));

            return Ok(result);
        }

        [HttpPost("api/v1/Fornecedor")]
        public IActionResult Post([FromBody] FornecedorModel fornecedor)
        {
            var fornecedorEntidade = fornecedor.GetEntidade();
            fornecedorEntidade.Create();

            return Ok("Fornecedor cadastrado!");
        }

        [HttpPut("api/v1/Fornecedor/{id}")]
        public IActionResult Put(long id, [FromBody] FornecedorModel fornecedorAtualizado)
        {
            var fornecedorDB = new Fornecedor().Get(id);

            fornecedorDB.Nome = fornecedorAtualizado.Nome ?? fornecedorDB.Nome;
            fornecedorDB.Cnpj = fornecedorAtualizado.Cnpj ?? fornecedorDB.Cnpj;
            fornecedorDB.Contato = fornecedorAtualizado.Contato ?? fornecedorDB.Contato;

            fornecedorDB.Update();

            return Ok("Fornecedor atualizado!");
        }

        [HttpDelete("api/v1/Fornecedores/{id}")]
        public IActionResult Delete(long id)
        {
            var fornecedor = new Fornecedor().Get(id);
            fornecedor.Delete();

            return Ok("Fornecedor deletado!");
        }
    }
}