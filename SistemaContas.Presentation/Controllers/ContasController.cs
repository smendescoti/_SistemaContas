using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SistemaContas.Data.Entities;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentation.Models;
using System.Reflection;

namespace SistemaContas.Presentation.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        //Contas/Cadastro
        public IActionResult Cadastro()
        {
            var model = new ContasCadastroModel();
            model.ItensCategoria = ObterItensCategoria();

            return View(model);
        }

        [HttpPost] //Recebe o SUBMIT POST do formulário
        public IActionResult Cadastro(ContasCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                    var conta = new Conta();
                    conta.IdConta = Guid.NewGuid();
                    conta.Nome = model.Nome;
                    conta.Data = model.Data;
                    conta.Valor = model.Valor;
                    conta.Tipo = model.Tipo;
                    conta.Observacoes = model.Observacoes;
                    conta.IdCategoria = model.IdCategoria.Value;
                    conta.IdUsuario = usuarioModel.IdUsuario;

                    var contaRepository = new ContaRepository();
                    contaRepository.Inserir(conta);

                    TempData["MensagemSucesso"] = "Conta cadastrada com sucesso.";
                    model = new ContasCadastroModel();
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao cadastrar conta: " + e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário, por favor verifique.";
            }

            model.ItensCategoria = ObterItensCategoria();
            return View(model);
        }

        //Contas/Consulta
        public IActionResult Consulta()
        {
            var model = new ContasConsultaModel();

            try
            {
                var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);
                var dataAtual = DateTime.Now;

                model.DataInicio = new DateTime(dataAtual.Year, dataAtual.Month, 1);
                model.DataFim = model.DataInicio.Value.AddMonths(1).AddDays(-1);

                var contaRepository = new ContaRepository();
                model.Contas = contaRepository.ObterTodos(model.DataInicio.Value, model.DataFim.Value, usuarioModel.IdUsuario);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Falha ao consultar contas: " + e.Message;
            }

            return View(model);
        }

        [HttpPost] //Receber o SUBMIT POST do formulário
        public IActionResult Consulta(ContasConsultaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                    var contaRepository = new ContaRepository();
                    model.Contas = contaRepository.ObterTodos(model.DataInicio.Value, model.DataFim.Value, usuarioModel.IdUsuario);
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao consultar contas: " + e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário, por favor verifique.";
            }

            return View(model);
        }

        //Contas/Edicao
        public IActionResult Edicao(Guid id)
        {
            var model = new ContasEdicaoModel();
            model.ItensCategoria = ObterItensCategoria();

            try
            {
                var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                var contaRepository = new ContaRepository();
                var conta = contaRepository.ObterPorId(id);

                if(conta != null && conta.IdUsuario == usuarioModel.IdUsuario)
                {
                    model.IdConta = conta.IdConta;
                    model.Nome = conta.Nome;
                    model.Data = conta.Data;
                    model.Valor = conta.Valor;
                    model.Tipo = conta.Tipo;
                    model.Observacoes = conta.Observacoes;
                    model.IdCategoria = conta.IdCategoria;
                }
                else
                {
                    return RedirectToAction("Consulta");
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao obter conta: " + e.Message;
            }

            return View(model);
        }

        [HttpPost] //Receber o SUBMIT POST da página
        public IActionResult Edicao(ContasEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                    var conta = new Conta();
                    conta.IdConta = model.IdConta;
                    conta.Nome = model.Nome;
                    conta.Data = model.Data;
                    conta.Valor = model.Valor;
                    conta.Observacoes = model.Observacoes;
                    conta.Tipo = model.Tipo;
                    conta.IdCategoria = model.IdCategoria.Value;

                    var contaRepository = new ContaRepository();
                    contaRepository.Atualizar(conta);

                    TempData["MensagemSucesso"] = "Conta atualizada com sucesso";
                    return RedirectToAction("Consulta");
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao atualizar conta: " + e.Message;
                }
            }

            model.ItensCategoria = ObterItensCategoria();
            return View(model);
        }

        //Contas/Exclusao/{id}
        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                var contaRepository = new ContaRepository();
                var conta = contaRepository.ObterPorId(id);

                if(conta != null && conta.IdUsuario == usuarioModel.IdUsuario)
                {
                    contaRepository.Excluir(conta);
                    TempData["MensagemSucesso"] = "Conta excluída com sucesso.";
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao excluir conta: " + e.Message;
            }

            return RedirectToAction("Consulta");
        }

        private List<SelectListItem> ObterItensCategoria()
        {
            var itensCategoria = new List<SelectListItem>();

            try
            {
                var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);
                var categoriaRepository = new CategoriaRepository();
                var categorias = categoriaRepository.ObterTodos(usuarioModel.IdUsuario);

                foreach (var item in categorias)
                {
                    var selectListItem = new SelectListItem();
                    selectListItem.Value = item.IdCategoria.ToString();
                    selectListItem.Text = item.Nome;

                    itensCategoria.Add(selectListItem);
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Falha ao obter categorias: " + e.Message;
            }

            return itensCategoria;
        }
    }
}
