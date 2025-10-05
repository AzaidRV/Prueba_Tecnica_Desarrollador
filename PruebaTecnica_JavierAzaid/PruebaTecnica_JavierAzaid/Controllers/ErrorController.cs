using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica_JavierAzaid.Dto;

namespace PruebaTecnica_JavierAzaid.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Maneja los errores no controlados en la aplicación.
        /// </summary>
        /// <returns></returns>
        [Route("error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var response = new GenericResponse<string>
            {
                ProcesoCorrecto = false,
                CodigoError = 500,
                Descripcion = "Ocurrió un error interno en el servidor.",
                Respuesta = context?.Error?.Message ?? "Error desconocido",
                TipoMensaje = "Error"
            };

            return StatusCode(500, response);
        }
    }
}
