using LyricsCollector.Context;
using LyricsCollector.Models;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.ConcreteServices;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace LyricsCollector
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMemoryCache();

            services.AddCors(o => o.AddPolicy("CORSPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            ));

            services.AddDbContext<LyricsCollectorDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            var jwtSection = Configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(jwtSection);
            var appSettings = jwtSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = true;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LyricsCollector", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddHttpClient("lyrics", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("LyricsAPI"));
                c.DefaultRequestHeaders.Add("Authorization", "Bearer");
            });
            services.AddHttpClient("spotify", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("SpotifyAPI"));
            });


            services.AddTransient<ILyricsService, LyricsService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISpotifyService, SpotifyService>();

            services.AddHttpContextAccessor();
            
            //services.AddSingleton(SpotifyClientConfig.CreateDefault());
            //services.AddScoped<SpotifyClientBuilder>();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Spotify", policy =>
            //    {
            //        policy.AuthenticationSchemes.Add("Spotify");
            //        policy.RequireAuthenticatedUser();
            //    });
            //});
            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    })
            //    .AddCookie(options =>
            //    {
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
            //    })
            //    .AddSpotify(options =>
            //    {
            //        options.ClientId = Configuration.GetValue<string>("SpotifyClientId");
            //        options.ClientSecret = Configuration.GetValue<string>("SpotifyClientSecret");
            //        options.CallbackPath = "/Auth/callback";
            //        options.SaveTokens = true;
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LyricsCollector v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORSPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
