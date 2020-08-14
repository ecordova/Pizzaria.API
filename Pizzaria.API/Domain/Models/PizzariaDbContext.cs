using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Pizzaria.API.Domain.Models
{
    public class PizzariaDbContext : DbContext
    {
        private IConfiguration _configuration;
        private string connectionString;

        public PizzariaDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PizzariaDbContext(DbContextOptions<PizzariaDbContext> options)
            : base(options)
        {
        }

        public PizzariaDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public virtual DbSet<Clientes> ClienteSet { get; set; }
        public virtual DbSet<Pedidos> PedidoSet { get; set; }
        public virtual DbSet<PedidosItens> PedidosItenSet { get; set; }
        public virtual DbSet<Produtos> ProdutoSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string sConn = connectionString;
                optionsBuilder.UseSqlServer(sConn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.ClienteId)
                    .HasName("PK__Clientes__71ABD0A7B69B03DD");

                entity.HasIndex(e => new { e.Celular, e.ClienteId })
                    .HasName("IDX_Clientes_Celular");

                entity.HasIndex(e => new { e.Nome, e.ClienteId })
                    .HasName("IDX_Clientes_Nome");

                entity.HasIndex(e => new { e.Telefone, e.ClienteId })
                    .HasName("IDX_Clientes_Telefone");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Bairro)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cidade)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Complemento)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Telefone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Uf)
                    .HasColumnName("UF")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Pedidos>(entity =>
            {
                entity.HasKey(e => e.PedidoId)
                    .HasName("PK__Pedidos__09BA1410E4CD8903");

                entity.HasIndex(e => e.DataEmissao)
                    .HasName("IDX_Pedidos_DataEmissao");

                entity.Property(e => e.PedidoId).HasColumnName("PedidoID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.DataEmissao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NomeCliente)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ValorFrete).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<PedidosItens>(entity =>
            {
                entity.HasKey(e => e.PedidoItemId)
                    .HasName("PK__PedidosI__4A8A539382B4EF2D");

                entity.HasIndex(e => new { e.PedidoId, e.PedidoItemId })
                    .HasName("IDX_PedidosItens_PedidoID");

                entity.Property(e => e.PedidoItemId).HasColumnName("PedidoItemID");

                entity.Property(e => e.PedidoId).HasColumnName("PedidoID");

                entity.Property(e => e.ProdutoId).HasColumnName("ProdutoID");

                entity.Property(e => e.ValorItem).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Produtos>(entity =>
            {
                entity.HasKey(e => e.ProdutoId)
                    .HasName("PK__Produtos__9C8800C34076E7FC");

                entity.HasIndex(e => new { e.Nome, e.ProdutoId })
                    .HasName("IDX_Produtos_Nome");

                entity.Property(e => e.ProdutoId).HasColumnName("ProdutoID");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            });
        }
    }
}
