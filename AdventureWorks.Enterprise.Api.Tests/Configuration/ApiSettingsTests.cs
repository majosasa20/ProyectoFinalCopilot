using AdventureWorks.Enterprise.Api.Models.Configuration;
using AdventureWorks.Enterprise.Api.Tests.Infrastructure;

namespace AdventureWorks.Enterprise.Api.Tests.Configuration
{
    /// <summary>
    /// Tests para la configuración de ApiSettings
    /// </summary>
    public class ApiSettingsTests : BaseControllerTest
    {
        #region Constructor Tests

        [Fact]
        public void ApiSettings_DeberiaInicializarseCorrectamente()
        {
            // Act
            var apiSettings = new ApiSettings();

            // Assert
            apiSettings.Should().NotBeNull();
            apiSettings.ApiKey.Should().NotBeNull();
            apiSettings.ApiKey.Should().BeEmpty();
        }

        #endregion

        #region SectionName Tests

        [Fact]
        public void SectionName_DeberiaRetornarNombreCorrect()
        {
            // Act & Assert
            ApiSettings.SectionName.Should().Be("ApiSettings");
        }

        #endregion

        #region IsValid Tests

        [Fact]
        public void IsValid_DeberiaRetornarFalse_CuandoApiKeyEsNull()
        {
            // Arrange
            var apiSettings = new ApiSettings { ApiKey = null! };

            // Act
            var result = apiSettings.IsValid();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_DeberiaRetornarFalse_CuandoApiKeyEsVacia()
        {
            // Arrange
            var apiSettings = new ApiSettings { ApiKey = string.Empty };

            // Act
            var result = apiSettings.IsValid();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_DeberiaRetornarFalse_CuandoApiKeyEsEspaciosEnBlanco()
        {
            // Arrange
            var apiSettings = new ApiSettings { ApiKey = "   " };

            // Act
            var result = apiSettings.IsValid();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_DeberiaRetornarFalse_CuandoApiKeyEsMenorA32Caracteres()
        {
            // Arrange
            var apiSettings = new ApiSettings { ApiKey = "short-key" }; // 9 caracteres

            // Act
            var result = apiSettings.IsValid();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_DeberiaRetornarTrue_CuandoApiKeyTiene32Caracteres()
        {
            // Arrange
            var apiSettings = new ApiSettings { ApiKey = "12345678901234567890123456789012" }; // 32 caracteres

            // Act
            var result = apiSettings.IsValid();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValid_DeberiaRetornarTrue_CuandoApiKeyTieneMasDe32Caracteres()
        {
            // Arrange
            var apiSettings = new ApiSettings { ApiKey = "AW-2024-ENTERPRISE-API-KEY-SAGASTUME-SECRET-7891234567890" }; // > 32 caracteres

            // Act
            var result = apiSettings.IsValid();

            // Assert
            result.Should().BeTrue();
        }

        #endregion

        #region Property Tests

        [Fact]
        public void ApiKey_DeberiaPermitirAsignacionYLectura()
        {
            // Arrange
            var apiSettings = new ApiSettings();
            var expectedApiKey = "test-api-key-for-validation-purposes-123456789";

            // Act
            apiSettings.ApiKey = expectedApiKey;

            // Assert
            apiSettings.ApiKey.Should().Be(expectedApiKey);
        }

        #endregion

        #region Edge Cases Tests

        [Theory]
        [InlineData(31, false)] // Un carácter menos del mínimo
        [InlineData(32, true)]  // Exactamente el mínimo
        [InlineData(33, true)]  // Un carácter más del mínimo
        [InlineData(64, true)]  // Doble del mínimo
        [InlineData(128, true)] // Cuádruple del mínimo
        public void IsValid_DeberiaValidarCorrectamentePorLongitud(int length, bool expectedResult)
        {
            // Arrange
            var apiKey = new string('A', length);
            var apiSettings = new ApiSettings { ApiKey = apiKey };

            // Act
            var result = apiSettings.IsValid();

            // Assert
            result.Should().Be(expectedResult);
        }

        #endregion
    }
}