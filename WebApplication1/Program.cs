using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microshaoft;
using Microsoft.AspNetCore.Mvc.ApplicationModels;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddControllers()
//===================================
    .AddNewtonsoftJson();
//===================================

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//=================================================================================================
builder
    .Services
    .Configure
        <KestrelServerOptions>
            (
                (options) =>
                {
                    options
                            .AllowSynchronousIO = true;
                }
            );
var configurationBuilder = new ConfigurationBuilder();
var configuration = configurationBuilder.Build();


#region ConfigurableActionConstrainedRouteApplicationModelProvider
// for both NETCOREAPP2_X and NETCOREAPP3_X
// for Sync or Async Action Selector
builder
        .Services
        .TryAddEnumerable
            (
                ServiceDescriptor
                    .Singleton
                        <
                            IApplicationModelProvider
                            , ConfigurableActionConstrainedRouteApplicationModelProvider
                                                                    <ConstrainedRouteAttribute>
                        >
                    (
                        (x) =>
                        {
                            return
                                new
                                    ConfigurableActionConstrainedRouteApplicationModelProvider
                                            <ConstrainedRouteAttribute>
                                        (
                                            configuration
                                            , (attribute) =>
                                            {
                                                return
                                                    new ConfigurableActionConstraint
                                                                <ConstrainedRouteAttribute>
                                                            (
                                                                attribute
                                                            );
                                            }
                                        );
                        }
                    )
            );
#endregion














//==================================================================================================












//==================================================================================================
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
