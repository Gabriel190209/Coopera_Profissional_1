using System;
using System.Collections.Generic;

namespace API_CooperaProfissional.Models;

public partial class Servico
{
    public int IdServico { get; set; }

    public string NomeServico { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
