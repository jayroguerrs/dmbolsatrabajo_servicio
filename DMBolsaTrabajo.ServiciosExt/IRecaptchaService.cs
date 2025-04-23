namespace DMBolsaTrabajo.ServiciosExt
{
    public interface IRecaptchaService
    {
        Task<bool> VerifyRecaptcha(string token);
        Task<RecaptchaValidationResult> ValidateRecaptchaWithExternalService(string token);
    }
}