using System;
using System.Web;
using System.Web.Security;

//using Notus.Authentication;

namespace Notus.Web.Core.Authentication
{
    public class DefaultFormsAuthentication : IFormsAuthentication
    {
        public void Signout()
        {
            FormsAuthentication.SignOut();
        }

        public void SetAuthCookie(HttpContextBase httpContext, FormsAuthenticationTicket authenticationTicket)
        {
            var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            httpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                Expires = CalculateCookieExpirationDate()
            });
        }

        public void SetAuthCookie(HttpContext httpContext, FormsAuthenticationTicket authenticationTicket)
        {
            var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            httpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                Expires = CalculateCookieExpirationDate()
            });
        }

        public FormsAuthenticationTicket Decrypt(string encryptedTicket)
        {
            return FormsAuthentication.Decrypt(encryptedTicket);
        }

        public void SetAuthCookie(string userName, bool persistent)
        {
            FormsAuthentication.SetAuthCookie(userName, persistent);
        }

        private static DateTime CalculateCookieExpirationDate()
        {
            return DateTime.Now.AddHours(1);
        }
    }
}