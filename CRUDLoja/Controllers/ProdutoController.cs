using CRUDLoja.Data;
using CRUDLoja.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDLoja.Controllers
{

    public class ProdutoController : Controller
    {
        
        public IRepository Repository { get; }

        public ProdutoController(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<IActionResult> ProdutoIndex()
        {
            Produto[] produtos = await Repository.GetAllProdutosAsync();
            return View(produtos);
        }

        public async Task<IActionResult> CreateProduto()
        {
            ViewBag.Categorias = await Repository.GetAllCategoriasAsync(false);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduto(Produto produto, int categoriaId)
        {

            int id = categoriaId;
            if (id == 0)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {

                    produto.CategoriaProdutos = new List<CategoriaProduto>
                            {
                                new CategoriaProduto()
                                    {
                                        CategoriaId = id,
                                        ProdutoId = produto.Id,
                                    }
                            };

                    Repository.Add(produto);

                    if (await Repository.SaveChangesAsync())
                    {

                        return RedirectToAction(nameof(ProdutoIndex));
                    }

                    return View();
                }
                else
                {
                    return View(produto);
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> UpdateProduto(int id)
        {

            ViewBag.Categorias = await Repository.GetAllCategoriasAsync(false);

            try
            {
                Produto produto = await Repository.GetProdutoAsync(id);


                if (produto != null)
                {
                    return View(produto);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduto(int id, Produto produto, int categoriaId)
        {
            
            try
            {

                produto.CategoriaProdutos = new List<CategoriaProduto>
                            {
                                new CategoriaProduto()
                                    {
                                        CategoriaId = categoriaId,
                                        ProdutoId = produto.Id,
                                    }
                            };

                if (produto != null)
                {
                    Repository.Update(produto);
                    
                    if (await Repository.SaveChangesAsync())
                    {
                        return RedirectToAction(nameof(ProdutoIndex));
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                Produto produto = await Repository.GetProdutoAsync(id);
                if (produto != null)
                {
                    return View(produto);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduto(int id, Produto produto)
        {
            try
            {
                Produto produtoTemp = await Repository.GetProdutoAsync(id);
                if (produtoTemp != null)
                {
                    Repository.Delete(produto);
                    if (await Repository.SaveChangesAsync())
                    {
                        return RedirectToAction(nameof(ProdutoIndex));
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

    }
}
