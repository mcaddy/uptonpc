namespace UptonParishCouncil.Site.Councillors.admin
{
    using System;

    /// <summary>
    /// For Reading Page
    /// </summary>
    public partial class ForReading : System.Web.UI.Page
    {
        /// <summary>
        /// Top Month (for heading display)
        /// </summary>
        int month = 0;

        /// <summary>
        /// Top Year (for heading display)
        /// </summary>
        int year = 0;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = Utils.GetMasterPage();
        }

        /// <summary>
        /// display month title if differnt to last item
        /// </summary>
        /// <param name="uploadDate">Upload Date of current item</param>
        /// <returns>A Month title (if required)</returns>
        public string MonthTitle(object uploadDate)
        {
            string value = string.Empty;

            if (uploadDate is DateTime)
            {
                if ((month != ((DateTime)uploadDate).Month) || (year != ((DateTime)uploadDate).Year))
                {
                    value = string.Format("</ul><h2>{0:MMMM yyyy}</h2><ul>", (DateTime)uploadDate);
                    month = ((DateTime)uploadDate).Month;
                    year = ((DateTime)uploadDate).Year;
                }
            }

            return value;
        }
    }
}