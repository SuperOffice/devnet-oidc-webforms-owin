using System;
using System.Linq;
using SuperOffice.Security.Principal;
using System.Configuration;
using System.Web;
using System.Security.Claims;

namespace WebApp.Helpers
{

    /// <summary>
    /// Plugin responsible for setting tenant specific information, like service url
    /// </summary>
    [ContextInitializerPlugin("OnlineTenantContextInitializer")]
    public class ContextInitializer : IContextInitializerPlugin
    {
        public void InitializeContext(string contextIdentifier)
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null && httpContext.User != null && 
                httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
            {
                var ctx = httpContext.User as ClaimsPrincipal;

                if(ctx != null)
                {
                    var contextId = ctx.Claims.Where(c => c.Type.Contains("ctx")).FirstOrDefault().Value;

                    if (String.Equals(contextIdentifier, contextId, StringComparison.InvariantCultureIgnoreCase))
                    {
                        var netserverUrl = ctx.Claims.Where(c => c.Type.Contains("netserver_url")).FirstOrDefault().Value;

                        SuperOffice.Configuration.ConfigFile.WebServices.RemoteBaseURL = netserverUrl;
                    }
                }
                // Set the tenants url.

                //Add more application specific modifications
                //...
            }

        }
    }
}