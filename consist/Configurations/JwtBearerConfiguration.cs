using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class JwtBearerConfiguration
{
    public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder, string jwtPrivateKey)
    {
        return builder.AddJwtBearer(options =>
        {

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(jwtPrivateKey)),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false

            };
        });
    }
}