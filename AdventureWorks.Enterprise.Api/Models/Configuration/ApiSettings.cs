namespace AdventureWorks.Enterprise.Api.Models.Configuration
{
    /// <summary>
    /// Configuración de API Settings
    /// </summary>
    public class ApiSettings
    {
        public const string SectionName = "ApiSettings";
        
        /// <summary>
        /// Clave secreta para autenticación de API
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Valida que la configuración sea correcta
        /// </summary>
        /// <returns>True si es válida</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ApiKey) && ApiKey.Length >= 32;
        }
    }
}