using System.Text.RegularExpressions;

namespace Business.Helper.MailOptions
{
    public static class MailCreateHelper
    {
        /// <summary>
        /// İsim, soyisim ve şirket ismine göre mail adresi oluşrur.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="companyInfo"></param>
        /// <returns></returns>
        public static string CreateMail(string firstName, string lastName, string companyInfo)
        {
            if (firstName is not null && lastName is not null && companyInfo is not null)
            {
                firstName = firstName.ToLower().Trim();
                lastName = lastName.ToLower().Trim();
                companyInfo = companyInfo.ToLower().Trim();
                companyInfo = Regex.Replace(companyInfo, @"\s+", "");
                firstName = Regex.Replace(firstName, @"\s+", "");
                lastName = Regex.Replace(lastName, @"\s+", "");

                return $"{ConvertUtf8(firstName)}.{ConvertUtf8(lastName)}@{ConvertUtf8(companyInfo)}.com";
            }
            return null;
        }

        /// <summary>
        /// Mail adresindeki Türçe karekterleri düzeltir.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string ConvertUtf8(string name)
        {
            name = name.Replace('ö', 'o');
            name = name.Replace('ü', 'u');
            name = name.Replace('ğ', 'g');
            name = name.Replace('ş', 's');
            name = name.Replace('ı', 'i');
            name = name.Replace('ç', 'c');
            return name;
        }
    }
}
