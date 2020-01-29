using System;
using SuperOffice.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Helpers;

namespace WebApp
{
    public partial class Callback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;

            if(user != null && user.Identity.IsAuthenticated)
            {
                if(SuperOffice.SoContext.CurrentPrincipal != null)
                {
                    SuperOffice.SoContext.CloseCurrentSession();
                }

                var accessToken = user.Claims.Where(c => c.Type.StartsWith("access_token")).FirstOrDefault().Value;

                var mySession = SuperOffice.SoSession.Authenticate(
                    new SoAccessTokenSecurityToken(accessToken));

                using (var contactAgent = new SuperOffice.CRM.Services.ContactAgent())
                {
                    var contactEntity = contactAgent.GetMyContact();
                }

                Response.Redirect("~/");
            }
        }
    }
}