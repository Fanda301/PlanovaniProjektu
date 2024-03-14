using System;
using System.Collections.Generic;

namespace PlanovaniProjektu.Models;

public partial class TbProjekt
{
    public int Id { get; set; }

    public string? Stav { get; set; }

    public string? Nazev { get; set; }

    public string? Popis { get; set; }

    public DateTime? DatumZacatku { get; set; }

    public DateTime? PlanovaneDokonceni { get; set; }

    public int? Vedouci { get; set; }

    public virtual TbUzivatel? VedouciNavigation { get; set; }
}
