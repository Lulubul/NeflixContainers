
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;

namespace ApiGw_Base
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _cfg = configuration;
        }

        public IConfiguration _cfg { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var identityUrl = _cfg.GetValue<string>("IdentityUrl");
            var authenticationProviderKey = "IdentityApiKey";

            services.AddHealthChecks()
               .AddCheck("self", () => HealthCheckResult.Healthy());
            /* .AddUrlGroup(new Uri(_cfg["ProfileUrlHC"]), name: "profileapi-check", tags: new string[] { "profileapi" })
              .AddUrlGroup(new Uri(_cfg["HistoryUrlHC"]), name: "historyapi-check", tags: new string[] { "historyapi" })
              .AddUrlGroup(new Uri(_cfg["MarketingUrlHC"]), name: "marketingapi-check", tags: new string[] { "marketingapi" })
              .AddUrlGroup(new Uri(_cfg["MoviemetadataUrlHC"]), name: "moviemetadataapi-check", tags: new string[] { "moviemetadataapi" })
              .AddUrlGroup(new Uri(_cfg["RecommendationUrlHC"]), name: "recommendationapi-check", tags: new string[] { "recommendationapi" })
              .AddUrlGroup(new Uri(_cfg["IdentityUrlHC"]), name: "identityapi-check", tags: new string[] { "identityapi" });
              */

            services.AddAuthentication()
               .AddJwtBearer(authenticationProviderKey, x =>
               {
                   x.Authority = identityUrl;
                   x.RequireHttpsMetadata = false;
                   x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidAudiences = new[] { "profiles", "history", "marketing", "moviemetadata", "recommendation" }
                   };
               });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });

            services.AddOcelot(_cfg);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

            app.UseCors("CorsPolicy");
            await app.UseOcelot();
        }
    }
}
