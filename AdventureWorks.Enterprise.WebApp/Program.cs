using AdventureWorks.Enterprise.WebApp.Components;
using AdventureWorks.Enterprise.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure HttpClient for API communication
builder.Services.AddHttpClient<ApiService>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetService<IConfiguration>() ?? builder.Configuration;
    var logger = serviceProvider.GetService<ILogger<Program>>();
    
    try
    {
        // Obtener la URL base de la configuraci�n
        var baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        
        if (string.IsNullOrEmpty(baseUrl))
        {
            // URL por defecto si no est� configurada
            baseUrl = "https://localhost:7001";
            logger?.LogWarning("?? ApiSettings:BaseUrl no configurada, usando URL por defecto: {BaseUrl}", baseUrl);
        }
        else
        {
            logger?.LogInformation("?? ApiSettings:BaseUrl obtenida de configuraci�n: {BaseUrl}", baseUrl);
        }
        
        // Asegurar que la URL termine con '/'
        if (!baseUrl.EndsWith('/'))
        {
            baseUrl += '/';
        }
        
        // Validar que la URL sea v�lida
        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri))
        {
            throw new InvalidOperationException($"La URL base '{baseUrl}' no es v�lida");
        }
        
        client.BaseAddress = uri;
        logger?.LogInformation("? BaseAddress configurada correctamente: {BaseAddress}", client.BaseAddress);
        
        client.Timeout = TimeSpan.FromSeconds(30);
        
        // Add default headers
        client.DefaultRequestHeaders.Add("User-Agent", "AdventureWorks-WebApp/1.0");
        
        // Agregar API Key desde configuraci�n
        var apiKey = configuration.GetValue<string>("ApiSettings:ApiKey");
        if (!string.IsNullOrEmpty(apiKey))
        {
            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
            logger?.LogInformation("?? API Key agregada a headers correctamente");
        }
        else
        {
            logger?.LogWarning("?? API Key no encontrada en configuraci�n - ApiSettings:ApiKey");
        }
        
        logger?.LogInformation("?? HttpClient configurado exitosamente para ApiService");
    }
    catch (Exception ex)
    {
        logger?.LogError(ex, "? Error cr�tico configurando HttpClient para ApiService");
        throw new InvalidOperationException($"Error configurando HttpClient: {ex.Message}", ex);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Log configuration at startup with comprehensive diagnostics
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    
    try
    {
        logger.LogInformation("?? === DIAGN�STICO DE CONFIGURACI�N ===");
        
        // Verificar configuraci�n b�sica
        var baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        var apiKey = configuration.GetValue<string>("ApiSettings:ApiKey");
        
        logger.LogInformation("?? API Base URL: {BaseUrl}", baseUrl ?? "? NO CONFIGURADA");
        logger.LogInformation("?? API Key: {HasApiKey} (Length: {Length})", 
            !string.IsNullOrEmpty(apiKey) ? "? Configurada" : "? NO CONFIGURADA", 
            apiKey?.Length ?? 0);
        
        // Test HttpClient configuration
        logger.LogInformation("?? Probando configuraci�n de ApiService...");
        var apiService = scope.ServiceProvider.GetRequiredService<ApiService>();
        logger.LogInformation("? ApiService registrado e inyectado correctamente");
        
        // Test de conectividad b�sica (opcional)
        try
        {
            logger.LogInformation("?? Intentando ping a API...");
            var pingResult = await apiService.PingAsync();
            logger.LogInformation("?? Resultado del ping: {Result}", pingResult ? "? �xito" : "? Error");
        }
        catch (Exception pingEx)
        {
            logger.LogWarning(pingEx, "?? No se pudo hacer ping a la API (puede estar apagada)");
        }
        
        logger.LogInformation("?? Configuraci�n validada correctamente");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "? Error cr�tico en validaci�n de configuraci�n");
        
        // Log informaci�n adicional para debugging
        logger.LogError("?? Environment: {Environment}", app.Environment.EnvironmentName);
        logger.LogError("?? Content Root: {ContentRoot}", app.Environment.ContentRootPath);
        logger.LogError("?? App URLs: {Urls}", string.Join(", ", app.Urls));
        
        // No lanzar excepci�n para permitir que la app inicie, pero log el error
    }
}

app.Run();
