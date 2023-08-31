using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    [Table("Lanches")]  // não precisaria colocar aqui pq lá no db context já tem
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        [Required(ErrorMessage = "O nome do lanche deve ser informado")]
        [Display(Name = "Nome do Lanche")]
        public string Nome { get; set; }

        [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
        [Display(Name = "Descrição do Lanche")]
        public string DescricaoCurta { get; set; }

        [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        [Required(ErrorMessage = "A descrição detalhada do lanche deve ser informada")]
        [Display(Name = "Descrição detalhada do Lanche")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99,ErrorMessage ="O preço deve estar entre 1 e 999,99 reais")]
        public decimal Preco { get; set; }

        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Caminho Imagem Normal")]
        public string ImagemUrl { get; set; }

        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Caminho Imagem Miniatura")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        // Propriedade de Navegação - Foreign Key
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        // virtual keyword is used to declare a method, property, or indexer in a base class that can be overridden (redefined) by derived classes
    }
}
