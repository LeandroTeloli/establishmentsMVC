using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace estabelecimentoMVC.Models
{
    [Table("estabelecimento")]
    public class Estabelecimento
    {
        [Key]
        public int IdEstabelecimento { get; set; }
        [Required]
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        [Required]
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public virtual Categoria Categoria { get; set; }
        public string Status { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
    }
}
