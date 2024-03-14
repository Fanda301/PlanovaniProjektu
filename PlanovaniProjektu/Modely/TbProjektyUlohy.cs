using System;
using System.Collections.Generic;

namespace PlanovaniProjektu.Modely;

public partial class TbProjektyUlohy
{
    public int Id { get; set; }

    public int? Projekt { get; set; }

    public int? Uloha { get; set; }

    public virtual TbProjekt? ProjektNavigation { get; set; }

    public virtual TbUloha? UlohaNavigation { get; set; }
}
