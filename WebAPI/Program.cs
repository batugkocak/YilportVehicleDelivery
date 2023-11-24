using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;

var MyAllowSpecificOrigins = "_myFrontEnd";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*").AllowAnyHeader().AllowAnyOrigin();
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
new CoreModule()
});

// builder.Services.AddSingleton<IVehicleService, VehicleManager>();
// builder.Services.AddSingleton<IVehicleDal, EfVehicleDal>();
//
// builder.Services.AddSingleton<IVehicleOnTaskService, VehicleOnTaskManager>();
// builder.Services.AddSingleton<IVehicleOnTaskDal, EfVehicleOnTaskDal>();
//
// builder.Services.AddSingleton<IBrandService, BrandManager>();
// builder.Services.AddSingleton<IBrandDal, EfBrandDal>();
//
// builder.Services.AddSingleton<IDriverService, DriverManager>();
// builder.Services.AddSingleton<IDriverDal, EfDriverDal>();
//
// builder.Services.AddSingleton<ITaskService, TaskManager>();
// builder.Services.AddSingleton<ITaskDal, EfTaskDal>();
//
// builder.Services.AddSingleton<IDepartmentService, DepartmentManager>();
// builder.Services.AddSingleton<IDepartmentDal, EfDepartmentDal>();
//
// builder.Services.AddSingleton<IOwnerService, OwnerManager>();
// builder.Services.AddSingleton<IOwnerDal, EfOwnerDal>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(options =>
    options.RegisterModule(new AutofacBusinessModule())
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* 
app.UseExceptionHandler(async options =>
{
    options.Run(async handler =>
    {
        handler.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        handler.Response.ContentType = "text/html";
        var exceptionObject = handler.Features.Get<IExceptionHandlerFeature>();
        if (null != exceptionObject)
        {
            var errorMessage = $"{exceptionObject.Error.Message}";
            await handler.Response.WriteAsync(errorMessage).ConfigureAwait(false);
        }
    });
});
*/

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();