using AutoMapper;
using Cryptography;
using TODO.Configuration;
using JwtManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using TODO.Data.Account;
using TODO.Business.Account;
using TODO.Data.User;
using TODO.Business.User;
using MongoDB.Data.Generic;
using Mapper;
using TODO.Business.Todo;
using TODO.Data.Todo;

namespace TODO.WebApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        #region Configure services
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCookiePolicy(services);
            ConfigureDependencyInjection(services);
            ConfigureIdentity(services);
            ConfigureJwt(services);
            ConfigureCors(services);
            ConfigureLocalization(services);
            ConfigureVersioning(services);
            services.ConfigureConfigurationOptions(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // In production, the Angular files will be served from this directory         
        }

        /// <summary>Configures the JWT settings.</summary>
        /// <param name="services">The services.</param>
        private void ConfigureJwt(IServiceCollection services)
        {
            var jwtSettings = Configuration.GetSection("JwtAuthorizationSettings");
            services.AddAuthentication().AddCookie(config =>
                {
                    config.SlidingExpiration = true;
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(jwtSettings["ExpirationTimeInMinute"]));
                    //config.CookieSecure = CookieSecurePolicy.Always;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                    };
                });
            services.AddTransient<ITokenGenerator, TokenGenerator>();

            //services.ConfigureJwt(jwtSettings["Issuer"], jwtSettings["Audience"], jwtSettings["SecretKey"]);
        }
        /// <summary>Configures the cookie policy.</summary>
        /// <param name="services">The services.</param>
        private void ConfigureCookiePolicy(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        /// <summary>Configures the dependency injection.</summary>
        /// <param name="services">The services.</param>
        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            #region Cryptography
            services.AddTransient<ICipher, Cipher>();
            #endregion          
            services.AddTransient<IAppConfiguration, AppConfiguration>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            #region Repository
            services.AddSingleton(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
            #endregion
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountBL, AccountBL>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<ITodoBL, TodoBL>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperModelConfiguration());
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>Configures the cors.</summary>
        /// <param name="services">The services.</param>
        public void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
        /// <summary>Configures the identity.</summary>
        /// <param name="services">The services.</param>
        public void ConfigureIdentity(IServiceCollection services)
        {
            #region Identity

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            #endregion           
            #region Session
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration["Authorization:SessionIdleTimeout"]));
                options.Cookie.HttpOnly = true;
            });
            #endregion

            #region Identity options
            services.Configure<IdentityOptions>(configureOptions =>
            {
                // Password settings.
                configureOptions.Password.RequireDigit = true;
                configureOptions.Password.RequiredLength = 8;
                configureOptions.Password.RequireNonAlphanumeric = true;
                configureOptions.Password.RequireUppercase = false;
                configureOptions.Password.RequireLowercase = true;

                // Lockout settings.
                configureOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(Configuration["Authorization:DefaultLockoutTime"]));
                configureOptions.Lockout.MaxFailedAccessAttempts = Convert.ToInt32(Configuration["Authorization:AccessFailedCount"]);
                configureOptions.Lockout.AllowedForNewUsers = true;

                // User settings.            
                configureOptions.User.RequireUniqueEmail = true;
            });
            #endregion
        }

        public void ConfigureLocalization(IServiceCollection services)
        {
            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            //--< set uploadsize large files >----

            services.Configure<FormOptions>(options =>

            {

                options.ValueLengthLimit = int.MaxValue;

                options.MultipartBodyLengthLimit = int.MaxValue;

                options.MultipartHeadersLengthLimit = int.MaxValue;

            });

            //--</ set uploadsize large files >----
        }

        public void ConfigureVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
        #endregion
        #region Configure env
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>Configures the specified application.</summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            //app.ConfigureCustomExceptionMiddleware();
            app.UseAuthentication();

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr-CH"),
                new CultureInfo("nl-BE"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

        }
        #endregion

    }
}
