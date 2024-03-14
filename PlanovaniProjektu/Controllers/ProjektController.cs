using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanovaniProjektu.Models;

namespace PlanovaniProjektu.Controllers
{
    public class ProjektController : Controller
    {
        DbProjektyContext _conn  = new DbProjektyContext();
        DbProjektyContext _conn2 = new DbProjektyContext();
        // GET: ProjektController
        public ActionResult Index()
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Autorizovano = b;

            return View();
        }

        public ActionResult NovyProjekt()
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                
            }
            else if(b == "Uzivatel")
            {
                ViewBag.AlertMessage = "Nemate dostatecna opravneni k provedeni teto akce !";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Autorizovano = b;

            ViewBag.Vedouci = _conn.TbUzivatels.Where(x => x.Role == 1 || x.Role == 2).ToList();

            return View();
        }
        public ActionResult ZalozitNovy(TbProjekt projekt)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (b == "Uzivatel")
            {
                // NEDOSTATECNA OPRAVNENI
            }

            ViewBag.Autorizovano = b;

            projekt.Vedouci = CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"));
            _conn.TbProjekts.Add(projekt);
            _conn.SaveChanges();

            return RedirectToAction("SeznamProjektu", "Projekt");
        }


        public ActionResult SeznamProjektu(string typ, string vlastnik)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));

            List<TbProjekt> projekts = new List<TbProjekt>();
            //_conn.TbUzivatels.Where(x => x.Id == CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"))).ToList()

            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (b != "Manazer" && vlastnik == "kdokoliv")
            {
                // NEDOSTATECNA OPRAVNENI
            }

            ViewBag.Autorizovano = b;

            switch (typ)
            {
                case "aktualni":
                    projekts = _conn.TbProjekts.Where(x => x.PlanovaneDokonceni >= DateTime.Today).ToList();
                    break;

                case "vsechny":
                    projekts = _conn.TbProjekts.ToList();
                    break;

                case "dokoncene":
                    projekts = _conn.TbProjekts.Where(x => x.PlanovaneDokonceni <= DateTime.Today).ToList();
                    break;

                default:
                    break;
            }

            if(vlastnik == "uzivatel")
            {
                int usr = CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"));
                List<TbProjekt> vedeneProjekty = projekts.Where(x => x.Vedouci == usr).ToList();
                List<TbProjekt> prjUzivatele = new List<TbProjekt>();

                foreach(TbProjektyUzivatele2 item in _conn.TbProjektyUzivateles)
                {
                    if(item.Uzivatel == usr)
                      prjUzivatele.Add(_conn2.TbProjekts.Where(x => x.Id == item.Projekt).FirstOrDefault());
                }

                projekts = vedeneProjekty.Concat(prjUzivatele).ToList();
            }

            List<SeznamProjektuProVypis> final = new List<SeznamProjektuProVypis>();

            foreach(TbProjekt p in projekts)
            {
                SeznamProjektuProVypis tmp = new SeznamProjektuProVypis();
                tmp.Id = p.Id;
                tmp.nazev = p.Nazev;
                tmp.vedouci = _conn.TbUzivatels.Where(x => x.Id == p.Vedouci).FirstOrDefault().Jmeno + " " + _conn.TbUzivatels.Where(x => x.Id == p.Vedouci).FirstOrDefault().Prijmeni;
                tmp.dokonceni = Convert.ToDateTime(p.PlanovaneDokonceni).Date;
                tmp.ulohy = 2;
                final.Add(tmp);
            }

            return View(final);
        }

        // GET: ProjektController/Details/5
        public ActionResult Details(int id)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Autorizovano = b;

            TbProjekt prj = _conn.TbProjekts.Where(x => x.Id == id).FirstOrDefault();

            ViewBag.Vedouci = _conn.TbUzivatels.Where(x => x.Id == prj.Vedouci).FirstOrDefault().Jmeno + " " + _conn.TbUzivatels.Where(x => x.Id == prj.Vedouci).FirstOrDefault().Prijmeni;

            return View(prj);
        }

        // GET: ProjektController/Create
        public ActionResult Create()
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (b == "Uzivatel")
            {
                // NEDOSTATECNA OPRAVNENI
            }

            ViewBag.Autorizovano = b;

            return View();
        }

        // POST: ProjektController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjektController/Edit/5
        public ActionResult Edit(int id)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (b == "Uzivatel")
            {
                // NEDOSTATECNA OPRAVNENI
            }

            ViewBag.Autorizovano = b;

            return View(_conn.TbProjekts.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: ProjektController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TbProjekt projekt)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (b == "Uzivatel")
            {
                // NEDOSTATECNA OPRAVNENI
            }

            ViewBag.Autorizovano = b;

            try
            {
                TbProjekt restDetails = (from TbProjekt in _conn.TbProjekts
                                         where TbProjekt.Id == id
                                          select TbProjekt).Single();

                restDetails.Stav = projekt.Stav;
                restDetails.Vedouci = CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"));
                restDetails.DatumZacatku = projekt.DatumZacatku;
                restDetails.Nazev = projekt.Nazev;
                restDetails.PlanovaneDokonceni = projekt.PlanovaneDokonceni;
                restDetails.Popis = projekt.Popis;
                _conn.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjektController/Delete/5
        public ActionResult Delete(int id)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (b == "Uzivatel")
            {
                // NEDOSTATECNA OPRAVNENI
            }

            ViewBag.Autorizovano = b;

            _conn.TbProjekts.Remove(_conn.TbProjekts.Where(x => x.Id == id).FirstOrDefault());
            _conn.SaveChanges(true);

            return RedirectToAction(nameof(Index));
        }

       
    }
}
