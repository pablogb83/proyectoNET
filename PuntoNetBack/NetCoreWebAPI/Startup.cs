using BusinessLayer.BL;
using BusinessLayer.IBL;
using DataAccessLayer;
using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCoreWebAPI.Helpers;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreWebAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        //public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddHttpClient();
            //services.AddScoped<TenantInfo>();
            //services.UseDiscriminatorColumn(Configuration);

            //services.AddMultiTenant<TenantInfo>().WithStaticStrategy("1").WithEFCoreStore<MultiTenantStoreDbContext>();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.Configure<AppSettings>(appSettingsSection);
            services.AddMultiTenant<Institucion>().WithDelegateStrategy(async context =>
            {
                var httpContext = context as HttpContext;
                var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                string tenantId = new Util().decodeToken(httpContext, token, key);
                return tenantId;
            }).WithEFCoreStore<MultiTenantStoreDbContext, Institucion>();
            services.AddDbContext<WebAPIContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("CommanderConnection")));
            //services.AddDefaultIdentity<Usuario>().AddRoles<Role>();
            // configure strongly typed settings objects

            IdentityBuilder builder = services.AddIdentityCore<Usuario>();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<WebAPIContext>();
            builder.AddDefaultTokenProviders();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<Usuario>>();

            services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                          .GetBytes(Configuration.GetSection("AppSettings:Secret").Value)),
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero
                  };
              });

            services.Configure<IdentityOptions>(options =>
            {
                // User settings.
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";

                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 9;

                // Lockout settings.
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });


            /*services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();*/

            // configure jwt authentication

            /*services.AddAuthentication(x =>
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
                        var userService = context.HttpContext.RequestServices.GetRequiredService <IDAL_Usuario>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetUsuarioById(userId);
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
            });*/

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Inyeccion de dependencias
            services.AddScoped<IDAL_Usuario, DataAccessLayer.DAL.DAL_Usuario_EF>();
            services.AddScoped<IBL_Usuario, BL_Usuario>();
            services.AddScoped<IDAL_Institucion, DataAccessLayer.DAL.DAL_Institucion_EF>();
            services.AddScoped<IBL_Institucion, BL_Institucion>();
            services.AddScoped<IDAL_Registro, DataAccessLayer.DAL.DAL_Registro_EF>();
            services.AddScoped<IBL_Registro, BL_Registro>();
            services.AddScoped<IDAL_Role, DataAccessLayer.DAL.DAL_Role_EF>();
            services.AddScoped<IBL_Role, BL_Role>();
            services.AddScoped<IDAL_Edificio, DataAccessLayer.DAL.DAL_Edificio_EF>();
            services.AddScoped<IBL_Edificio, BL_Edificio>();
            services.AddScoped<IDAL_Salon, DataAccessLayer.DAL.DAL_Salon_EF>();
            services.AddScoped<IBL_Salon, BL_Salon>();
            services.AddScoped<IDAL_Puerta, DataAccessLayer.DAL.DAL_Puerta_EF>();
            services.AddScoped<IBL_Puerta, BL_Puerta>();
            services.AddScoped<IDAL_UsuarioEdificio, DataAccessLayer.DAL.DAL_UsuarioEdificio>();
            services.AddScoped<IBL_UsuarioEdificio, BL_UsuarioEdificio>();
            services.AddScoped<IDAL_Producto, DataAccessLayer.DAL.DAL_Producto>();
            services.AddScoped<IBL_Producto, BL_Producto>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetCoreWebAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCoreWebAPI v1"));
            }

            //app.UseMiddleware<JwtMiddleware>();

            app.UseMultiTenant();

            app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
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
