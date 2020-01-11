using ServiceMadeEasy.Com.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ServiceMadeEasy.Com.Areas.Admin.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class BaseController : Controller
    {
        public DateTime DateTime { get; set; }
      
        protected string StorageRoot
        {
            get
            {
                string path = Path.Combine(Server.MapPath("~/Images"));

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        #region File Upload Code
        public string FileExtn { get; set; }
        public string CaptionText { get; set; }
        protected string GetMediaType(string FileExtn)
        {
            return "Image";
        }

        protected string StorageRootForBlogMedia
        {
            get
            {
                string path = Path.Combine(Server.MapPath("~/BlogMedia"));

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        public JsonResult UploadFilesResult { get; set; }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void DeleteFile(string id)
        {
            var filename = id;
            var filePath = Path.Combine(StorageRoot, filename);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Download(string fileName)
        {

            var filePath = Path.Combine(StorageRoot, fileName);

            var context = HttpContext;

            if (System.IO.File.Exists(filePath))
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                context.Response.ContentType = "application/octet-stream";
                //context.Response.ClearContent();
                context.Response.WriteFile(filePath);
            }
            else
                context.Response.StatusCode = 404;
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS

        protected bool UploadFiles(string uniqueFileName, bool isBlogMedia = false)
        {
            var r = new List<ViewDataUploadFilesResult>();
            try
            {
                foreach (string file in Request.Files)
                {
                    var statuses = new List<ViewDataUploadFilesResult>();
                    var headers = Request.Headers;


                    if (string.IsNullOrEmpty(headers["X-File-Name"]))
                    {
                        UploadWholeFile(Request, statuses, uniqueFileName, isBlogMedia);

                    }
                    else
                    {
                        UploadPartialFile(headers["X-File-Name"], Request, statuses, uniqueFileName, isBlogMedia);
                    }

                    UploadFilesResult = Json(statuses);
                    UploadFilesResult.ContentType = "text/plain";
                }
                if (Request.Form["captionText"] != null)
                {
                    CaptionText = Request.Form["captionText"].ToString();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ViewDataUploadFilesResult> statuses, string uniqueFileName, bool isBlogMedia = false)
        {
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var fullPath = Path.Combine(StorageRoot, uniqueFileName);

            using (var fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(new ViewDataUploadFilesResult()
            {
                name = file.FileName,
                size = file.ContentLength,
                type = file.ContentType,
                url = fullPath,
                delete_url = fullPath,
                thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath),
                delete_type = "GET",
            });
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses, string uniqueFileName, bool isBlogMedia = false)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                FileExtn = Path.GetExtension(file.FileName);

                string uniqueFileNamewithExtn = uniqueFileName + FileExtn;
                var fullPath = string.Empty;
                if (isBlogMedia)
                {
                    fullPath = Path.Combine(StorageRootForBlogMedia, uniqueFileNamewithExtn);
                }
                else
                {
                    fullPath = Path.Combine(StorageRoot, uniqueFileNamewithExtn);
                }


                file.SaveAs(fullPath);

                if (ISValidFileType(fullPath))
                {
                    ImageCompression imgCompress = ImageCompression.GetImageCompressObject;
                    imgCompress.GetImage = new System.Drawing.Bitmap(Image.FromFile(fullPath));
                    //imgCompress.Height = 190;
                    //imgCompress.Width = 190;

                    if (isBlogMedia)
                    {
                        imgCompress.Save("small_" + uniqueFileName + FileExtn, StorageRootForBlogMedia);
                    }
                    else
                    {
                        imgCompress.Save("small_" + uniqueFileName + FileExtn, StorageRoot);
                    }


                }

                if (isBlogMedia)
                {
                    statuses.Add(new ViewDataUploadFilesResult()
                    {
                        name = file.FileName,
                        size = file.ContentLength,
                        type = file.ContentType,
                        url = StorageRootForBlogMedia + "/" + uniqueFileNamewithExtn,
                        delete_url = StorageRootForBlogMedia + "/" + uniqueFileNamewithExtn,
                        thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath),
                        delete_type = "GET",
                    });

                }
                else
                {
                    statuses.Add(new ViewDataUploadFilesResult()
                    {
                        name = file.FileName,
                        size = file.ContentLength,
                        type = file.ContentType,
                        url = StorageRoot + "/" + uniqueFileNamewithExtn,
                        delete_url = StorageRoot + "/" + uniqueFileNamewithExtn,
                        thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath),
                        delete_type = "GET",
                    });
                }

            }
        }

        private bool ISValidFileType(string fileName)
        {
            bool isValidExt = false;
            string fileExt = Path.GetExtension(fileName);
            switch (fileExt.ToLower())
            {
                case ImageType.JPEG:
                case ImageType.BTM:
                case ImageType.JPG:
                case ImageType.PNG:
                    isValidExt = true;
                    break;
            }
            return isValidExt;
        }

        public string UploadMediaURL(string mediaLink)
        {
            string YoutubeCode;
            string VideoUrl = "";

            return "false";
        }

        #endregion
    }

    public static class ImageHelper
    {
        public static byte[] CropImage(byte[] content, int x, int y, int width, int height)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                return CropImage(stream, x, y, width, height);
            }
        }


        public static byte[] ResizeImageFixedWidth(Image imgToResize, int width)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = ((float)width / (float)sourceWidth);

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            using (Bitmap newBitMap = new Bitmap(destWidth, destHeight))
            {
                //Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)newBitMap);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();

                return GetBitmapBytes(newBitMap);
            }
        }

        public static byte[] CropImage(Stream content, int x, int y, int width, int height)
        {
            //Parsing stream to bitmap
            using (Bitmap sourceBitmap = new Bitmap(content))
            {
                //Get new dimensions
                double sourceWidth = Convert.ToDouble(sourceBitmap.Size.Width);
                double sourceHeight = Convert.ToDouble(sourceBitmap.Size.Height);
                Rectangle cropRect = new Rectangle(x, y, Convert.ToInt32(width), height);

                //Creating new bitmap with valid dimensions
                using (Bitmap newBitMap = new Bitmap(cropRect.Width, cropRect.Height))
                {
                    using (Graphics g = Graphics.FromImage(newBitMap))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawImage(sourceBitmap, new Rectangle(0, 0, newBitMap.Width, newBitMap.Height), cropRect, GraphicsUnit.Pixel);

                        return GetBitmapBytes(newBitMap);
                    }
                }
            }
        }
        public static byte[] GetBitmapBytes(Bitmap source)
        {
            //Settings to increase quality of the image
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[4];
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            //Temporary stream to save the bitmap
            using (MemoryStream tmpStream = new MemoryStream())
            {
                source.Save(tmpStream, codec, parameters);

                //Get image bytes from temporary stream
                byte[] result = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(result, 0, (int)tmpStream.Length);

                return result;
            }
        }
    }
    public class ImageType
    {
        public const string JPEG = ".jpeg";
        public const string PNG = ".png";
        public const string JPG = ".jpg";
        public const string BTM = ".btm";
    }
    public static class FileHelper
    {
        public static void SaveFile(byte[] content, string path)
        {
            string filePath = GetFileFullPath(path);
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            //Save file
            using (FileStream str = File.Create(filePath))
            {
                str.Write(content, 0, content.Length);
            }
        }

        public static string GetFileFullPath(string path)
        {
            string relName = path.StartsWith("~") ? path : path.StartsWith("/") ? string.Concat("~", path) : path;

            string filePath = relName.StartsWith("~") ? HostingEnvironment.MapPath(relName) : relName;

            return filePath;
        }
    }
    public class ViewDataUploadFilesResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string delete_url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_type { get; set; }
    }
    public class ImageCompression
    {
        #region[PrivateData]
        private static volatile ImageCompression imageCompress;
        private Bitmap bitmap;
        private int width;
        private int height;
        private Image img;
        #endregion[Privatedata]

        #region[Constructor]
        /// <summary>
        /// It is used to restrict to create the instance of the ImageCompression
        /// </summary>
        private ImageCompression()
        {
        }
        #endregion[Constructor]

        #region[Poperties]
        /// <summary>
        /// Gets ImageCompression object
        /// </summary>
        public static ImageCompression GetImageCompressObject
        {
            get
            {
                if (imageCompress == null)
                {
                    imageCompress = new ImageCompression();
                }
                return imageCompress;
            }
        }

        /// <summary>
        /// Gets or sets Width
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Gets or sets Width
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets or sets Image
        /// </summary>
        public Bitmap GetImage
        {
            get { return bitmap; }
            set { bitmap = value; }
        }
        #endregion[Poperties]

        #region[PublicFunction]
        /// <summary>
        /// This function is used to save the image
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        public void Save(string fileName, string path)
        {
            if (ISValidFileType(fileName))
            {
                string pathaname = path + @"\" + fileName;
                save(pathaname, 60);
            }
        }
        #endregion[PublicFunction]

        #region[PrivateData]
        /// <summary>
        /// This function is use to compress the image to
        /// predefine size
        /// </summary>
        /// <returns>return bitmap in compress size</returns>
        private Image CompressImage()
        {
            if (GetImage != null)
            {
                //Width = (Width == 0) ? GetImage.Width : Width;
                //Height = (Height == 0) ? GetImage.Height : Height;
                if (GetImage.Height <= 400 && GetImage.Width <= 400)
                {
                    Height = GetImage.Height;
                    Width = GetImage.Width;
                }
                else
                {
                    Height = (GetImage.Height / GetImage.Width) * GetImage.Width / 2;
                    Width = GetImage.Width / 2;
                    if (Height == 0)
                    {
                        Width = (GetImage.Width / GetImage.Height) * GetImage.Height / 2;
                        Height = GetImage.Height / 2;
                    }
                }

                Bitmap newBitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                newBitmap = bitmap;
                //newBitmap.MakeTransparent();
                //newBitmap.SetResolution(80, 80);
                return newBitmap.GetThumbnailImage(Width, Height, null, IntPtr.Zero);
            }
            else
            {
                throw new Exception("Please provide bitmap");
            }
        }

        /// <summary>
        /// This function is used to check the file Type
        /// </summary>
        /// <param name="fileName">String data type:contain the file name</param>
        /// <returns>true or false on the file extention</returns>
        private bool ISValidFileType(string fileName)
        {
            bool isValidExt = false;
            string fileExt = Path.GetExtension(fileName);
            switch (fileExt.ToLower())
            {
                case ImageType.JPEG:
                case ImageType.BTM:
                case ImageType.JPG:
                case ImageType.PNG:
                    isValidExt = true;
                    break;
            }
            return isValidExt;
        }

        /// <summary>
        /// This function is used to get the imageCode info
        /// on the basis of mimeType
        /// </summary>
        /// <param name="mimeType">string data type</param>
        /// <returns>ImageCodecInfo data type</returns>
        private ImageCodecInfo GetImageCodeInfo(string mimeType)
        {
            ImageCodecInfo[] codes = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codes.Length; i++)
            {
                if (codes[i].MimeType == mimeType)
                {
                    return codes[i];
                }
            }
            return null;
        }
        /// <summary>
        /// this function is used to save the image into a
        /// given path
        /// </summary>
        /// <param name="path">string data type</param>
        /// <param name="quality">int data type</param>
        private void save(string path, int quality)
        {
            img = CompressImage();
            ////Setting the quality of the picture
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ////Seting the format to save
            ImageCodecInfo imageCodec = GetImageCodeInfo("image/jpeg");
            ////Used to contain the poarameters of the quality
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = qualityParam;
            ////Used to save the image to a  given path
            img.Save(path, imageCodec, parameters);
        }
        #endregion[PrivateData]
    }
}