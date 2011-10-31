using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace UptonParishCouncil.Site.Events.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {

            
        }

        protected void CreateEventButton_Click(object sender, EventArgs e)
        {
            AddEvent();

            ClearAddEventForm();

            RebindEvents();
        }

        private void RebindEvents()
        {
            EventsRecuringRepeater.DataBind();
            EventsRepeater.DataBind();

            FrequencyWeekDropDownList.ClearSelection();
            FrequencyDayDropDownList.ClearSelection();
        }

        protected void CreateEventRecuringButton_Click(object sender, EventArgs e)
        {
            AddEventRecuring();

            ClearAddEvenRecuringtForm();

            RebindEvents();
        }

        private void ClearAddEventForm()
        {
            DateTextBox.Text = string.Empty;
            LocationTextBox.Text = string.Empty;
            StartTimeTextBox.Text = string.Empty;
            TitleTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
        }

        private void ClearAddEvenRecuringtForm()
        {
            StartDateTextBox.Text = string.Empty;
            EndDateTextBox.Text = string.Empty;
            FrequencyWeekDropDownList.SelectedIndex = -1;
                FrequencyWeekDropDownList.SelectedIndex = -1;
                TitleRecuringTextBox.Text = string.Empty;
                DescriptionRecuringTextBox.Text = string.Empty;

            LocationRecuringTextBox.Text = string.Empty;
            StartTimeRecuringTextBox.Text = string.Empty;
        }

        private void AddEvent()
        {
            using (SqlConnection SurveyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SurveyConnection.Open();
                using (SqlCommand SurveyCommand = new SqlCommand("UptonPC_AddEvent", SurveyConnection))
                {
                    SurveyCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    SurveyCommand.Parameters.Add("@Date", SqlDbType.DateTime);
                    SurveyCommand.Parameters["@Date"].Value = DateTime.Parse(DateTextBox.Text);

                    SurveyCommand.Parameters.Add("@Title", SqlDbType.NVarChar,50);
                    SurveyCommand.Parameters["@Title"].Value = TitleTextBox.Text;

                    SurveyCommand.Parameters.Add("@Description", SqlDbType.NVarChar,255);
                    SurveyCommand.Parameters["@Description"].Value = DescriptionTextBox.Text;

                    SurveyCommand.Parameters.Add("@Location", SqlDbType.NVarChar, 50);
                    SurveyCommand.Parameters["@Location"].Value =  LocationTextBox.Text;

                    SurveyCommand.Parameters.Add("@Time", SqlDbType.NVarChar,5);
                    SurveyCommand.Parameters["@Time"].Value = StartTimeTextBox.Text;

                    SurveyCommand.Parameters.Add("@Featured", SqlDbType.Bit);
                    SurveyCommand.Parameters["@Featured"].Value = FeaturedCheckBox.Checked;

                    SurveyCommand.ExecuteNonQuery();
                }
            }
        }

        private void AddEventRecuring()
        {
            using (SqlConnection SurveyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SurveyConnection.Open();
                using (SqlCommand SurveyCommand = new SqlCommand("UptonPC_AddEventRecuring", SurveyConnection))
                {
                    SurveyCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    SurveyCommand.Parameters.Add("@StartDate", SqlDbType.DateTime);
                    SurveyCommand.Parameters["@StartDate"].Value = DateTime.Parse(StartDateTextBox.Text);

                    if (!string.IsNullOrEmpty(EndDateTextBox.Text))
                    {
                        SurveyCommand.Parameters.Add("@EndDate", SqlDbType.DateTime);
                        SurveyCommand.Parameters["@EndDate"].Value = DateTime.Parse(EndDateTextBox.Text);
                    }

                    SurveyCommand.Parameters.Add("@DayOfWeek", SqlDbType.Int);
                    SurveyCommand.Parameters["@DayOfWeek"].Value = FrequencyDayDropDownList.SelectedValue;

                    SurveyCommand.Parameters.Add("@WeekNumber", SqlDbType.Int);
                    SurveyCommand.Parameters["@WeekNumber"].Value = FrequencyWeekDropDownList.SelectedValue;

                    SurveyCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 50);
                    SurveyCommand.Parameters["@Title"].Value = TitleRecuringTextBox.Text;

                    SurveyCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 255);
                    SurveyCommand.Parameters["@Description"].Value = DescriptionRecuringTextBox.Text;

                    SurveyCommand.Parameters.Add("@Location", SqlDbType.NVarChar, 50);
                    SurveyCommand.Parameters["@Location"].Value = LocationRecuringTextBox.Text;

                    SurveyCommand.Parameters.Add("@Time", SqlDbType.NVarChar, 5);
                    SurveyCommand.Parameters["@Time"].Value = StartTimeRecuringTextBox.Text;

                    SurveyCommand.Parameters.Add("@Featured", SqlDbType.Bit);
                    SurveyCommand.Parameters["@Featured"].Value = FeaturedRecuringCheckBox.Checked;

                    SurveyCommand.ExecuteNonQuery();
                }
            }
        }
      
        enum SqlDayOfWeek
        {
            Sunday = 1,
            Monday = 2,
            Tuesday = 3,
            Wednesday = 4,
            Thursday = 5,
            Friday = 6,
            Saturday = 7
        }

        private string WeekNumDisplay(int weekNumber)
        {
            switch (weekNumber)
            {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3rd";
                case 4:
                    return "4th";
                case 5:
                    return "5th";
                case -1:
                    return "Last";
                default:
                    return string.Empty;
            }
        }

        public string RecurranceDescription(object dayOfWeek, object weekNumber)
        {
            if (dayOfWeek.GetType() == typeof(int) &&
                weekNumber.GetType() == typeof(int))
            {
                return string.Format("{0} {1}", WeekNumDisplay((int)weekNumber), ((SqlDayOfWeek)dayOfWeek));
            }
            return string.Empty;
        }

        protected void EventsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Delete"))
            {
                using (SqlConnection SurveyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                {
                    SurveyConnection.Open();
                    
                    // Set old values
                    using (SqlCommand SurveyCommand = new SqlCommand("UptonPC_GetEvent", SurveyConnection))
                    {
                        SurveyCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        SurveyCommand.Parameters.Add("@EventId", SqlDbType.Int);
                        SurveyCommand.Parameters["@EventId"].Value = e.CommandArgument;

                        SqlDataReader SurveyReader = SurveyCommand.ExecuteReader();
                        if (SurveyReader.Read())
                        {
                            DateTextBox.Text = SurveyReader.GetDateTime(SurveyReader.GetOrdinal("Date")).ToString("dd/MM/yyyy");
                            TitleTextBox.Text = SurveyReader.GetString(SurveyReader.GetOrdinal("Title"));
                            
                            int ColumnOrdinal = SurveyReader.GetOrdinal("Description");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                DescriptionTextBox.Text = SurveyReader.GetString(ColumnOrdinal);
                            }
                            
                            ColumnOrdinal = SurveyReader.GetOrdinal("Location");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                LocationTextBox.Text = SurveyReader.GetString(ColumnOrdinal);
                            }
                            
                            ColumnOrdinal = SurveyReader.GetOrdinal("Time");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                StartTimeTextBox.Text = SurveyReader.GetString(ColumnOrdinal);
                            }
                            
                            ColumnOrdinal = SurveyReader.GetOrdinal("Featured");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                FeaturedCheckBox.Checked = SurveyReader.GetBoolean(ColumnOrdinal);
                            }
                        }
                        SurveyReader.Close();
                    }

                    // Delete Item
                    using (SqlCommand SurveyCommand = new SqlCommand("UptonPC_DeleteEvent", SurveyConnection))
                    {
                        SurveyCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        
                        SurveyCommand.Parameters.Add("@EventId", SqlDbType.Int);
                        SurveyCommand.Parameters["@EventId"].Value = e.CommandArgument;

                        SurveyCommand.ExecuteNonQuery();
                    }

                    // Update the Repeaters
                    RebindEvents();
                }
            }
        }

        protected void EventsRecuringRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Delete"))
            {
                using (SqlConnection SurveyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                {
                    SurveyConnection.Open();
                    
                    // Set old values
                    using (SqlCommand SurveyCommand = new SqlCommand("UptonPC_GetEventRecuring", SurveyConnection))
                    {
                        SurveyCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        SurveyCommand.Parameters.Add("@EventId", SqlDbType.Int);
                        SurveyCommand.Parameters["@EventId"].Value = e.CommandArgument;

                        SqlDataReader SurveyReader = SurveyCommand.ExecuteReader();
                        if (SurveyReader.Read())
                        {
                            StartDateTextBox.Text = SurveyReader.GetDateTime(SurveyReader.GetOrdinal("StartDate")).ToString("dd/MM/yyyy");

                            int ColumnOrdinal = SurveyReader.GetOrdinal("EndDate");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                EndDateTextBox.Text = SurveyReader.GetDateTime(ColumnOrdinal).ToString("dd/MM/yyyy");
                            }
                            
                            FrequencyDayDropDownList.ClearSelection();
                            FrequencyDayDropDownList.Items.FindByValue(SurveyReader.GetInt32(SurveyReader.GetOrdinal("DayOfWeek")).ToString()).Selected = true;

                            FrequencyWeekDropDownList.ClearSelection();
                            FrequencyWeekDropDownList.Items.FindByValue(SurveyReader.GetInt32(SurveyReader.GetOrdinal("WeekNumber")).ToString()).Selected = true;

                            ColumnOrdinal = SurveyReader.GetOrdinal("Title");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                TitleRecuringTextBox.Text = SurveyReader.GetString(ColumnOrdinal);
                            }

                            ColumnOrdinal = SurveyReader.GetOrdinal("Description");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                DescriptionRecuringTextBox.Text = SurveyReader.GetString(ColumnOrdinal);
                            }

                            ColumnOrdinal = SurveyReader.GetOrdinal("Location");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                LocationRecuringTextBox.Text = SurveyReader.GetString(ColumnOrdinal);
                            }

                            ColumnOrdinal = SurveyReader.GetOrdinal("Time");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                StartTimeRecuringTextBox.Text = SurveyReader.GetString(ColumnOrdinal);
                            }

                            ColumnOrdinal = SurveyReader.GetOrdinal("Featured");
                            if (!SurveyReader.IsDBNull(ColumnOrdinal))
                            {
                                FeaturedRecuringCheckBox.Checked = SurveyReader.GetBoolean(ColumnOrdinal);
                            }
                        }
                        SurveyReader.Close();
                    }
                    
                    // Delete Item
                    using (SqlCommand SurveyCommand = new SqlCommand("UptonPC_DeleteEventRecuring", SurveyConnection))
                    {
                        SurveyCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        SurveyCommand.Parameters.Add("@EventId", SqlDbType.Int);
                        SurveyCommand.Parameters["@EventId"].Value = e.CommandArgument;

                        SurveyCommand.ExecuteNonQuery();
                    }
                    
                    // Update the Repeaters
                    RebindEvents();
                }                
            }
        }

        public System.Data.SqlDbType sqlDbType { get; set; }
    }
}