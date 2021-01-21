using System.IO;
using AutoMapper;
using CrowdFunding.ActionFilters;
using Contracts;
using CrowdFunding.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using CrowdFunding.ActionFiltersz;
using Entities.DataTransferObjects;
using Repository.DataShaping;
using AspNetCoreRateLimit;
using CrowdFunding.Utility;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;
using CrowdFunding.HealthCheck;
using HealthChecks.UI.Client;

namespace CrowdFunding
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                {
                    Duration = 120
                });
            }).AddNewtonsoftJson();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidateEndeavourExistsAttribute>();
            services.AddScoped<IDataShaper<EndeavorDto>, DataShaper<EndeavorDto>>();
            services.AddScoped<ValidateMediaTypeAttribute>();
            services.ConfigureVersioning();
            services.ConfigureResponseCaching();
            services.ConfigureHttpCacheHeaders();
            services.AddMemoryCache();
            services.ConfigureRateLimitingOptions();
            services.AddHttpContextAccessor();
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.ConfigureSwagger();

            var paystackAPIUrl = Configuration.GetSection("Paystack");
            var url = paystackAPIUrl.GetSection("paysStackApiUrl").Value;
            var securityLogFilePath = this.Configuration["SecurityLogFilePath"];

            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString("sqlConnection"), failureStatus: HealthStatus.Unhealthy, tags: new[] { "ready" })
                .AddUrlGroup(new Uri(url), "Paystack API health Check", HealthStatus.Degraded, tags: new[] { "ready" }, timeout: new TimeSpan(0,0,5))
               // .AddCheck("File Path Health Check", new FilePathWriterHealthCheck(securityLogFilePath), HealthStatus.Unhealthy, tags: new[] { "ready" })
                .AddFilePathWrite(securityLogFilePath, HealthStatus.Unhealthy, tags: new[] { "ready" });

                services.AddHealthChecksUI().AddInMemoryStorage(); 
       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseIpRateLimiting();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    ResultStatusCodes = {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                    },
                    ResponseWriter = WriteHealthCheckResponse,
                    Predicate = (check) => check.Tags.Contains("ready"),
                    AllowCachingResponses = false

                });
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions() {
                    Predicate = (check) => !check.Tags.Contains("live"),
                    ResponseWriter = WriteHealthCheckLiveResponse,
                    AllowCachingResponses = false
                });

                endpoints.MapHealthChecks("/healthui", new HealthCheckOptions() { 
                   Predicate = _ => true,
                   ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
            app.UseHealthChecksUI();
            app.UseSwagger();
            app.UseSwaggerUI(s => {
              s.SwaggerEndpoint("/swagger/v1/swagger.json", "Crowd Funding API v1");
              s.SwaggerEndpoint("/swagger/v2/swagger.json", "Crowd Funding API v2");
            });
        }

        private Task WriteHealthCheckLiveResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";
            var json = new JObject(
                new JProperty("OverallStatus", result.Status.ToString()),
                new JProperty("TotalChecksdDuration", result.TotalDuration.TotalSeconds.ToString("0:0:00")));
            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }

        private Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";
            var json = new JObject(
                new JProperty("OverallStatus", result.Status.ToString()),
                new JProperty("TotalChecksdDuration", result.TotalDuration.TotalSeconds.ToString("0:0:00")),
                new JProperty("DependencyHealthChecks", new JObject(result.Entries.Select(dicItem =>
                    new JProperty(dicItem.Key, new JObject(
                        new JProperty("Status", dicItem.Value.Status.ToString()),
                        new JProperty("Duration", dicItem.Value.Duration.TotalSeconds.ToString("0:0:00")),
                        new JProperty("Exception", dicItem.Value.Exception?.Message),
                        new JProperty("Data", new JObject(dicItem.Value.Data.Select(dicData => new JProperty(dicData.Key,dicData.Value))))
                        ))
                    )))
              );

            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }


    }
}
