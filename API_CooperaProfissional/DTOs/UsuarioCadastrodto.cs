namespace API_CooperaProfissional.DTOs
{
    public class UsuarioCadastrodto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public string? Descricao { get; set; }
        public string? Telefone { get; set; }
        public string? Servico { get; set; }
        public required int Pin {  get; set; }
        public byte[]? FotoPerfil { get; set; }
        public string? Funcao { get; set; }
        public byte[]? ImagemPortifolio { get; set; }
    }
}
