using System.Drawing;

namespace Business.Helper.ImageConverter
{
    public static class Converter
    {
        public static byte[] ConvertToByteArray(this Image img)
        {
            if (img != null)
            {
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
            }
            return null;
        }

        public static Image ConvertToImage(this byte[] byteArr)
        {
            if (byteArr == null) return null;
            MemoryStream ms = new MemoryStream(byteArr, 0, byteArr.Length);
            ms.Write(byteArr, 0, byteArr.Length);
            Image img = Image.FromStream(ms, true);
            ms.Close();
            return img;
        }
    }
}
