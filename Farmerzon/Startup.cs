using System.Text;
using FarmerzonDataAccess;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Implementations;
using FarmerzonDataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Farmerzon
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
            services.AddCors(c => 
            {
                c.AddPolicy(allowOrigins,
                    options =>
                    {
                        options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                    });
            });
            
            // serialization for GraphQL error responses was not able. The following solution was found on stackoverflow
            // under the following url: https://stackoverflow.com/questions/59199593/net-core-3-0-possible-object-cycle
            // -was-detected-which-is-not-supported
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            
            services.AddControllers();

            services.AddDbContextPool<FarmerzonContext>(
                option => option.UseNpgsql(
                    Configuration.GetConnectionString("Farmerzon"),
                    x => x.MigrationsAssembly(nameof(Farmerzon))));
            
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
            
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<Query, Query>();
            services.AddScoped<Mutation, Mutation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FarmerzonContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(allowOrigins);

            // It is important to use app.UseAuthentication(); before app.UseAuthorization();
            // Otherwise authentication with json web tokens doesn't work.
            app.UseAuthentication();
            app.UseAuthorization();

            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            context.Database.Migrate();
        }
    }
}