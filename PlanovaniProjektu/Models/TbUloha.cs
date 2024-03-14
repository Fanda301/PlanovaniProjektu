using System;
using System.Collections.Generic;

namespace PlanovaniProjektu.Models;

public partial class TbUloha
{
    public int Id { get; set; }

    public string Nazev { get; set; } = null!;

    public string? Popis { get; set; }

    public int HodinovaSazba { get; set; }

    public int PredpokladanaDobaTrvani { get; set; }

    public int Projekt { get; set; }
    public bool JeSpracovavana { get; set; }

    public virtual ICollection<TbUzivateleUlohy> TbUzivateleUlohies { get; set; } = new List<TbUzivateleUlohy>();
}
