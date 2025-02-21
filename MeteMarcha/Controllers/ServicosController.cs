using MeteMarcha.Models.Servicos;
using MeteMarcha.Utils.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MeteMarcha.Controllers
{
    public class ServicosController : Controller
    {
        public IActionResult Index()
        {
            var model = new ServicosModel
            {
                Servicos = new Servico()
                            .GetAll()
                            .Select(servicoEntidade => new ServicoModel(servicoEntidade)
                            {
                                Cliente = new Cliente().Get(servicoEntidade.Cliente_Id), // Carrega o Cliente
                                Peca = new Peca().Get(servicoEntidade.Peca_Id)          // Carrega a Peça
                            })
                            .ToList()
            };

            return View(model);
        }

        public IActionResult Record(long? id)
        {
            var model = id.HasValue ? new ServicoModel(new Servico().Get(id.Value)) : new ServicoModel();

            // Carregar listas de clientes e peças
            var clientes = new Cliente().GetAll();
            var pecas = new Peca().GetAll();

            // Passando as listas para ViewBag (com verificação para nulo)
            ViewBag.Clientes = clientes ?? new List<Cliente>();
            ViewBag.Pecas = pecas ?? new List<Peca>();

            return View(model);
        }




        [HttpPost]
        public IActionResult ConfirmDelete(long id)
        {
            var servico = new Servico().Get(id);

            if (servico == null)
            {
                return NotFound("Serviço não encontrado!");
            }

            try
            {
                servico.Delete();
            }
            catch
            {
                return BadRequest("Não foi possível deletar o serviço. Verifique se ele não está relacionado a outros registros.");
            }

            return RedirectToAction("Index");
        }

        //public IActionResult Record(long? id)
        //{
        //    var model = id.HasValue ? new ServicoModel(new Servico().Get(id.Value)) : new ServicoModel();
        //    return View(model);
        //}


        [HttpPost]
        public IActionResult Record(ServicoModel model, string type)
        {
            // Verifique se os IDs de Cliente e Peça são válidos
            if (model.Cliente_Id <= 0 || model.Peca_Id <= 0)
            {
                ModelState.AddModelError("", "Selecione um cliente e uma peça válidos.");
                ViewBag.Clientes = new Cliente().GetAll() ?? new List<Cliente>();
                ViewBag.Pecas = new Peca().GetAll() ?? new List<Peca>();
                return View(model); // Retorna para a view com a mensagem de erro
            }

            // Atribuir o valor da peça ao modelo de serviço
            var peca = new Peca().Get(model.Peca_Id);
            model.PecaValor = peca?.Valor ?? 0;  // Certifique-se de que o valor da peça está sendo atribuído

            Servico servico = model.GetEntidade();

            if (type == "save")
            {
                if (servico.ID > 0)
                {
                    servico.Update();
                }
                else
                {
                    servico.Create();
                }
            }
            else if (type == "delete")
            {
                try
                {
                    servico.Delete();
                }
                catch
                {
                    return BadRequest("Não foi possível deletar o serviço. Verifique se ele não está relacionado a outros registros.");
                }
            }
            else
            {
                return BadRequest("Requisição inválida!");
            }

            return RedirectToAction("Index");
        }






        // API Endpoints
        [HttpGet("api/v1/Servicos")]
        public IActionResult Get()
        {
            var result = new Servico().GetAll().Select(servico => new ServicoModel(servico));
            return Ok(result);
        }

        [HttpPost("api/v1/Servicos")]
        public IActionResult Post([FromBody] ServicoModel servico)
        {
            if (servico == null)
                return BadRequest("Dados inválidos!");

            var servicoEntidade = servico.GetEntidade();
            servicoEntidade.Create();

            return CreatedAtAction(nameof(Get), new { id = servicoEntidade.ID }, "Serviço cadastrado com sucesso!");
        }

        [HttpPut("api/v1/Servicos/{id}")]
        public IActionResult Put(long id, [FromBody] ServicoModel servicoAtualizado)
        {
            if (servicoAtualizado == null)
                return BadRequest("Dados inválidos!");

            var servicoDB = new Servico().Get(id);
            if (servicoDB == null)
                return NotFound("Serviço não encontrado!");

            servicoDB.Nome = servicoAtualizado.Nome ?? servicoDB.Nome;
            servicoDB.Cliente_Id = servicoAtualizado.Cliente_Id > 0 ? servicoAtualizado.Cliente_Id : servicoDB.Cliente_Id;
            servicoDB.Peca_Id = servicoAtualizado.Peca_Id > 0 ? servicoAtualizado.Peca_Id : servicoDB.Peca_Id;

            servicoDB.Update();

            return Ok("Serviço atualizado com sucesso!");
        }

        [HttpDelete("api/v1/Servicos/{id}")]
        public IActionResult Delete(long id)
        {
            var servico = new Servico().Get(id);

            if (servico == null)
                return NotFound("Serviço não encontrado!");

            try
            {
                servico.Delete();
            }
            catch
            {
                return BadRequest("Não foi possível deletar o serviço. Verifique se ele não está relacionado a outros registros.");
            }

            return Ok("Serviço deletado com sucesso!");
        }
    }
}
