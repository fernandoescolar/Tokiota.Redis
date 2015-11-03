namespace Retwis.Managers
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RetwisAuthorizeAttribute : AuthorizeAttribute
    {
        private const string AuthCookieName = "retwis-auth";
        private const string TokenName = "token";
        private const string UserIdName = "userid"; 
        private const string UsernameName = "username";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!ValidateCookie() && filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length == 0)
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
        }

        public static bool ValidateCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies[AuthCookieName];
            if (cookie != null)
            {
                var token = cookie[TokenName];
                var userId = cookie[UserIdName];
                var username = cookie[UsernameName];

                if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(username))
                {
                    var storedToken = string.Empty;
                    using (var redis = RedisManager.Current.GetClient())
                    {
                        storedToken = redis.Strings.GetString("auths:" + userId);
                    }
                    if (storedToken == token)
                    {
                        var identity = new GenericIdentity(username, "Retwis");
                        identity.AddClaim(new Claim(UserIdName, userId));

                        HttpContext.Current.User = new GenericPrincipal(identity, null);
                        return true;
                    }
                }
            }

            return false;
        }

        public static void CreateCookie(string username)
        {
            var token = Guid.NewGuid().ToString();
            var userId = string.Empty;

            using (var redis = RedisManager.Current.GetClient())
            {
                userId = redis.Hashes.HGetString("users", username);
                redis.Strings.Set("auths:" + userId, token, (int)TimeSpan.FromDays(1).TotalSeconds);
            }


            var cookie = new HttpCookie(AuthCookieName);
            cookie[TokenName] = token;
            cookie[UserIdName] = userId;
            cookie[UsernameName] = username;
            cookie.Expires = DateTime.Now.AddDays(1);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void DeleteCookie(string username)
        {
            var cookie = HttpContext.Current.Request.Cookies[AuthCookieName];
            cookie.Expires = DateTime.Now.AddMinutes(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);

            using (var redis = RedisManager.Current.GetClient())
            {
                redis.Keys.Del("auths:" + username);
            }
        }
    }
}