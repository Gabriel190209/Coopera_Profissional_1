using System;
using System.Collections.Generic;

namespace API_CooperaProfissional.Models;

public partial class Avaliaco
{
    public int IdAvaliacao { get; set; }

    public string NomeAvaliador { get; set; } = null!;

    public int IdUsuario { get; set; }

    public int QtEstrelas { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
