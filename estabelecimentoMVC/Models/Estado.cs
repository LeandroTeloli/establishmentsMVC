using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace estabelecimentoMVC.Models
{
    [Table("estado")]
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }
        public int CodigoUf { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public int Regiao { get; set; }
        public virtual ICollection<Municipio> Municipios { get; set; }

    }
}
