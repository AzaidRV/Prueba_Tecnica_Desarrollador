namespace PruebaTecnica_JavierAzaid.Dto
{
    /// <summary>
    /// DTO genérico para respuestas de servicios.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericResponse<T>
    {
        /// <summary>
        /// Respuesta del servicio.
        /// </summary>
        public T Respuesta { get; set; }

        /// <summary>
        /// Indica si el proceso fue correcto.
        /// </summary>
        public bool ProcesoCorrecto { get; set; }

        /// <summary>
        /// Código de error en caso de fallo.
        /// </summary>
        public int CodigoError { get; set; }

        /// <summary>
        /// Descripción del resultado o error.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Tipo de mensaje (e.g., "Error", "Warning", "Info").
        /// </summary>
        public string TipoMensaje { get; set; }

        /// <summary>
        /// Constructor por defecto que inicializa las propiedades.
        /// </summary>
        public GenericResponse()
        {
           ProcesoCorrecto = false;
           CodigoError = 0;
           Descripcion = string.Empty;
           TipoMensaje = string.Empty;
        }
    }
}
