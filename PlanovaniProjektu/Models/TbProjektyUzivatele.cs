namespace PlanovaniProjektu.Models
{
    public class TbProjektyUzivatele2
    {
        public int Id { get; set; }

        public int? Uzivatel { get; set; }

        public int? Projekt { get; set; }

        //public virtual TbUzivatel? UzivatelNavigation { get; set; }

        //public virtual TbProjekt? ProjektNavigation { get; set; }
    }
}
