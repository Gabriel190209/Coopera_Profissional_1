using System;
using System.Collections.Generic;
using System.Text;

namespace MAUI_Coopera.Models
{
    public class UsuarioModel
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

        public ImageSource? Foto => FotoPerfil == null
            ? null
            : ImageSource.FromStream(() => new MemoryStream(FotoPerfil));
    }
}
