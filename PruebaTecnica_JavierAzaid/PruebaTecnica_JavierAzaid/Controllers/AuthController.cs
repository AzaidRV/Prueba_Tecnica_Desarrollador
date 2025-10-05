using Microsoft.AspNetCore.Mvc;
using PruebaTecnica_JavierAzaid.Business;
using PruebaTecnica_JavierAzaid.Dto.Request;
using PruebaTecnica_JavierAzaid.Models;

namespace PruebaTecnica_JavierAzaid.Controllers
{
    /// <summary>
    /// Controlador para manejar la autenticación de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Usuario y contraseña predefinidos para la demostración.
        /// </summary>
        private const string DEMO_USER = "admin";
        private const string DEMO_PASS = "1234";

        /// <summary>
        /// Inicia sesión con un usuario y contraseña predefinidos.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] RequestLogin request)
        {
            if (request.Username == DEMO_USER && request.Password == DEMO_PASS)
            {
                return Ok(new
                {
                    procesoCorrecto = true,
                    descripcion = "Inicio de sesión exitoso"
                });
            }

            return Unauthorized(new
            {
                procesoCorrecto = false,
                descripcion = "Usuario o contraseña incorrectos"
            });
        }
    }
}
