using BCrypt.Net;
using System.Text;

namespace DMBolsaTrabajo.ServiciosExt
{
    public class Contrasenias
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convertir el hash a hexadecimal
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
