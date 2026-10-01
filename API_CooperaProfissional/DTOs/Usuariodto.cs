namespace API_CooperaProfissional.DTOs
{
    public class Usuariodto
    {
        public int ID { get; set; }
        public string? NomeCompleto { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Telefone { get; set; } = null;
        public string? Descricao { get; set; } = null;
        public int? Estrelas { get; set; }
        public string? Servico { get; set; } = null;
        public string? FuncaoUsuario { get; set; }
        public byte[]? FotoPerfil { get; set; }
    }
}
