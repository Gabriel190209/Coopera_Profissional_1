using System;
using System.Collections.Generic;
using System.Text;

namespace MAUI_Coopera.Models
{
    public class PerfilProfissionalModel
    {
        public int IdProfissional { get; set; } 
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string Descricao { get; set; }
        public byte[]? FotoPerfil { get; set; }
        public List<byte[]> Portfolio { get; set; } = new();
    }
}
