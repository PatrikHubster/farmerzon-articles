using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using AutoMapper;
using FarmerzonArticles.Helper;
using FarmerzonArticlesDataAccess;
using FarmerzonArticlesDataAccess.Implementation;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesErrorHandling;
using FarmerzonArticlesManager.Implementation;
using FarmerzonArticlesManager.Interface;
using FarmerzonArticlesManager.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FarmerzonArticles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string allowOrigins = "allowOrigins";
        
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/
        // ?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddDapr();
            
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
            
            // Disable default model validation like it is described under the following link
            // https://www.talkingdotnet.com/disable-automatic-model-state-validation-in-asp-net-core-2-1/
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            // Add a custom model validation like it is described under the following link 
            // https://www.talkingdotnet.com/validate-model-state-automatically-asp-net-core-2-0/
            services.AddMvc(options => { options.Filters.Add(typeof(ValidateModelStateAttribute)); });

            services.AddDbContext<FarmerzonArticlesContext>(
                option => option.UseNpgsql(
                    Configuration.GetConnectionString("FarmerzonArticles"),
                    x => x.MigrationsAssembly(nameof(FarmerzonArticles))));
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]))
                };
            });
            
            // Adds the mapper for the business logic
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

            // repositories DI container
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ITransactionHandler, TransactionHandler>();
            
            // manager DI container
            services.AddScoped<IArticleManager, ArticleManager>();
            services.AddScoped<IPersonManager, PersonManager>();
            services.AddScoped<IUnitManager, UnitManager>();
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Farmerzon Articles API", Version = "v1"});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // The configuring of the authentication in swagger ui changed in dotnet core 3.1. For more details:
                // https://stackoverflow.com/questions/58179180/jwt-authentication-and-swagger-with-net-core-3-0
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.Enter 'Bearer' [space] and then " +
                                  "your token in the text input below. Example: 'Bearer header.payload.signature'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FarmerzonArticlesContext context)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farmerzon Articles API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            }

            app.UseRouting();
            app.UseCloudEvents();

            // It is important to use app.UseAuthentication(); before app.UseAuthorization();
            // Otherwise authentication with json web tokens doesn't work.
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers();
            });
            
            context.Database.Migrate();
        }
    }
}
