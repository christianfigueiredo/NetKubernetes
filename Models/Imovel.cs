using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetKubernetes.Models
{
    public class Imovel
    {
        [Key]
        public  int Id { get; set; }
        public string? Nome { get; set; }
        public string? Endereco { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public required decimal Preco { get; set; }
        public string? Imagem { get; set; }
        public DateTime DataCriacao { get; set; }

    }
}