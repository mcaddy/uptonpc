using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace UptonParishCouncil.Site.News.Admin
{
    public partial class ManagePolls : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            BindPollsList();
        }

        protected void lbPoll_Click(object sender, EventArgs e)
        {
            string Guid = ((LinkButton)sender).CommandArgument;
            ViewState["CurrentGuid"] = Guid;
            BindPollItem();
        }

        private void BindPollsList()
        {
            rptPolls.DataSource = GetPollsNodeList();
            rptPolls.DataBind();
        }

        private void BindPollItem()
        {
            fvPoll.DataSource = GetPollsNodeList(string.Format("/Polls/Poll[@Guid='{0}']", (ViewState["CurrentGuid"] != null ? ViewState["CurrentGuid"].ToString() : "-1")));
            fvPoll.DataBind();
        }

        private XmlNodeList GetPollsNodeList()
        {
            return GetPollsNodeList("/Polls/Poll");
        }
        private XmlNodeList GetPollsNodeList(string XPathFilter)
        {
            XmlDocument PollsDoc = new XmlDocument();
            PollsDoc.Load(Server.MapPath("~/App_Data/Polls.xml"));
            return PollsDoc.SelectNodes(XPathFilter);
        }

        private void SavePollsDoc(XmlDocument Doc)
        {
            Doc.Save(Server.MapPath("~/App_Data/Polls.xml"));
        }

        protected void lbToggleActive_Click(object sender, EventArgs e)
        {
            XmlDocument PollsDoc = new XmlDocument();
            PollsDoc.Load(Server.MapPath("~/App_Data/Polls.xml"));

            XmlNode CurrentPoll = PollsDoc.SelectSingleNode(string.Format("/Polls/Poll[@Guid='{0}']", ViewState["CurrentGuid"].ToString()));

            CurrentPoll.Attributes["Active"].Value = ((LinkButton)sender).CommandArgument.ToString();

            SavePollsDoc(PollsDoc);
            System.Threading.Thread.Sleep(250);
            BindPollItem();
            BindPollsList();
        }
    }
}