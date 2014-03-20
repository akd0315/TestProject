using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private LinkedInExample db = new LinkedInExample();

        //
        // GET: /Home/

        public ActionResult Index(string Code)
        {
            if (Code == null)
            {
                ViewBag.RedirectUrl = Authenticate();
            }
            else
            {
                LinkedIn li = new LinkedIn();
               AccessToken a  = ConfirmAuthentication(Code);
                OAuth2 oauth = new OAuth2();
             string details =  oauth.GetData("v1/people/~", a.access_token);
            }
            return View(db.Users.ToList());
        }


        public string Authenticate()
        {
            OAuth2 oAuth = new OAuth2();
            return oAuth.GetAuthentication("r_fullprofile%20r_emailaddress%20r_network%20rw_nus", "http://localhost:61575/Home/");
        }
        public AccessToken ConfirmAuthentication(string code)
        {
            OAuth2 oAuth = new OAuth2();
            string tokenDetails = oAuth.VerifyAuthentication(code, "http://localhost:61575/Home/");
           return new JavaScriptSerializer().Deserialize<AccessToken>(tokenDetails);
            
        }
        public String PostStatus(string text, string accessToken)
        {
            OAuth2 oAuth = new OAuth2();
            var sign = "grant_type=authorization_code&code=" + HttpUtility.UrlEncode(code) + "&redirect_uri=" + HttpUtility.HtmlEncode(redirectUrl) + "&client_id=" + lIn.ConsumerKey + "&client_secret=" + lIn.ConsumerSecret;

            string postDetails = oAuth.PostData("/people/~/shares", accessToken);

            return postDetails;
        }
        //
        // GET: /Home/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}