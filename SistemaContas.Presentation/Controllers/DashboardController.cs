using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentation.Models;

namespace SistemaContas.Presentation.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Método para abrir a página /Dashboard/Index
        /// </summary>
        public IActionResult Index()
        {
            var model = new DashboardModel();

            try
            {
                var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                var dataAtual = DateTime.Now;
                model.DataInicio = new DateTime(dataAtual.Year, dataAtual.Month, 1);
                model.DataFim = model.DataInicio.Value.AddMonths(1).AddDays(-1);

                var contaRepository = new ContaRepository();
                var contas = contaRepository.ObterTodos(model.DataInicio.Value, model.DataFim.Value, usuarioModel.IdUsuario);

                model.ContasAReceber = contas.Where(c => c.Tipo == 1).ToList();
                model.ContasAPagar = contas.Where(c => c.Tipo == 2).ToList();

                model.TotalAReceber = model.ContasAReceber.Sum(c => c.Valor);
                model.TotalAPagar = model.ContasAPagar.Sum(c => c.Valor);
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao carregar dashboard: " + e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DashboardModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                    var contaRepository = new ContaRepository();
                    var contas = contaRepository.ObterTodos(model.DataInicio.Value, model.DataFim.Value, usuarioModel.IdUsuario);

                    model.ContasAReceber = contas.Where(c => c.Tipo == 1).ToList();
                    model.ContasAPagar = contas.Where(c => c.Tipo == 2).ToList();

                    model.TotalAReceber = model.ContasAReceber.Sum(c => c.Valor);
                    model.TotalAPagar = model.ContasAPagar.Sum(c => c.Valor);
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao carregar dashboard: " + e.Message;
                }
            }

            return View(model);
        }
    }
}
