using System.Data.OleDb;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections;
using System.Web.UI.WebControls;
using System;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace UptonParishCouncil.Site.News.Admin
{
    public partial class FileUploader : System.Web.UI.UserControl
    {
        public string FilesBaseLocation
        {
            get { return (ViewState["FilesBaseLocation"] != null ? ViewState["FilesBaseLocation"].ToString() : string.Empty); }
            set { ViewState["FilesBaseLocation"] = value; }
        }

        private ArrayList UnpackZip(string zipPath)
        {
            ArrayList filesUnziped = new ArrayList();
            string zipPathDirectoryName = string.Format(@"{0}\{1}\", Path.GetDirectoryName(zipPath), Path.GetFileNameWithoutExtension(zipPath));
            Directory.CreateDirectory(zipPathDirectoryName);
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipPath)))
            {

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name).Replace("&", "and");

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(zipPathDirectoryName + fileName))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        filesUnziped.Add(zipPathDirectoryName + fileName);
                    }
                }
            }
            return filesUnziped;
        }

        private void UploadPhotos()
        {
            string saveDirPath = string.Format(@"{0}\{1}-{2}-{3}\", Server.MapPath(FilesBaseLocation), DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Directory.CreateDirectory(saveDirPath);

            ArrayList filesToProcess = new ArrayList();

            ProcessFileUpload(fuPhoto1, filesToProcess, saveDirPath);

            foreach (string fileName in filesToProcess)
            {
                switch (Path.GetExtension(fileName).ToLower())
                {
                    case ".jpg":
                        Mrc.Common.ImageUtils.ResizeJpeg(fileName, 640, 640);
                        break;
                }
            }
        }

        private void ProcessFileUpload(FileUpload fuPhoto, ArrayList FileList, string saveDirPath)
        {
            if (fuPhoto.HasFile)
            {
                string fullSavePath = saveDirPath + fuPhoto.FileName.Replace("&", "and"); ;
                fuPhoto.SaveAs(fullSavePath);
                if (Path.GetExtension(fullSavePath).ToLower() == ".zip")
                {
                    ArrayList tempList = UnpackZip(fullSavePath);
                    FileList.AddRange(tempList);
                    File.Delete(fullSavePath);
                }
                else
                {
                    FileList.Add(fullSavePath);
                }
            }
        }

        private void AddItemToDB(int GalleryId, string PhotoPath, string Photographer)
        {
            string toRemove = Server.MapPath(FilesBaseLocation);
            string reletivePhotoPath = PhotoPath.Replace(toRemove, "").Replace("\\", "/");
            using (SqlConnection conUptonPCSql = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conUptonPCSql.Open();
                using (SqlCommand cmdCurrentGallery = new SqlCommand("INSERT INTO Items (szName,szLocation,iGalleryId,szCopyright) VALUES (@szName,@szLocation,@iGalleryId,@szCopyright)", conUptonPCSql))
                {
                    cmdCurrentGallery.Parameters.AddWithValue("@szName", Path.GetFileNameWithoutExtension(PhotoPath));
                    cmdCurrentGallery.Parameters.AddWithValue("@szLocation", reletivePhotoPath);
                    cmdCurrentGallery.Parameters.AddWithValue("@iGalleryId", GalleryId);
                    cmdCurrentGallery.Parameters.AddWithValue("@szCopyright", Photographer);
                    cmdCurrentGallery.ExecuteNonQuery();
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadPhotos();
        }
    }
}
