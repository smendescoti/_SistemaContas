using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Entities;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentation.Models;

namespace SistemaContas.Presentation.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        //Categorias/Cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] //recebe o SUBMIT POST do formulário
        public IActionResult Cadastro(CategoriasCadastroModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //capturando os dados do usuário autenticado (Cookie de autenticação)
                    var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                    //criando um objeto do tipo Categoria
                    var categoria = new Categoria();
                    categoria.IdCategoria = Guid.NewGuid();
                    categoria.Nome = model.Nome;
                    categoria.IdUsuario = usuarioModel.IdUsuario;

                    //cadastrando categoria no banco de dados
                    var categoriaRepository = new CategoriaRepository();
                    categoriaRepository.Inserir(categoria);

                    TempData["MensagemSucesso"] = "Categoria cadastrada com sucesso.";
                    ModelState.Clear(); //limpar os campos do formulário
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao cadastrar categoria: " + e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do formulário, por favor verifique.";
            }

            return View();
        }

        //Categorias/Consulta
        public IActionResult Consulta()
        {
            var lista = new List<CategoriasConsultaModel>();

            try
            {
                //capturar o usuário autenticado através do Cookie do AspNet
                var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                //consultar, no banco de dados, as categorias cadastradas pelo usuário
                var categoriaRepository = new CategoriaRepository();
                var categorias = categoriaRepository.ObterTodos(usuarioModel.IdUsuario);

                foreach (var item in categorias)
                {
                    var model = new CategoriasConsultaModel();
                    model.IdCategoria = item.IdCategoria;
                    model.Nome = item.Nome;
                    lista.Add(model);
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao consultar categoria: " + e.Message;
            }

            return View(lista);
        }

        //Categorias/Exclusao/id?
        public IActionResult Exclusao(Guid id)
        {
            try
            {
                //capturando o usuário autenticado
                var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                //consultar a categoria no banco de dados, através do ID
                var categoriaRepository = new CategoriaRepository();
                var categoria = categoriaRepository.ObterPorId(id);

                //verificar se a categoria foi encontrada
                if(categoria != null && categoria.IdUsuario == usuarioModel.IdUsuario)
                {
                    //trazer a quantidade de contas da categoria
                    var qtdContas = categoriaRepository.ObterQuantidadeContas(categoria.IdCategoria);

                    //verificar se a categoria não possui contas
                    if(qtdContas == 0)
                    {
                        //excluindo a categoria
                        categoriaRepository.Excluir(categoria);
                        TempData["MensagemSucesso"] = "Categoria excluída com sucesso.";
                    }
                    else
                    {
                        TempData["MensagemAlerta"] = $"Não é possível excluir a categoria, pois ela possui {qtdContas} conta(s) vinculada(s).";
                    }                    
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Falha ao excluir categoria: " + e.Message;
            }

            //redirecionamento
            return RedirectToAction("Consulta");
        }

        //Categoria/Edicao/id?
        public IActionResult Edicao(Guid id)
        {
            var model = new CategoriasEdicaoModel();

            try
            {
                //capturar o usuário autenticado
                var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                //consultar a categoria no banco de dados, através do ID
                var categoriaRepository = new CategoriaRepository();
                var categoria = categoriaRepository.ObterPorId(id);

                //verificar se a categoria foi encontrada e se pertence ao usuário autenticado
                if(categoria != null && categoria.IdUsuario == usuarioModel.IdUsuario)
                {
                    model.IdCategoria = categoria.IdCategoria;
                    model.Nome = categoria.Nome;
                }
                else
                {
                    return RedirectToAction("Consulta");
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao obter a categoria: " + e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(CategoriasEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoria = new Categoria();
                    categoria.IdCategoria = model.IdCategoria;
                    categoria.Nome = model.Nome;

                    var categoriaRepository = new CategoriaRepository();
                    categoriaRepository.Atualizar(categoria);

                    TempData["MensagemSucesso"] = "Categoria atualizada com sucesso.";
                    return RedirectToAction("Consulta");
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao atualizar categoria: " + e.Message;
                }
            }

            return View(model);
        }

    }
}
