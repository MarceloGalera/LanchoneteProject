using Lanchonete.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// The DbContext is a key component of EF Core that represents a session with the database and manages the interaction between your application and the database
// Within your DbContext class, you define DbSet properties to represent the entity collections
// Each DbSet property corresponds to a table in your database.

namespace Lanchonete.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        // IdentityDbContext extends DbContext with additional features related to user management and authentication
        // It includes the necessary entity classes to represent users, roles, claims, logins, and other identity-related data
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }

    }
}
 