using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanovaniProjektu.Models;

namespace PlanovaniProjektu.Controllers
{
    public class UzivatelController : Controller
    {
        DbProjektyContext _conn = new DbProjektyContext();
        // GET: UzivatelController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult SeznamUzivatelu()
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
            return View(_conn.TbUzivatels.ToList());
        }

        public IActionResult SpravaLidi(int id)
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

            ViewBag.Projekt = id;  

            List<int> idUzivatelu = new List<int>();
            List<TbProjektyUzivatele2> uzivatels = new List<TbProjektyUzivatele2>();
            uzivatels = _conn.TbProjektyUzivateles.ToList();

            foreach(TbProjektyUzivatele2 pp in uzivatels)
            {
                if(pp.Projekt == id)
                    idUzivatelu.Add(Convert.ToInt32(pp.Uzivatel));
            }

            ViewBag.lidi = idUzivatelu;
            int a = Convert.ToInt32(_conn.TbProjekts.Where(x => x.Id == id).FirstOrDefault().Vedouci);

            List<TbUzivatel> zzz = _conn.TbUzivatels.Where(x => x.Role != 1).ToList().Where(x => x.Id != a).ToList();
            return View(zzz);
        }
        public IActionResult MůjProfil()
        {
            if (CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken")) == null)
                return RedirectToAction("Login", "Login");
            else
                ViewBag.Autorizovano = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));

            return View(_conn.TbUzivatels.Where(x => x.Id == CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"))).ToList());
        }

        public ActionResult PridatOsobuDoProjektu(int id, int projekt)
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

            TbProjektyUzivatele2 tmp = new TbProjektyUzivatele2();
            tmp.Projekt = projekt;
            tmp.Uzivatel = id;

            _conn.TbProjektyUzivateles.Add(tmp);
            _conn.SaveChanges();

            return RedirectToAction("SpravaLidi", "Uzivatel", new { id = projekt });
        }

        public ActionResult OdebratOsobuZProjektu(int id, int projekt)
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

            _conn.TbProjektyUzivateles.Remove(_conn.TbProjektyUzivateles.Where(x => x.Uzivatel == id && x.Projekt == projekt).FirstOrDefault());
            _conn.SaveChanges(true);

            return RedirectToAction("SpravaLidi", "Uzivatel", new { id = id });
        }

        public IActionResult ZmenaUdaju(string jmeno, string prijmeni, string email, string telefon, string mesto, string uliceCp, string psc)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Autorizovano = b;

            TbUzivatel restDetails = (from TbUzivatel in _conn.TbUzivatels
                                       where TbUzivatel.Id == CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"))
                                       select TbUzivatel).Single();

            restDetails.Jmeno = jmeno;
            restDetails.Prijmeni = prijmeni;
            restDetails.Email = email;
            restDetails.Telefon = telefon;
            restDetails.Mesto = mesto;
            restDetails.UliceCp = uliceCp;
            restDetails.Psc = psc;
            _conn.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        // project owner, projetc manager, pepa
        public IActionResult ZmenaHesla(string heslo, string noveHeslo)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Autorizovano = b;

            TbUzivatel restDetails = (from TbUzivatel in _conn.TbUzivatels
                                      where TbUzivatel.Id == CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"))
                                      select TbUzivatel).Single();

            if (restDetails != null && Encryption.Encrypt(heslo).ToString() == restDetails.Heslo)
            {
                restDetails.Heslo = Encryption.Encrypt(noveHeslo).ToString();
                _conn.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: UzivatelController/Details/5
        public ActionResult Details(int id)
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


            ViewBag.Role = _conn.TbRoles.Where(x => x.Id == _conn.TbUzivatels.Where(x => x.Id == id).FirstOrDefault().Role).FirstOrDefault().Nazev;

            return View(_conn.TbUzivatels.Where(x => x.Id == id).FirstOrDefault());
        }

        public ActionResult VypisPrace(int id, DateTime datum, DateTime datumDruhy, int projekt)
        {

            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (b == "Uzivatel" && CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken")) != id)
            {
                // NEDOSTATECNA OPRAVNENI
            }

            ViewBag.Autorizovano = b;

            List<TbUzivateleUlohy> tmp = new List<TbUzivateleUlohy>();

            if(projekt != 0)
            {
                List<TbUloha> ulohy = _conn.TbUlohas.Where(x => x.Projekt == projekt).ToList();

                foreach(TbUloha u in ulohy)
                {
                    tmp.Add(_conn.TbUzivateleUlohies.Where(x => x.Uzivatel == id && x.Uloha == u.Id).FirstOrDefault());
                }
            }
            else
            {
                tmp = _conn.TbUzivateleUlohies.Where(x => x.Uzivatel == id).ToList();
            }
            

            List<SeznamPraceProVypis> seznamPraceProVypis = new List<SeznamPraceProVypis>();
            SeznamPraceProVypis sp = new SeznamPraceProVypis();
            foreach (TbUzivateleUlohy u in tmp)
            {
                sp.nazevUlohy = _conn.TbUlohas.Where(x => x.Id == u.Uloha).FirstOrDefault().Nazev;
                sp.odpracovano = u.CasStravenyPraci;
                sp.datum = u.DatumDokonceniPrace;
                seznamPraceProVypis.Add(sp);
            }

            if(datum == Convert.ToDateTime("0001-01-01") && datumDruhy == Convert.ToDateTime("0001-01-01"))
            {
                seznamPraceProVypis = seznamPraceProVypis.ToList();
                ViewBag.datum = datum.Date;
                ViewBag.datumDruhy = datumDruhy.Date;
            }
            else if(datum == Convert.ToDateTime("0001-01-01"))
            {
                seznamPraceProVypis = seznamPraceProVypis.Where(x => x.datum <= datumDruhy.AddDays(1)).ToList();
                ViewBag.datum = datum.Date;
                ViewBag.datumDruhy = datumDruhy.Date;
            }
            else if (datumDruhy == Convert.ToDateTime("0001-01-01"))
            {
                seznamPraceProVypis = seznamPraceProVypis.Where(x => x.datum >= datum).ToList();
                ViewBag.datum = datum.Date;
                ViewBag.datumDruhy = datumDruhy.Date;
            }
            else
            {
                seznamPraceProVypis = seznamPraceProVypis.Where(x => x.datum >= datum && x.datum <= datumDruhy.AddDays(1)).ToList();
                ViewBag.datum = datum.Date;
                ViewBag.datumDruhy = datumDruhy.Date;
            }

            if(datumDruhy == Convert.ToDateTime("0001-01-01"))
                datumDruhy = DateTime.Today.AddDays(1);

            List<TbUzivateleUlohy> xxx = _conn.TbUzivateleUlohies.Where(x => x.Uzivatel == id && x.DatumDokonceniPrace >= datum && x.DatumDokonceniPrace <= datumDruhy.AddDays(1)).ToList();
            List<TbUloha> yyy = new List<TbUloha>();
                
            foreach(TbUzivateleUlohy i in xxx)
            {
                if(projekt != 0)
                {
                    if(_conn.TbUlohas.Where(x => x.Id == i.Uloha && x.Projekt == projekt).FirstOrDefault() != null)
                        yyy.Add(_conn.TbUlohas.Where(x => x.Id == i.Uloha && x.Projekt == projekt).FirstOrDefault());
                } 
                else
                    yyy.Add(_conn.TbUlohas.Where(x => x.Id == i.Uloha).FirstOrDefault());
            }
                
            ViewBag.Suma = Math.Round(CheckUser.Kvyplaceni(yyy, xxx),2);
            ViewBag.Hodiny = CheckUser.CelkemVpraci(xxx);

            ViewBag.SeznamPrace = seznamPraceProVypis;

            return View();
        }

        // GET: UzivatelController/Create
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

            ViewBag.Role = _conn.TbRoles.ToList();

            return View();
        }

        // POST: UzivatelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TbUzivatel uzivatel)
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
                uzivatel.Heslo = Encryption.Encrypt(uzivatel.Heslo);

                _conn.TbUzivatels.Add(uzivatel);
                _conn.SaveChanges(true);

                return RedirectToAction(nameof(SeznamUzivatelu));
            }
            catch
            {
                return View();
            }
        }

        // GET: UzivatelController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: UzivatelController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UzivatelController/Delete/5
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

            _conn.TbUzivatels.Remove(_conn.TbUzivatels.Where(x => x.Id == id).FirstOrDefault());
            _conn.SaveChanges(true);

            return RedirectToAction(nameof(SeznamUzivatelu));
        }

        // POST: UzivatelController/Delete/5
        
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
