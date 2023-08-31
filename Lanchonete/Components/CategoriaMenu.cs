using Lanchonete.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaMenu(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _repository.Categorias.OrderBy(c => c.CategoriaNome);
            return View(categorias);
        }
    }
}
