namespace API_CooperaProfissional.DTOs
{
    public class UsuarioAtualizarFotoPerfildto
    {
        public required string email { get; set; }
        public required byte[] foto { get; set; }
    }
}
