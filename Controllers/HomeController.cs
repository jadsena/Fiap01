using Fiap01.Data;
using Fiap01.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap01.Controllers
{
    public class HomeController : Controller
    {
        private PerguntasContext _context;
        public HomeController(PerguntasContext perguntasContext)
        {
            _context = perguntasContext;
        }
        public IActionResult Index()
        {
            //ViewBag.Nome = "Jean";
            //ViewData["NomeDoAluno"] = $"Outro Nome {DateTime.Now}";
            //var pergunta = new Pergunta()
            //{
            //    Id=1,
            //    Descricao="Que pergunta?",
            //    Autor="Jean"
            //};

            return View(_context.Perguntas.ToList());
        }
        public IActionResult Ajuda()
        {
            return View();
        }
        public IActionResult Sobre()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                _context.Perguntas.Add(pergunta);
                await _context.SaveChangesAsync();
                var p = pergunta;
            }
            return View();
        } 
    }
}
