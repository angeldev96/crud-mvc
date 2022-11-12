using System;
using System.Collections.Generic;

namespace crud_mvc.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public virtual ICollection<Cupone> Cupones { get; } = new List<Cupone>();
}
