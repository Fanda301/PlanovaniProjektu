using System;
using System.Collections.Generic;

namespace PlanovaniProjektu.Modely;

public partial class TbAutorizacniTokeny
{
    public string Id { get; set; } = null!;

    public string? Uzivatel { get; set; }

    public DateTime DatumVystaveni { get; set; }

    public DateTime DatumPlatnosti { get; set; }
}
