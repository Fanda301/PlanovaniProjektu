namespace PlanovaniProjektu.Models
{
    public static class CheckUser
    {
        private static DbProjektyContext _conn = new DbProjektyContext();
        public static int cislo = 0;
        public static int IdUzivatele(string SessionToken)
        {
            TbAutorizacniTokeny exist = _conn.TbAutorizacniTokenies.Where(x => x.Id == SessionToken).FirstOrDefault();

            if (exist == null)
                return 0;

            if (exist != null)
            {
                TbUzivatel tmp = _conn.TbUzivatels.Where(x => x.PrihlasovaciJmeno == exist.Uzivatel).FirstOrDefault();
                return tmp.Id;
            }
            return 0;
        }
        public static string ChechAutorizationToken(string SessionToken)
        {
            TbAutorizacniTokeny exist = _conn.TbAutorizacniTokenies.Where(x => x.Id == SessionToken).FirstOrDefault();

            if (exist == null)
                return "";

            if (exist.DatumPlatnosti < DateTime.Now)
            {
                TbAutorizacniTokeny toDelete = _conn.TbAutorizacniTokenies.Find(exist.Id);
                _conn.TbAutorizacniTokenies.Remove(toDelete);
                _conn.SaveChanges();
                return null;
            }


            if (exist != null)
            {
                TbUzivatel tmp = _conn.TbUzivatels.Where(x => x.PrihlasovaciJmeno == exist.Uzivatel).FirstOrDefault();
                TbRole role = _conn.TbRoles.Where(x => x.Id == tmp.Role).FirstOrDefault();
                return role.Nazev;
            }
            return "";
        }

        public static double Kvyplaceni(List<TbUloha> ulohy, List<TbUzivateleUlohy> uzivateleUlohy)
        {
            double castka = 0;

            foreach (TbUloha u in ulohy)
            {
                foreach (TbUzivateleUlohy uu in uzivateleUlohy)
                {
                    if (uu.Uloha == u.Id)
                        castka += u.HodinovaSazba * uu.CasStravenyPraci.TotalHours;
                }
            }

            return castka;
        }

        public static TimeSpan CelkemVpraci(List<TbUzivateleUlohy> uzivateleUlohy)
        {
            TimeSpan celkem = new TimeSpan(0, 0, 0);

            foreach (TbUzivateleUlohy uu in uzivateleUlohy)
            {
                celkem += uu.CasStravenyPraci;
            }

            return celkem;
        }
    }

    public struct SeznamPraceProVypis
    {
        public string nazevUlohy { get; set; }
        public TimeSpan odpracovano { get; set; }
        public DateTime datum { get; set; }
    }

    public struct SeznamProjektuProVypis
    {
        public int Id { get; set; }
        public string nazev { get; set; }
        public string vedouci { get; set; }
        public DateTime dokonceni { get; set; }

        public int ulohy { get; set; }
    }
}
