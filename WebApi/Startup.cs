using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DataServiceProvider.Services;
using Newtonsoft.Json.Serialization;
using AuthenticationLibrary.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using WebApi.Helpers;
using WebApi.Midlleware;
using WebApi.Observability;

namespace WebApi
{
  internal class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      Configuration = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

      //configuring serilog and replacing the built-in one.
      Log.Logger = new LoggerConfiguration()
        .ReadFrom
        .Configuration(configuration)
        .CreateLogger();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<ErrorHandlingHelper>();
      services.AddDataAccessServiceInternals();
      services.AddAuthenticationLibraryInternals();
      services.AddControllers();
      services.AddHealthChecks();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
      });
      services.AddMvc(
        options =>
        {
          // Refer to this article for more details on how to properly set the caching for your needs
          // https://docs.microsoft.com/en-us/aspnet/core/performance/caching/response
          options.CacheProfiles.Add(
            "default",
            new CacheProfile
            {
              Duration = 600,
              Location = ResponseCacheLocation.None
            });
        }).AddNewtonsoftJson(options =>
            {
              options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
              options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }); 
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
      }
      
      app.UseMiddleware<RequestValidationMiddleware>();

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();
 

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
        {
          AllowCachingResponses = false,
          Predicate = (check) => check.Tags.Contains("ready"),
          ResponseWriter = HealthReportWriter.WriteResponse
        });

        endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
        {
          AllowCachingResponses = false,
          Predicate = (check) => check.Tags.Contains("live"),
          ResponseWriter = HealthReportWriter.WriteResponse
        });
      });


    }
  }
}
