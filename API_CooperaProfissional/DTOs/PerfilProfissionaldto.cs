namespace API_CooperaProfissional.DTOs
{
    public class PerfilProfissionaldto
    {
        public int IdProfissional {  get; set; } // FIZ ISSO PARA PODER PASSAR COM Preferences.Set no MAUI para a tela de Perfil Profissional para poder pegar a avaliação na AvaliacaoController (usuario/{idUsuario})
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string Descricao { get; set; }
        public byte[]? FotoPerfil { get; set; }
        public List<byte[]> Portfolio { get; set; } = new();
    }
}
