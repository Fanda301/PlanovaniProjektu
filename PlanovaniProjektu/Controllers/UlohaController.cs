using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanovaniProjektu.Models;

namespace PlanovaniProjektu.Controllers
{
    public class UlohaController : Controller
    {
        DbProjektyContext _conn = new DbProjektyContext();
        // GET: UlohaController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult NovaUloha(int id)
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

            HttpContext.Session.SetString("CisloUlohy", id.ToString());
            return View();
        }

        public ActionResult VytvoreniUlohy(TbUloha uloha)
        {
            if (CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken")) == null)
                return RedirectToAction("Login", "Login");
            else if (CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken")) == "Uzivatel")
                return RedirectToAction("Details", "Projekt", new { id = Convert.ToInt32(HttpContext.Session.GetString("CisloUlohy")) });
            else
                ViewBag.Autorizovano = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));

            uloha.Projekt = Convert.ToInt32(HttpContext.Session.GetString("CisloUlohy"));
            _conn.TbUlohas.Add(uloha);
            _conn.SaveChanges();

            return RedirectToAction("Details", "Projekt", new { id = uloha.Projekt });
        }

        public ActionResult SeznamUloh(int id)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Autorizovano = b;

            ViewBag.id = id;
            return View(_conn.TbUlohas.Where(x => x.Projekt == id).ToList());
        }

        // GET: UlohaController/Details/5
        public ActionResult Details(int id)
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Autorizovano = b;


            return View(_conn.TbUlohas.Where(x => x.Id == id).FirstOrDefault());
        }

        // GET: UlohaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UlohaController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
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

        public ActionResult AktivniPrace(int id)
        {
            if (_conn.TbUlohas.Where(x => x.Id == id).FirstOrDefault().JeSpracovavana)
                return RedirectToAction("Details", "Uloha", new { id = id });

            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Autorizovano = b;

            int uzivatel = CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"));
            HttpContext.Session.SetString("IdUlohy", id.ToString());
            ViewBag.NazevUlohy = _conn.TbUlohas.Where(x => x.Id == id).FirstOrDefault().Nazev;
            ViewBag.Popis = _conn.TbUlohas.Where(x => x.Id == id).FirstOrDefault().Popis;

            try
            {
                TbUloha restDetails = (from TbUloha in _conn.TbUlohas
                                       where TbUloha.Id == id
                                       select TbUloha).Single();

                restDetails.JeSpracovavana = true;
                _conn.SaveChanges();
            }
            catch
            {
                return View();
            }

            return View();
        }

        public ActionResult UlozeniAktivniPrace(int stopkyHodiny, int stopkyMinuty, int stopkySekundy)
        {
            int uzivatel = CheckUser.IdUzivatele(HttpContext.Session.GetString("AutorizacniToken"));
            int uloha = Convert.ToInt32(HttpContext.Session.GetString("IdUlohy"));

            try
            {
                TbUloha restDetails = (from TbUloha in _conn.TbUlohas
                                       where TbUloha.Id == uloha
                                       select TbUloha).Single();

                restDetails.JeSpracovavana = false;
                _conn.SaveChanges();
            }
            catch
            {

            }

            TbUzivateleUlohy tmp = new TbUzivateleUlohy();
            tmp.Uzivatel = uzivatel;
            tmp.Uloha = uloha;
            tmp.CasStravenyPraci = new TimeSpan(stopkyHodiny, stopkyMinuty, stopkySekundy);
            tmp.DatumDokonceniPrace = DateTime.Now;

            _conn.TbUzivateleUlohies.Add(tmp);
            _conn.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: UlohaController/Edit/5
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

            return View(_conn.TbUlohas.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: UlohaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TbUloha uloha)
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
                TbUloha restDetails = (from TbUloha in _conn.TbUlohas
                                       where TbUloha.Id == id
                                       select TbUloha).Single();

                restDetails.Nazev = uloha.Nazev;
                restDetails.HodinovaSazba = uloha.HodinovaSazba;
                restDetails.Popis = uloha.Popis;
                restDetails.PredpokladanaDobaTrvani = uloha.PredpokladanaDobaTrvani;
                _conn.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: UlohaController/Delete/5
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

            _conn.TbUlohas.Remove(_conn.TbUlohas.Where(x => x.Id == id).FirstOrDefault());
            _conn.SaveChanges(true);

            return RedirectToAction("Index", "Home");
        }

        // POST: UlohaController/Delete/5
       
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
