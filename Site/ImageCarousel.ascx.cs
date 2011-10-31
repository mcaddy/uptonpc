using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace UptonParishCouncil.Site
{
    public partial class ImageCarousel : System.Web.UI.UserControl
    {
        public string ModuleDiv;

        public string CycleEffect { get; set; }
        public string CycleParameters { get; set; }

        public string ImageSourcePath { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CycleEffect))
            {
                CycleEffect = "fade";
            }

            if (string.IsNullOrEmpty(CycleParameters))
            {
                CycleParameters = "timeout: 4000, delay: -2000, pause: 1, pager: '#nav', slideExpr: 'img', random: 1";
            }
            // Make sure we can handle multiple instnaces
            ModuleDiv = "Carousel_" + this.ID.ToString();

            string photosFilePath = Server.MapPath(ImageSourcePath);

            if (Directory.Exists(photosFilePath))
            {
                string[] files = Directory.GetFiles(photosFilePath);
                List<string> images = new List<string>();
                foreach (string file in files)
                {
                    if (Mrc.Common.Utils.GetMimeType(Path.GetExtension(file)).Contains("image"))
                    {
                        images.Add(file.Replace(photosFilePath, ImageSourcePath));
                    }
                }

                lstPhotos.DataSource = images;
                lstPhotos.DataBind();
            }
        }
    }
}