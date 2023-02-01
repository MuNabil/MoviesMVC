namespace MoviesMVC.Services
{
    public class ImageService
    {
        public static string NewImageUrl(IFormFile? ImageFile, string? oldImageUrl = null)
        {
            string? newImageUrl = UploadImage(ImageFile);

            if (newImageUrl != null)
            {
                DeleteImage(oldImageUrl);
            }
            else
            {
                newImageUrl = oldImageUrl ?? ImageCons.DefaultImage;
            }
            return newImageUrl;
        }

        public static string? UploadImage(IFormFile? ImageFile)
        {
            if (ImageFile != null)
            {
                var imageName = ImageFile.FileName;

                string uniqueImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageName);

                var imagePath = Path.Combine(@"wwwroot/", "Images", uniqueImageName);
                ImageFile.CopyTo(new FileStream(imagePath, FileMode.Create));

                return uniqueImageName;
            }
            else
            {
                return null;
            }
        }
        public static bool DeleteImage(string? OldImageUrl)
        {
            try
            {
                if (OldImageUrl != null && OldImageUrl != ImageCons.DefaultImage && OldImageUrl != Guid.Empty.ToString())
                {
                    var OldImage = Path.Combine(@"wwwroot/", "Images", OldImageUrl);

                    if (System.IO.File.Exists(OldImage))
                        System.IO.File.Delete(OldImage);
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}