namespace NetKubernetes.Dtos.UsuarioDtos
{
    public class UsuarioRegistroRequestDto
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Senha { get; set; }
    }
}