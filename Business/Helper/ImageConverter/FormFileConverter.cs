using Microsoft.AspNetCore.Http;

namespace Business.Helper.ImageConverter
{
    public static class FormFileConverter
    {
        /// <summary>
        /// Web sayfasından alınan resmi root'a kaydeder. Özel bir resim ismi oluşturup bu ismi geri döndürür.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ToStringUrl(this IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var newImageName = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImageName);
            var stream = new FileStream(location, FileMode.Create);
            file.CopyTo(stream);
            return newImageName;
        }

        /// <summary>
        /// İsmi verilen resmi kayıtlı olduğu yerden siler.
        /// </summary>
        /// <param name="imageName"></param>
        public static void DeleteImageFromUrl(this string imageName)
        {
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", imageName);
            File.Delete(location);
        }
    }
}
