using System;
using System.Collections.Generic;

namespace PlanovaniProjektu.Modely;

public partial class TbUzivatel
{
    public int Id { get; set; }

    public string? Jmeno { get; set; }

    public string? Prijmeni { get; set; }

    public string? Email { get; set; }

    public string? Telefon { get; set; }

    public string? Mesto { get; set; }

    public string? UliceCp { get; set; }

    public string? Psc { get; set; }

    public string PrihlasovaciJmeno { get; set; } = null!;

    public string Heslo { get; set; } = null!;

    public int? Role { get; set; }

    public virtual TbRole? RoleNavigation { get; set; }

    public virtual ICollection<TbProjekt> TbProjekts { get; set; } = new List<TbProjekt>();

    public virtual ICollection<TbUzivateleUlohy> TbUzivateleUlohies { get; set; } = new List<TbUzivateleUlohy>();
}
