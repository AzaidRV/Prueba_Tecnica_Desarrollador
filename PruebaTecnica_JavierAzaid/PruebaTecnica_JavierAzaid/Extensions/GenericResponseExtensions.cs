using PruebaTecnica_JavierAzaid.Dto;

namespace PruebaTecnica_JavierAzaid.Extensions
{
    /// <summary>
    /// Extensiones para la clase GenericResponse.
    /// </summary>
    public static class GenericResponseExtensions
    {
        /// <summary>
        /// Marca la respuesta como exitosa, estableciendo la descripción, el código de error y la respuesta.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="respuestaGenericaDto"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigoError"></param>
        /// <param name="respuesta"></param>
        /// <returns></returns>
        public static GenericResponse<T> ToSuccess<T>(this GenericResponse<T> respuestaGenericaDto, string descripcion = "", int codigoError = 0, T respuesta = default)
        {
            if (string.IsNullOrEmpty(respuestaGenericaDto.Descripcion))
                respuestaGenericaDto.Descripcion = descripcion;

            if (respuesta != null)
                respuestaGenericaDto.Respuesta = respuesta;

            respuestaGenericaDto.CodigoError = codigoError;
            respuestaGenericaDto.ProcesoCorrecto = true;
            return respuestaGenericaDto;
        }

        /// <summary>
        /// Marca la respuesta como un error, estableciendo la descripción y el código de error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="respuestaGenericaDto"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigoError"></param>
        /// <returns></returns>
        public static GenericResponse<T> ToError<T>(this GenericResponse<T> respuestaGenericaDto, string descripcion = "", int codigoError = 0)
        {
            if (string.IsNullOrEmpty(respuestaGenericaDto.Descripcion))
                respuestaGenericaDto.Descripcion = descripcion;

            respuestaGenericaDto.CodigoError = codigoError;
            respuestaGenericaDto.ProcesoCorrecto = false;
            return respuestaGenericaDto;
        }
    }
}
