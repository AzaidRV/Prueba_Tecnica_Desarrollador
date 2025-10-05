namespace PruebaTecnica_JavierAzaid.Dto.Request
{
    /// <summary>
    /// DTO para la solicitud de inicio de sesión.
    /// </summary>
    public class RequestLogin
    {
        /// <summary>
        /// Nombre de usuario.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Password { get; set; }
    }
}
