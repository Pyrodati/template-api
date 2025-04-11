using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Template.Infrastructure.Authentification;

namespace Template.Api.OptionsSetup;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    private const string SecretKeyEnvVariable = "TEMPLATE_JWT_SECRET_KEY";
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostEnvironment;
    public JwtOptionsSetup(IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);

        if (_hostEnvironment.IsProduction())
        {
            var secretKeyFromEnv = Environment.GetEnvironmentVariable(SecretKeyEnvVariable, EnvironmentVariableTarget.Machine);
            options.SecretKey = !string.IsNullOrEmpty(secretKeyFromEnv) ? secretKeyFromEnv : string.Empty;
        }

        if (string.IsNullOrWhiteSpace(options.SecretKey))
        {
            throw new InvalidOperationException("SecretKey must be defined and cannot be empty.");
        }
    }
}
