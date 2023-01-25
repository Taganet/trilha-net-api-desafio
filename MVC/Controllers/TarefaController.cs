using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Context;
using MVC.Models;

namespace MVC.Controllers
{
    public class TarefaController : Controller
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        //Exibindo todas as tarefas na raiz /Tarefa
        public IActionResult Index()
        {
            var tarefas = _context.Tarefas.ToList();
            return View(tarefas);
        }

        //Formulario cadastro de novas tarefas
        public IActionResult Criar()
        {
            return View();
        }
        
        //Salvando novas tarefas
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if(ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        //Formulário de edição de tarefas
        public IActionResult Editar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if(tarefa == null)
                return RedirectToAction(nameof(Index));

            return View(tarefa);
        }

        //Salvando edição de tarefa
        [HttpPost]
        public IActionResult Editar(Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);
            if(tarefaBanco == null)
                return NotFound();

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //Exibindo detalhes da tarefa por Id
        public IActionResult Detalhes(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if(tarefa == null)
                return RedirectToAction(nameof(Index));

            return View(tarefa);
        }

        //Formulário de exclusão de tarefas
        public IActionResult Excluir(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if(tarefa == null)
                return RedirectToAction(nameof(Index));

            return View(tarefa);
        }

        //Excluindo tarefa confirmada
        [HttpPost]
        public IActionResult Excluir(Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //Localizar por titulo
        public IActionResult ObterPorTitulo()
        {
            return View();
        }

        //Resultado Localizar por titulo
        [HttpPost]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));

            return View(tarefas);
        }

        //Localizar por titulo
        public IActionResult ObterPorData()
        {
            return View();
        }

        //Resultado Localizar por titulo
        [HttpPost]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefas = _context.Tarefas.Where(x => x.Data.Date == data.Date);

            return View(tarefas);
        }

        //Localizar por titulo
        public IActionResult ObterPorStatus()
        {
            return View();
        }

        //Resultado Localizar por titulo
        [HttpPost]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefas = _context.Tarefas.Where(x => x.Status == status);

            return View(tarefas);
        }
    }
}