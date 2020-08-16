using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace estabelecimentoMVC.Models
{
    [Table("municipio")]
    public class Municipio
    {
        [Key]
        public int IdMunicipio { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public int IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }

    }
}
