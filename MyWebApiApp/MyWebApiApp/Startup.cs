using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyWebApiApp.HttpClients;
using MyWebApiApp.Middleware;
using MyWebApiApp.Options;

namespace MyWebApiApp
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CustomExceptionHandler>();
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<SwapiClientOptions>(Configuration.GetSection(nameof(ApplicationOptions.SwapiClient)));

            services.AddHttpClient<ISwapiClient, SwapiClient>()
            .ConfigureHttpClient((serviceProvider, options) =>
            {
                var swapiClientSettings = serviceProvider.GetRequiredService<IOptions<SwapiClientOptions>>().Value;
                options.BaseAddress = swapiClientSettings.BaseUrl;
            });

            services.AddTransient<ThrowsBeforeNext>();
            services.AddTransient<ThrowsAfterNext>();

            services.AddSwaggerGen(options =>
            {
                var swaggerGenSettings = Configuration
                                            .GetSection(nameof(ApplicationOptions.SwaggerGen))
                                            .Get<SwaggerGenConfigurationOptions>();
                options.SwaggerDoc(swaggerGenSettings.Name, swaggerGenSettings.ApiInfo);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCustomExceptionHandlerMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var swaggerGenSettings = Configuration
                            .GetSection(nameof(ApplicationOptions.SwaggerGen))
                            .Get<SwaggerGenConfigurationOptions>();
                options.SwaggerEndpoint($"{swaggerGenSettings.Name}/swagger.json", "My API V1");
                // options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            // app.UseThrowsBeforeNextMiddleware();
            // app.UseThrowsAfterNextMiddleware();

            app.UseMvc();
        }
    }
}
