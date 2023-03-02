using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TestJwt.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var configuration = builder.Configuration;

var authUserSettingsConfiguration = configuration.GetSection(nameof(AuthUserSettings));
var authUserSettings = authUserSettingsConfiguration.Get<AuthUserSettings>()!;
services.Configure<AuthUserSettings>(authUserSettingsConfiguration);

var authApplicationSettingsConfiguration = configuration.GetSection(nameof(AuthApplicationSettings));
var authApplicationSettings = authApplicationSettingsConfiguration.Get<AuthApplicationSettings>()!;
services.Configure<AuthApplicationSettings>(authApplicationSettingsConfiguration);

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("User",o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authUserSettings.Issuer,
        ValidAudience = authUserSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authUserSettings.Key)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
}).AddJwtBearer("Application",o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authApplicationSettings.Issuer,
        ValidAudience = authApplicationSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authApplicationSettings.Key)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
services.AddAuthentication();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
