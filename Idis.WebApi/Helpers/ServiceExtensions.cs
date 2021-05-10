using Idis.Application;
using Idis.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idis.WebApi
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.DocumentFilter<TagDescriptionsFilter>();
                setup.SchemaFilter<ExamplesSchemaFilter>();
                
                setup.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Industry Internship OpenAPI",
                    Version = "v2",
                    Description = "<p>This is a sample server Idis server.  You can find out more about Swagger at <a rel='noopener noreferrer' target='_blank' href='http://swagger.io'>http://swagger.io</a> or on <a rel='noopener noreferrer' target='_blank' href='http://swagger.io/irc/'>irc.freenode.net, #swagger</a>.  For this sample, you can use the api key <code>special-key</code> to test the authorization filters.</p>",
                    TermsOfService = new Uri("http://tempuri.org/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "",
                        Email = "chithachnguyen@outlook.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });

                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Industry Internship OpenAPI",
                    Version = "v1",
                    Description = "An API to perform Industry Internship operations",
                });

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat ="JWT"
                });

                setup.DescribeAllParametersInCamelCase();
                

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            BearerFormat ="JWT",
                            In = ParameterLocation.Header,
                            Scheme = "Bearer",                            
                        },
                        new List<string>()
                    }
                });

                //setup.DocInclusionPredicate((v_doc, api_desc) =>
                //{
                //    if (!api_desc.TryGetMethodInfo(out MethodInfo methodInfo))
                //        return false;

                //    var versions = methodInfo.DeclaringType
                //        .GetCustomAttributes(true)
                //        .OfType<ApiVersionAttribute>()
                //        .SelectMany(attr => attr.Versions);

                //    return versions.Any(v => $"v{v}" == v_doc);
                //});

                //setup.OrderActionsBy((apiDesc) =>
                //$"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
            });

            services.Configure<SwaggerOptions>(c => c.SerializeAsV2 = true);
        }

        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.UseApiBehavior = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
                options.DefaultApiVersion = new ApiVersion(2, 0);
            });
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfigurationSection appSettingsSection)
        {
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetOne(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services, string connectionString)
        {
            // CR:Add database context of webapp
            services.AddDbContext<DataContext>(options => options.UseMySQL(connectionString));

            // configure DI for application services
            services.AddAutoMapper(typeof(Startup)); // Add AutoMapper

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IInternRepository, InternRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IPointRepository, PointRepository>();

            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IInternService, InternService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventTypeService, EventTypeService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IPointService, PointService>();

            services.AddSingleton<IOriginService, OriginService>();
        }
    }
}
