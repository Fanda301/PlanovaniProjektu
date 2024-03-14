namespace PlanovaniProjektu.Models
{
    public class TbAutorizacniTokeny
    {
        public string? Id { get; set; }

        public string? Uzivatel { get; set; }

        public DateTime? DatumVystaveni { get; set; }
        public DateTime? DatumPlatnosti { get; set; }
    }
}
