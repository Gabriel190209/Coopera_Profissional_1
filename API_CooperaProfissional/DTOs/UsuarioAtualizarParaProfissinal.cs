namespace API_CooperaProfissional.DTOs
{
    public class UsuarioAtualizarParaProfissinal
    {
        public required string Descrição { get; set; }
        public required string Telefone { get; set; }
        public required string Servico { get; set; }
        public byte[]? Imagem { get; set; }
    }
}
