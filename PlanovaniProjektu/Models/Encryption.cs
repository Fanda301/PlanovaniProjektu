using System.Security.Cryptography;
using System.Text;

namespace PlanovaniProjektu.Models
{
    public static class Encryption
    {
        public static string Encrypt(string vstup)
        {
            vstup = vstup + "s";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(vstup);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("x2"));
                }

                return result.ToString();
            }
        }

        public static string CreateToken(string uzivatel)
        {
            DateTime dateTime = DateTime.Now;

            string tmp = DateTime.Now.ToString() + uzivatel;

            return Encrypt(tmp);
        }
    }
}
