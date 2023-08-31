using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;
using Lanchonete.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    public class CarrinhoCompraController : Controller
    {

        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();

            itens = _carrinhoCompra.CarrinhoCompraItens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
            // o controlador procura pela view com o mesmo nome do método action quando não informamos o nome da view
            // return View("NomeDaView", carrinhoCompraVM);
        }

        [Authorize]
        // poderia ser IActionResult
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra (int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra (int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

    }
}
