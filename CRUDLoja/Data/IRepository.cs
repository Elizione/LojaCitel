using CRUDLoja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDLoja.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Add<T, A>(T entity, A other) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Produto> GetProdutoAsync(int id);
        Task<Produto[]> GetAllProdutosAsync();

        Task<Categoria> GetCategoriaAsync(int id, bool includeProdutos);
        Task<Categoria[]> GetAllCategoriasAsync(bool includeProdutos);
    }
}
