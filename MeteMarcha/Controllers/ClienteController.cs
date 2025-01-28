using Microsoft.AspNetCore.Mvc;
using MeteMarcha.Models.Clientes;

namespace MeteMarcha.Controllers
{
    public class ClienteController : Controller
    {

        public List<ClienteModel> _Clientes = new List<ClienteModel>() {
                new ClienteModel() { Id = 1, Nome = "João", Situacao = "Ativo"},
                new ClienteModel() { Id = 2, Nome = "Maria", Situacao = "Ativo"},
                new ClienteModel() { Id = 3, Nome = "Ana", Situacao = "Ativo"},
                new ClienteModel() { Id = 4, Nome = "Bruno", Situacao = "Ativo"},
                new ClienteModel() { Id = 5, Nome = "Gabriel", Situacao = "Ativo"},
                new ClienteModel() { Id = 6, Nome = "Pedrinho", Situacao = "Ativo"},
            };
        public IActionResult Index()
        {
            var model = new ClientesModel()
            {
                Clientes = _Clientes,
            };
            return View(model);
        }


        public IActionResult Record(long id)
        {
            var clienteAtual = _Clientes.FirstOrDefault(cliente => cliente.Id == id);

            return View(clienteAtual);
        }

        public IActionResult Delete(long id)
        {
            var cliente = _Clientes.FirstOrDefault(cliente => cliente.Id == id);

            if (cliente == null)
            {
                // Caso o cliente não seja encontrado, redireciona para a lista de clientes
                return RedirectToAction("Index");
            }

            // Passa o cliente para a View de confirmação
            return View(cliente);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(long id)
        {
            var cliente = _Clientes.FirstOrDefault(cliente => cliente.Id == id);

            if (cliente != null)
            {
                _Clientes.Remove(cliente); // Se for uma lista em memória
             
            }

            return RedirectToAction("Index"); // Redireciona após a exclusão
        }
    }
}
