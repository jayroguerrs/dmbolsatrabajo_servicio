namespace DMBolsaTrabajo.ServiciosExt.GenerarToken
{
    public class ResetPasswordToken
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
