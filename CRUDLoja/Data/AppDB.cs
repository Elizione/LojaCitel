using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDLoja.Models;
using Microsoft.EntityFrameworkCore;


namespace CRUDLoja.Data
{
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options): base(options)
        {
            //Database.EnsureCreated();
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CategoriaProduto> CategoriaProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasData(
                    new Produto()
                    {
                        Id = 1,
                        Nome = "Bola",
                        Preco = 20.0
                    },
                    new Produto()
                    {
                        Id = 2,
                        Nome = "Chuteira",
                        Preco = 35.0
                    },
                    new Produto()
                    {
                        Id = 3,
                        Nome = "Biscoito",
                        Preco = 5.0
                    },
                    new Produto()
                    {
                        Id = 4,
                        Nome = "Bermuda",
                        Preco = 13.0
                    }
                );

            modelBuilder.Entity<Categoria>()
            .HasData(
                new Categoria()
                {
                    Id = 1,
                    Nome = "Futebol"
                },
                new Categoria()
                {
                    Id = 2,
                    Nome = "Alimentos"
                },
                new Categoria()
                {
                    Id = 3,
                    Nome = "Roupas"
                }
            );

            modelBuilder.Entity<CategoriaProduto>()
               .HasKey(PC => new { PC.ProdutoId, PC.CategoriaId });

            modelBuilder.Entity<CategoriaProduto>().HasData(
                    new CategoriaProduto()
                    {
                        CategoriaId = 1,
                        ProdutoId = 1
                    },
                    new CategoriaProduto()
                    {
                        CategoriaId = 1,
                        ProdutoId = 2
                    },
                    new CategoriaProduto()
                    {
                        CategoriaId = 2,
                        ProdutoId = 3
                    },
                    new CategoriaProduto()
                    {
                        CategoriaId = 3,
                        ProdutoId = 4
                    }
                );
        }
    }
}
