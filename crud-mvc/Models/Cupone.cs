using System;
using System.Collections.Generic;

namespace crud_mvc.Models;

public partial class Cupone
{
    public int IdCupon { get; set; }

    public string CodigoCupon { get; set; } = null!;

    public decimal Credito { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
