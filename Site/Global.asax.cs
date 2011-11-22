using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.Web.Configuration;

namespace UptonParishCouncil.Site
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

            // Enable the mobile detection provider.
            HttpCapabilitiesBase.BrowserCapabilitiesProvider =
                new FiftyOne.Foundation.Mobile.Detection.MobileCapabilitiesProvider();
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // Check if a redirection is needed.
            //FiftyOne.Foundation.Mobile.Redirection.RedirectModule redirectModule = new FiftyOne.Foundation.Mobile.Redirection.RedirectModule();

            //redirectModule.OnPostAcquireRequestState(sender, e);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            // News Routes
            routes.Ignore("News/Admin/{*pathInfo}");
            routes.MapPageRoute("SingleStory",
                "News/{categoryName}/{NoticeId}/{Title}",
                "~/News/single.aspx");
            routes.MapPageRoute("News",
                "News/{categoryName}",
                "~/News/Default.aspx");
            routes.MapHttpHandlerRoute("NewsRss", "NewsRss/{categoryName}", new NewsRss());
            // Resource Routes
            routes.MapPageRoute("GetResource",
                "Resource/{ResourceId}/{Title}",
                "~/Resources/GetBlob.ashx");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception LastError = Server.GetLastError().GetBaseException();
            if (LastError.Message.Contains("Padding is invalid and cannot be removed.") ||
                LastError.Message.Contains("Invalid viewstate."))
            {
                //This is an app pool reset issue
                return;
            }

            if (LastError.Message.Contains("File does not exist."))
            {
                //Ignore Missing Files
                return;
            }

            UptonParishCouncil.Site.Utils.SendErrorMail(LastError);

            //if (Request.AppRelativeCurrentExecutionFilePath.ToLower() != "~/error.aspx")
            //{
            //    string errorUri =  "~/Error.aspx?szErrorMessage=" + Server.UrlEncode(Server.HtmlEncode(LastError.Message)) +
            //    "&szErrorDetails=" + Server.UrlEncode(Server.HtmlEncode(LastError.StackTrace)) +
            //    "&szErrorPage=" + Server.UrlEncode(Request.Url.ToString());

            //    Response.Redirect(errorUri);
            //}

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
