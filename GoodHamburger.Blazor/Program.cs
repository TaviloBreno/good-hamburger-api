using GoodHamburger.Blazor;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var apiBaseUrl = ResolveApiBaseUrl(builder.Configuration, builder.Environment.IsDevelopment());

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

static string ResolveApiBaseUrl(IConfiguration configuration, bool isDevelopment)
{
    var candidates = new List<string>();

    var configuredSingle = configuration["ApiBaseUrl"];
    if (!string.IsNullOrWhiteSpace(configuredSingle))
        candidates.Add(configuredSingle);

    var configuredMany = configuration.GetSection("ApiBaseUrls").Get<string[]>() ?? Array.Empty<string>();
    foreach (var candidate in configuredMany)
    {
        if (!string.IsNullOrWhiteSpace(candidate))
            candidates.Add(candidate);
    }

    candidates.Add("https://localhost:7132");
    candidates.Add("http://localhost:5156");

    var uniqueCandidates = candidates
        .Select(NormalizeBaseUrl)
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToList();

    foreach (var candidate in uniqueCandidates)
    {
        if (IsApiReachable(candidate, isDevelopment))
        {
            Console.WriteLine($"[Blazor] API resolvida para: {candidate}");
            return candidate;
        }
    }

    var fallback = uniqueCandidates.FirstOrDefault() ?? "http://localhost:5156/";
    Console.WriteLine($"[Blazor] Nenhuma API alcancavel no startup. Usando fallback: {fallback}");
    return fallback;
}

static bool IsApiReachable(string baseUrl, bool isDevelopment)
{
    try
    {
        using var handler = new HttpClientHandler();

        if (isDevelopment)
        {
            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
            {
                if (request?.RequestUri?.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase) == true)
                    return true;

                return errors == System.Net.Security.SslPolicyErrors.None;
            };
        }

        using var client = new HttpClient(handler)
        {
            BaseAddress = new Uri(baseUrl),
            Timeout = TimeSpan.FromSeconds(2)
        };

        var healthResponse = client.GetAsync("health").GetAwaiter().GetResult();
        if (healthResponse.IsSuccessStatusCode)
            return true;

        var menuResponse = client.GetAsync("api/menu").GetAwaiter().GetResult();
        return menuResponse.StatusCode != HttpStatusCode.InternalServerError;
    }
    catch
    {
        return false;
    }
}

static string NormalizeBaseUrl(string baseUrl)
{
    var trimmed = baseUrl.Trim();
    return trimmed.EndsWith('/') ? trimmed : $"{trimmed}/";
}