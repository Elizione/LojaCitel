using CRUDLoja.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDLoja.Data
{
    public class AppRepository : IRepository
    {
        public AppDB Context { get; }
        public AppRepository(AppDB context)
        {
            Context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            //Context.Add(entity);
            Context.AddRangeAsync(entity);
        }

        public void Add<T, A>(T entity, A other) where T : class
        {
            //Context.Add(entity);
            Context.AddRangeAsync(entity, other);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            Context.UpdateRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0; 
        }

        public async Task<Produto> GetProdutoAsync(int id)
        {

            IQueryable<Produto> query = Context.Produtos
                .Include(c => c.CategoriaProdutos)
                .ThenInclude(c => c.Categoria);

            query = query.Where(x=> x.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Produto[]> GetAllProdutosAsync()
        {

            IQueryable<Produto> query = Context.Produtos
                .Include(c => c.CategoriaProdutos)
                .ThenInclude(c => c.Categoria);

            return await query.ToArrayAsync();
        }

        public async Task<Categoria> GetCategoriaAsync(int id, bool includeProdutos = false)
        {
            IQueryable<Categoria> query = Context.Categorias;

            if (includeProdutos)
            {
                query.Include(c => c.CategoriaProdutos)
                .ThenInclude(c => c.Produto);
            }

            query = query.Where(x => x.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Categoria[]> GetAllCategoriasAsync(bool includeProdutos = false)
        {
            IQueryable<Categoria> query = Context.Categorias;

            if (includeProdutos)
            {
                query.Include(c => c.CategoriaProdutos)
                .ThenInclude(c => c.Produto);
            }

            return await query.ToArrayAsync();
        }
    }
}
