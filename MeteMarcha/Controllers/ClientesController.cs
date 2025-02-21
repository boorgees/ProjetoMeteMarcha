using Microsoft.AspNetCore.Mvc;
using MeteMarcha.Utils.Entidades;
using MeteMarcha.Models.Clientes;

namespace MeteMarcha.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            var model = new ClientesModel();
            model.Clientes = new List<ClienteModel>();

            var clientes = new Cliente().GetAll();


            model.Clientes = clientes.Select(clienteEntidade => new ClienteModel(clienteEntidade)).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(long id)
        {
            var cliente = new Cliente().Get(id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado!");
            }

            cliente.Delete();             

            return RedirectToAction("Index");
        }



        public IActionResult Record(long? id)
        {
            var model = new ClienteModel();

            if (id.HasValue)
            {
                model = new ClienteModel(new Cliente().Get(id.Value));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Record(ClienteModel model, string type)
        {
            Cliente cliente = model.GetEntidade();
            if (type == "save")
            {
                if (cliente.ID > 0)
                {
                    cliente.Update();
                }
                else
                {
                    cliente.Create();
                }
            }
            else if (type == "delete")
            {
                cliente.Delete();
            }
            else
            {
                return BadRequest("Requisição inválida!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet("api/v1/Clientes")]
        public IActionResult Get()
        {
            var result = new Cliente().GetAll().Select(cliente => new ClienteModel(cliente));

            return Ok(result);
        }

        [HttpPost("api/v1/Cliente")]
        public IActionResult Post([FromBody] ClienteModel cliente)
        {
            var clienteEntidade = cliente.GetEntidade();
            clienteEntidade.Create();

            return Ok("Cliente cadastrado!");
        }

        [HttpPut("api/v1/Cliente/{id}")]
        public IActionResult Put(long id, [FromBody] ClienteModel clienteAtualizado)
        {
            var clienteDB = new Cliente().Get(id);

            //if (clienteDB.Nome != clienteAtualizado.Nome)
            //{
            //    clienteDB.Nome = clienteAtualizado.Nome;
            //}

            clienteDB.Nome = clienteAtualizado.Nome ?? clienteDB.Nome;
            clienteDB.Situacao = clienteAtualizado.Situacao ?? clienteDB.Situacao;


            clienteDB.Update();

            return Ok("Cliente atualizado!");
        }

        [HttpDelete("api/v1/Clientes/{id}")]
        public IActionResult Delete(long id)
        {
            var cliente = new Cliente().Get(id);
            cliente.Delete();

            return Ok("Cliente deletado!");
        }
    }
}