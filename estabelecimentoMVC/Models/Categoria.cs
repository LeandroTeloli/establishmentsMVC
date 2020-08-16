using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace estabelecimentoMVC.Models
{
    [Table("categoria")]
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Estabelecimento> Estabelecimentos { get; set; }
    }
}
