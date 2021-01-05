using LyricsCollector.Context;
using LyricsCollector.SpotifyClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

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

            services.AddDbContext<LyricsCollectorDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<LyricsCollectorDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
            });


            //services.AddScoped<SpotifyAuthenticationExtensions>
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("spotify", policy =>
            //    {
            //        policy.AuthenticationSchemes.Add("Spotify");
            //        policy.RequireAuthenticatedUser();
            //    });
            //});
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //    .AddCookie(options =>
            //    {
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
            //    })
            //    .AddSpotify(options =>
            //    {
            //        options.ClientId = Configuration["SpotifyClientId"];
            //        options.ClientSecret = Configuration["SpotifyClientSecret"];
            //        options.CallbackPath = "";
            //        options.SaveTokens = true;

            //        var scopes = new List<string> {

            //            UserReadEmail, UserReadPrivate, PlaylistReadPrivate, PlaylistReadCollaborative
            //        };
            //        options.Scope.Add(string.Join(",", scopes));
            //    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LyricsCollector", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddHttpClient("lyrics", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("LyricsAPI"));
            });
            services.AddHttpClient("spotify", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("SpotifyAPI"));
                //c.DefaultRequestHeaders ??
            });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
