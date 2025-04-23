using DMBolsaTrabajo.ServiciosExt.Recaptcha;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace DMBolsaTrabajo.ServiciosExt
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _secretKey;
        private readonly double _minimumScore;
        private readonly string _ServiceRecaptcha;

        public RecaptchaService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _minimumScore = configuration.GetValue<double>("RecaptchaSettings:MinimumScore");
            _httpClientFactory = httpClientFactory;
            _secretKey = configuration["RecaptchaSettings:SecretKey"];
            _ServiceRecaptcha = configuration["RecaptchaSettings:ServiceRecaptcha"];
        }

        public async Task<bool> VerifyRecaptcha(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={token}");

                var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(response);

                // Considera también verificar el score y la acción si necesitas mayor control
                return recaptchaResponse.Success && recaptchaResponse.Score >= _minimumScore &&
                        recaptchaResponse.Action == "submit";
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return false;
            }
        }

        public async Task<RecaptchaValidationResult> ValidateRecaptchaWithExternalService(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return new RecaptchaValidationResult { Success = false, ErrorCodes = new List<string> { "Token is empty or null" } };
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var requestBody = new
                {
                    response = token,
                    secret = _secretKey
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_ServiceRecaptcha, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new RecaptchaValidationResult
                    {
                        Success = false,
                        ErrorCodes = new List<string> { $"Request failed with status code: {response.StatusCode}" }
                    };
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponses>(responseString);

                return new RecaptchaValidationResult
                {
                    Success = recaptchaResponse.Success,
                    ErrorCodes = recaptchaResponse.ErrorCodes ?? new List<string>()
                };
            }
            catch (Exception ex)
            {
                return new RecaptchaValidationResult
                {
                    Success = false,
                    ErrorCodes = new List<string> { ex.Message }
                };
            }
        }
    }

    public class RecaptchaResponses
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("ErrorCodes")]
        public List<string> ErrorCodes { get; set; } = new List<string>();
    }

    public class RecaptchaValidationResult
    {
        public bool Success { get; set; }
        public List<string> ErrorCodes { get; set; } = new List<string>();
    }
}
