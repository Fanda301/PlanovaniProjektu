using System;
using System.Collections.Generic;

namespace PlanovaniProjektu.Models;

public partial class TbRole
{
    public int Id { get; set; }

    public string Nazev { get; set; } = null!;

    public virtual ICollection<TbUzivatel> TbUzivatels { get; set; } = new List<TbUzivatel>();
}
