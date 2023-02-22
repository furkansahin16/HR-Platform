using System.Security.Cryptography;
using System.Text;

namespace Business.Helper.PasswordHash
{
    public class HashingHelper
    {
        /// <summary>
        /// String olarak verilen şifreyi hash'ler.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        public static void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                passwordHash = (hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        /// <summary>
        /// Şifreyi veritabanıyla karşılaştırır.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, byte[] passwordHash)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                var computedHash = (hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
    }
}
