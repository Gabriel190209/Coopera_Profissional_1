using System;
using System.Collections.Generic;

namespace API_CooperaProfissional.Models;

public partial class Funcao
{
    public int IdFuncao { get; set; }

    public string? NomeFuncao { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
