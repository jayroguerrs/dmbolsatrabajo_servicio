using Newtonsoft.Json;

namespace DMBolsaTrabajo.ServiciosExt.Recaptcha
{
    public class RecaptchaResponse
    {
        public bool Success { get; set; }
        public double Score { get; set; } // Solo para v3 (0.0 a 1.0)
        public string Action { get; set; } // Solo para v3
        public DateTime ChallengeTs { get; set; }
        public string Hostname { get; set; }
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
