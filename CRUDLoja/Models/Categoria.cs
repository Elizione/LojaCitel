using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDLoja.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual List<CategoriaProduto> CategoriaProdutos { get; set; }
        
    }
}
