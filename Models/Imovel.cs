using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetKubernetes.Models
{
    public class Imovel
    {
        [Key]
        [Required]        
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cidade { get; set; }        
        public string? Endereco { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Preco { get; set; }
        public string? Tipo { get; set; }
        public string? ImagemUrl { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}