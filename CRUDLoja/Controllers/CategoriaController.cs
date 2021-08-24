using CRUDLoja.Data;
using CRUDLoja.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDLoja.Controllers
{
    public class CategoriaController : Controller
    {
        public IRepository Repository { get; }

        public CategoriaController(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<IActionResult> CategoriaIndex()
        {
            Categoria[] categorias = await Repository.GetAllCategoriasAsync(false);
            return View(categorias);
        }

        public IActionResult CreateCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Add(categoria);
                    if (await Repository.SaveChangesAsync())
                    {
                        return RedirectToAction(nameof(CategoriaIndex));
                    }

                    return View();
                }
                else
                {
                    return View(categoria);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateCategoria(int id)
        {

            try
            {
                Categoria categoria = await Repository.GetCategoriaAsync(id, false);
                if (categoria != null)
                {
                    return View(categoria);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategoria(int id, Categoria categoria)
        {

            try
            {
                Categoria categoriaTemp = await Repository.GetCategoriaAsync(id, false);
                if (categoriaTemp != null)
                {
                    Repository.Update(categoria);
                    if (await Repository.SaveChangesAsync())
                    {
                        return RedirectToAction(nameof(CategoriaIndex));
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                Categoria categoria = await Repository.GetCategoriaAsync(id, false);
                if(categoria != null)
                {
                    return View(categoria);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategoria(int id, Categoria categoria)
        {
            try
            {
                Categoria categoriaTemp = await Repository.GetCategoriaAsync(id, false);
                if (categoriaTemp != null)
                {
                    Repository.Delete(categoria);
                    if (await Repository.SaveChangesAsync())
                    {
                        return RedirectToAction(nameof(CategoriaIndex));
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
