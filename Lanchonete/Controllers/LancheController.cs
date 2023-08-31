using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;
using Lanchonete.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _repository;

        public LancheController(ILancheRepository repository)
        {
            _repository = repository;
        }

        public IActionResult List(string categoria)
        {
            //ViewData["Titulo"] = "Todos os Lanches";
            //var lanches = _repository.Lanches;
            //var totalLanches = lanches.Count();
            //return View(lanches);

            /*
            var lanchesListViewModel = new LancheListViewModel();
            lanchesListViewModel.Lanches = _repository.Lanches;
            lanchesListViewModel.CategoriaAtual = "Categoria Atual";
            */

            IEnumerable<Lanche> lanches;
            string categorialAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _repository.Lanches.OrderBy(x => x.LancheId);
                categorialAtual = "Todos os lanches";
            }
            else
            {
                lanches = _repository.Lanches
                    .Where(x => x.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(x => x.Nome);

                categorialAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categorialAtual
            };

            return View(lanchesListViewModel);
        }
    
        public IActionResult Details(int lancheId)
        {
            var lanche = _repository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            return View(lanche);
        }

        // poderia ser IActionResult também
        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _repository.Lanches.OrderBy(x => x.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _repository.Lanches
                    .Where(x => x.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any())
                {
                    categoriaAtual = "Lanches";
                }
                else
                {
                    categoriaAtual = "Nenhum lanche foi encontrado";
                }
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }

    }
}
