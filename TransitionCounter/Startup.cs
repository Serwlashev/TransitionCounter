using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransitionCounter.Services;
using TransitionCounter.Services.Interfaces;

namespace TransitionCounter
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
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSingleton<ISessionService, SessionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("<div><a href=\"home\">Home </a><a href=\"about\">About </a><a href=\"contacts\">Contacts</a></div>");
                });
            });

            app.Map("/home", (home) =>
            {
                home.Run(async (ctx) =>
                {
                    await ctx.Response.WriteAsync("<h1>home</h1></br><div><a href=\"about\">About </a><a href=\"contacts\">Contacts</a></div>");
                });
            });

            app.Map("/about", (home) =>
            {
                home.Run(async (ctx) =>
                {
                    await ctx.Response.WriteAsync("<h1>about</h1></br><div><a href=\"home\">Home </a><a href=\"contacts\">Contacts</a></div>");
                });
            });

            app.Map("/contacts", (home) =>
            {
                home.Run(async (ctx) =>
                {
                    await ctx.Response.WriteAsync("<h1>contacts</h1></br><div><a href=\"home\">Home </a><a href=\"about\">About</a></div>");
                });
            });
        }
    }
}
