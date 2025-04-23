using DMBolsaTrabajo.ServiciosExt.GenerarToken;
using System.Security.Cryptography;

namespace DMBolsaTrabajo.ServiciosExt
{
    public class ServicioToken
    {
        public string GenerateToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteToken = new byte[32];
                rng.GetBytes(byteToken);
                return Convert.ToBase64String(byteToken);
            }
        }

        public bool IsTokenValid(ResetPasswordToken token)
        {
            return token != null && token.ExpiryDate > DateTime.Now;
        }
    }
}
