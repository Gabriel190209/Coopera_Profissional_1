using System;
using System.Collections.Generic;

namespace API_CooperaProfissional.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NomeCompleto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Telefone { get; set; }

    public byte[]? FotoPerfil { get; set; }

    public int Pin { get; set; }

    public int? IdServico { get; set; }

    public int IdFuncao { get; set; }

    public virtual ICollection<Avaliaco> Avaliacos { get; set; } = new List<Avaliaco>();

    public virtual Funcao IdFuncaoNavigation { get; set; } = null!;

    public virtual Servico? IdServicoNavigation { get; set; }

    public virtual ICollection<ImagensPortifolio> ImagensPortifolios { get; set; } = new List<ImagensPortifolio>();
}
