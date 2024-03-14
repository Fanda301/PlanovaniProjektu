using System;
using System.Collections.Generic;

namespace PlanovaniProjektu.Modely;

public partial class TbProjektyUzivatele
{
    public int Id { get; set; }

    public int? Uzivatel { get; set; }

    public int? Projekt { get; set; }
}
