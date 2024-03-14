using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanovaniProjektu.Models;

namespace PlanovaniProjektu.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        DbProjektyContext _conn = new DbProjektyContext();
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Autorizovano = b;

            string token = HttpContext.Session.GetString("AutorizacniToken");

            TbAutorizacniTokeny toDelete = _conn.TbAutorizacniTokenies.Find(token);
            _conn.TbAutorizacniTokenies.Remove(toDelete);
            _conn.SaveChanges();

            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            TbUzivatel tmp = _conn.TbUzivatels.Where(x => x.Email == username || x.PrihlasovaciJmeno == username).FirstOrDefault();

            if (tmp != null && Encryption.Encrypt(password).ToString() == tmp.Heslo)
            {
                string kodTokenu = Encryption.CreateToken(tmp.PrihlasovaciJmeno);

                _conn.TbAutorizacniTokenies.Add(new TbAutorizacniTokeny() { Id = kodTokenu, Uzivatel = tmp.PrihlasovaciJmeno, DatumVystaveni = DateTime.Now, DatumPlatnosti = DateTime.Now.AddMinutes(720) });
                _conn.SaveChanges();

                HttpContext.Session.SetString("AutorizacniToken", kodTokenu);

                //if(HttpContext.Session.GetString("AutorizacniToken").ToString() == "kodTokenu"

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Neplatné přihlašovací údaje";
                return View("Login");
            }
        }

        //// GET: LoginController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: LoginController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: LoginController/Create
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

        //// GET: LoginController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LoginController/Edit/5
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

        //// GET: LoginController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LoginController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
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
