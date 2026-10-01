using System;
using System.Collections.Generic;
using System.Text;

namespace MAUI_Coopera.Models
{
    public class UsuarioAtualizarFotoPerfilModel
    {
        public required string email { get; set; }
        public required byte[] foto { get; set; }
    }
}
