using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UptonParishCouncil.Site.Events
{
    public partial class EventsPreview : System.Web.UI.UserControl
    {
        public DateTime maxDate = DateTime.MinValue;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateObject"></param>
        /// <returns></returns>
        protected string DateHeader(object dateObject)
        {
            DateTime date = (DateTime)dateObject;
            if (maxDate < date)
            {
                maxDate = (DateTime)Eval("Date");
                return string.Format("<div style=\"font-style:italic;\">{0:dd/MM/yyyy}</div>", date);

            }

            return string.Empty;
        }
    }
}