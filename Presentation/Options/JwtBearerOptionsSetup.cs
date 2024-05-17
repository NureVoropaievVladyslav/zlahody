using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Options;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IConfiguration _configuration;

    public JwtBearerOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.Authority = _configuration["JwtBearerOptions:ValidIssuer"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = _configuration["JwtBearerOptions:ValidIssuer"],
            ValidAudience = _configuration["JwtBearerOptions:ValidAudience"]
        };
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        options.Authority = _configuration["JwtBearerOptions:ValidIssuer"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = _configuration["JwtBearerOptions:ValidIssuer"],
            ValidAudience = _configuration["JwtBearerOptions:ValidAudience"]
        };
    }
}