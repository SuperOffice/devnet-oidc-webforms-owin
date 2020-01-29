using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Security;
using SuperOffice;
using SuperOffice.License;
using SuperOffice.Security.Principal;
using SuperOffice.Services75;
using UserType = SuperOffice.License.UserType;
using SuperOffice.Factory;
using Newtonsoft.Json.Linq;

namespace WebApp.Helpers
{

    /// <summary>
    /// Untility class supporting SuperOffice Online Authentication
    /// </summary>
    public static class SuperOfficeAuthHelper
    {
        /// <summary>
        /// Is a user authenticated with NetServer
        /// </summary>
        /// <returns></returns>
        internal static bool IsAuthenticatedWithNetServer()
        {
            ClassFactory.Init();

            var principal = SoContext.CurrentPrincipal;
            return IsAuthenticatedWithNetServer(principal);
        }

        private static bool IsAuthenticatedWithNetServer(SoPrincipal principal)
        {
            return principal != null && principal.UserType != UserType.AnonymousAssociate;
        }
    }
}