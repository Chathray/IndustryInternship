using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Linq;

namespace Idis.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Resolving DI Services
            string connectionString = Configuration.GetConnectionString("MYSQL");
            services.ConfigureDependencyInjection(connectionString);

            // Adds services for controllers
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.ConfigureSwaggerGen();
            // Adds service API versioning to the specified services collection
            services.ConfigureApiVersioning();

            // Enable Cross-Origin Requests (CORS) in ASP.NET Core
            services.AddCors();

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Configure JWT for authentication
            services.ConfigureJwtAuthentication(appSettingsSection);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "idis/{documentName}/docs.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Example);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.List);
                c.EnableDeepLinking();
                c.EnableFilter();
                c.MaxDisplayedTags(5);
                c.ShowExtensions();
                c.ShowCommonExtensions();
                c.EnableValidator();
                c.SupportedSubmitMethods(
                      SubmitMethod.Get
                    , SubmitMethod.Post
                    , SubmitMethod.Put
                    , SubmitMethod.Delete);
                c.UseRequestInterceptor("(request) => { return request; }");
                c.UseResponseInterceptor("(response) => { return response; }");

                c.SwaggerEndpoint("/idis/v2/docs.json", "Idis OpenAPI Version 2");
                c.SwaggerEndpoint("/idis/v1/docs.json", "Idis OpenAPI Version 1");

                c.RoutePrefix = "swagger";
                c.DocumentTitle = "Industry Internship Open API";

                c.IndexStream = () => GetType().Assembly.GetManifestResourceStream("Idis.WebApi.Swagger.docs.html");
            });

            app.UseReDoc(c =>
            {
                c.RoutePrefix = "redoc";
                c.SpecUrl = "/idis/v2/docs.json";
                c.DocumentTitle = "Idis REST API Specification";

                c.EnableUntrustedSpec();
                c.ScrollYOffset(10);
                c.HideHostname();
                c.ExpandResponses("200,201");
                c.RequiredPropsFirst();
                c.PathInMiddlePanel();
                c.NativeScrollbars();
                c.DisableSearch();
                c.OnlyRequiredInSamples();
                c.SortPropsAlphabetically();
            });

            app.UseRouting();

            // Enable request logging
            app.UseSerilogRequestLogging();

            // Global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
