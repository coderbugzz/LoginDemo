using LoginDemo.Models;
using LoginDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LoginDemo.Controllers
{
    public class AccountController : Controller
    {

        Repository repository = new Repository();
        // GET: Account
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel model) //Login
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            var result = await repository.login(model);
            if (result.resultCode == 200)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("Index","Home",result.Data); //redirect to login form
            }
            else
            {
                ModelState.AddModelError("", result.message);
            }

            return View();
        }


        public  ActionResult Register() //Register
        {
            return View();
        }



        public ActionResult Logout() //Register
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index"); //redirect to index
        }


        [HttpPost]
        public async Task<ActionResult> Register(LoginViewModel model) //Register
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var result = await repository.Register(model);

            if (result.resultCode == 200)
            {
                return RedirectToAction("Index"); //redirect to login form
            }
            else
            {
                ModelState.AddModelError("", result.message);
            }

            return View();
        }

        //public bool AccountVerification(string Email, string password)
        //{
        //    bool result = false;

        //    if (Email == "freecode@gmail.com" && password == "12345")
        //    {
        //        result = true;
        //    }

        //    return result;
        //}
    }
}