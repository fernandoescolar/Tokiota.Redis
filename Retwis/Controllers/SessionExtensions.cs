namespace Retwis.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Web;

    public static class SessionExtensions
    {
        private const string UserIdName = "userid"; 

        public static string GetUserId(this HttpSessionStateBase session)
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == UserIdName);
            if (claim != null)
            {
                return claim.Value;
            }

            return null;
        }

        public static string GetUsername(this HttpSessionStateBase session)
        {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}