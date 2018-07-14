using SportsStore.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            var success = FormsAuthentication.Authenticate(username, password);
            if (success)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return success;
        }
    }
}