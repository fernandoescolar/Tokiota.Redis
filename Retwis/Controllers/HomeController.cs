using Retwis.Managers;
using Retwis.Models;
using System.Web.Mvc;
using System.Linq;

namespace Retwis.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRetwisManager retwis;

        public HomeController()
            : this(new RetwisManager())
        {
        }

        public HomeController(IRetwisManager retwis)
        {
            this.retwis = retwis;
        }

        [AllowAnonymous]
        public ActionResult Index(int start = 0)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userInfo = this.retwis.GetUserInfo(Session.GetUserId());
                var posts = this.retwis.Timeline(Session.GetUserId(), start, 10).ToList();
                var model = new HomeViewModel {
                    Followers = userInfo.Followers,
                    Following = userInfo.Following,
                    Posts = posts,
                    Prev = start - 10,
                    Next = posts.Count() >= 10 ? start + 10 : 0
                };

                return View(model);
            }

            return View("Welcome");
        }

        [AllowAnonymous]
        public ActionResult Timeline(int start = 0)
        {
            var lastUsers = this.retwis.GetLastUsers();
            var posts = this.retwis.Timeline(start, 50).ToList();
            var model = new TimelineViewModel
            {
                LastUsers = lastUsers,
                Posts = posts,
                Prev = start - 50,
                Next = posts.Count() >= 50 ? start + 50 : 0
            };

            return View(model);
        }

        public ActionResult Profile(string username, int start = 0)
        {
            var userId = this.retwis.GetUserId(username);
            var userInfo = this.retwis.GetUserInfo(userId);
            var posts = this.retwis.Timeline(userId, start, 10).ToList();
            var model = new ProfileViewModel
            {
                UserId = userId,
                Username = userInfo.Username,
                IsMe = Session.GetUserId() == userId,
                IsFollowing = this.retwis.IsFollowing(Session.GetUserId(), userId),
                Followers = userInfo.Followers,
                Following = userInfo.Following,
                Posts = posts,
                Prev = start - 10,
                Next = posts.Count() >= 10 ? start + 10 : 0
            };

            return View(model);
        }

        public ActionResult Follow(string userId, string username)
        {
            this.retwis.Follow(Session.GetUserId(), userId);
            return RedirectToAction("Profile", new { username });
        }

        public ActionResult Unfollow(string userId, string username)
        {
            this.retwis.UnFollow(Session.GetUserId(), userId);
            return RedirectToAction("Profile", new { username });
        }

        [HttpPost]
        public ActionResult Post(string message)
        {
            this.retwis.Post(Session.GetUserId(), message);
            return RedirectToAction("Index");
        }
    }
}