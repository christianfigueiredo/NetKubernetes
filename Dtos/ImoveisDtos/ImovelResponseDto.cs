namespace NetKubernetes.Dtos.ImoveisDtos
{
    public class ImovelResponseDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Endereco { get; set; }
        public string? Picuture { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DatadeCriacao { get; set; }
        //public Guid UsuarioId { get; set; }        
    }
}