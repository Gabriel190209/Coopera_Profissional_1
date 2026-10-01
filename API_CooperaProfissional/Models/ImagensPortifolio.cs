using System;
using System.Collections.Generic;

namespace API_CooperaProfissional.Models;

public partial class ImagensPortifolio
{
    public int IdImagem { get; set; }

    public byte[]? Imagem { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
