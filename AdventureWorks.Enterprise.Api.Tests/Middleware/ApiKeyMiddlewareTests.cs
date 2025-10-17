using AdventureWorks.Enterprise.Api.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AdventureWorks.Enterprise.Api.Tests.Middleware
{
    public class ApiKeyMiddlewareTests
    {
        private readonly Mock<ILogger<ApiKeyMiddleware>> _mockLogger;
        private readonly ApiKeyMiddleware _middleware;
        private readonly Mock<RequestDelegate> _mockNext;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly DefaultHttpContext _context;

        public ApiKeyMiddlewareTests()
        {
            _mockLogger = new Mock<ILogger<ApiKeyMiddleware>>();
            _mockNext = new Mock<RequestDelegate>();
            _mockConfig = new Mock<IConfiguration>();
            _context = new DefaultHttpContext();
            _middleware = new ApiKeyMiddleware(_mockNext.Object, _mockConfig.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task InvokeAsync_ValidApiKey_AllowsRequest()
        {
            _mockConfig.Setup(c => c["ApiSettings:ApiKey"]).Returns("valid-key");
            _context.Request.Headers["X-API-Key"] = "valid-key";
            _context.Request.Path = "/api/test";

            await _middleware.InvokeAsync(_context);

            _mockNext.Verify(n => n(_context), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_MissingApiKey_ReturnsUnauthorized()
        {
            _mockConfig.Setup(c => c["ApiSettings:ApiKey"]).Returns("valid-key");
            _context.Request.Path = "/api/test";

            await _middleware.InvokeAsync(_context);

            Assert.Equal(401, _context.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_InvalidApiKey_ReturnsUnauthorized()
        {
            _mockConfig.Setup(c => c["ApiSettings:ApiKey"]).Returns("valid-key");
            _context.Request.Headers["X-API-Key"] = "invalid-key";
            _context.Request.Path = "/api/test";

            await _middleware.InvokeAsync(_context);

            Assert.Equal(401, _context.Response.StatusCode);
        }
    }
}
