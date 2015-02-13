using Retwis.Managers;
using System;
using System.Web.Mvc;

namespace Retwis.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRetwisManager retwis;

        public AccountController()
            : this(new RetwisManager())
        {
        }

        public AccountController(IRetwisManager retwis)
        {
            this.retwis = retwis;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var errorMessage = "Invalid username or password";
            try
            {
                if (retwis.Login(username, password))
                {
                    RetwisAuthorizeAttribute.CreateCookie(username);
                    return RedirectToAction("Index", "Home", null);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return View("Error", null, errorMessage);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(string username, string password, string password2)
        {
            var errorMessage = "The password is not the same";
            try
            {
                if (password == password2)
                {
                    retwis.Register(username, password);
                    RetwisAuthorizeAttribute.CreateCookie(username);
                    return View((object)username);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return View("Error", null, errorMessage);
        }

        public ActionResult Logout()
        {
            try
            {
                RetwisAuthorizeAttribute.DeleteCookie(User.Identity.Name);
                return RedirectToAction("Index", "Home", null);
            }
            catch (Exception ex)
            {
                return View("Error", null, ex.Message);
            }
        }
    }
}