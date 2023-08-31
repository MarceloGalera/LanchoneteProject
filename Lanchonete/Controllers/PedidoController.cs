using Lanchonete.Models;
using Lanchonete.Repositories;
using Lanchonete.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _repository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository repository, CarrinhoCompra carrinhoCompra)
        {
            _repository = repository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]   // só consegue fazer checkout quem tiver autenticado
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            // Obtém os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = items;

            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("Erro", "Seu carrinho está vazio");
            }

            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            if (ModelState.IsValid)
            {
                //cria o pedido e os detalhes
                _repository.CriarPedido(pedido);

                //define mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //limpa o carrinho do cliente
                _carrinhoCompra.LimparCarrinho();

                //exibe a view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }

            return View(pedido);
        }
    }
}
