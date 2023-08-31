using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    //[Table("CarrinhoCompraItens")]    -- não precisa colocar aqui pq lá no db context já tem
    public class CarrinhoCompraItem
    {
        public int CarrinhoCompraItemId { get; set; }
        public Lanche Lanche { get; set; }
        public int Quantidade { get; set; }

        [StringLength(200)]
        public string CarrinhoCompraId { get; set; }
    }
}
