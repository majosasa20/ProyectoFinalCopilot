namespace AdventureWorks.Enterprise.Api.Models.Configuration
{
    /// <summary>
    /// Configuraci�n de API Settings
    /// </summary>
    public class ApiSettings
    {
        public const string SectionName = "ApiSettings";
        
        /// <summary>
        /// Clave secreta para autenticaci�n de API
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Valida que la configuraci�n sea correcta
        /// </summary>
        /// <returns>True si es v�lida</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ApiKey) && ApiKey.Length >= 32;
        }
    }
}