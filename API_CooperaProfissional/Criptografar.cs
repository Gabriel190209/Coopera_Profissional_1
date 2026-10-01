using System.Security.Cryptography;
using System.Text;

namespace API_CooperaProfissional
{
    public static class Criptografar
    {
        public static string TornarHash(this string senha)
        {
            SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
