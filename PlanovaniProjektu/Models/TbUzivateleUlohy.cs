using System;
using System.Collections.Generic;

namespace PlanovaniProjektu.Models;

public partial class TbUzivateleUlohy
{
    public int Id { get; set; }

    public int? Uzivatel { get; set; }

    public int? Uloha { get; set; }

    public TimeSpan CasStravenyPraci { get; set; }

    public DateTime DatumDokonceniPrace { get; set; }

    public virtual TbUloha? UlohaNavigation { get; set; }

    public virtual TbUzivatel? UzivatelNavigation { get; set; }
}
