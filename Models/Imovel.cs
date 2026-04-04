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
        public string? Endereco { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Valor { get; set; }
        public string? Picuture { get; set; }
        public DateTime DatadeCriacao { get; set; }  
        public string? Descricao { get; set; }      
    }
}