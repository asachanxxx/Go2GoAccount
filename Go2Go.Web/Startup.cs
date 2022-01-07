using Go2Go.Core;
using Go2Go.Core.Extentions;
using Go2Go.Core.Genarators;
using Go2Go.Core.Validation;
using Go2Go.Data.Context;
using Go2Go.Implementations.Repositories;
using Go2Go.Implementations.Services;
using Go2Go.Web.Authentication;
using Go2Go.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Web
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
            services.AddControllersWithViews();
            services.AddAuthentication(authOption =>
            {
                authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                var key = Configuration.GetValue<string>("jwtconfig:key");
                var keyBytes = Encoding.ASCII.GetBytes(key);

                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false

                };
            });
            //https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware?view=aspnetcore-6.0
            services.AddResponseCaching();
            //https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core
            services.AddApplicationInsightsTelemetry();
            services.AddSingleton(typeof(IJwtTokenManager), typeof(JwtTokenManager));
            services.AddSwaggerGen();
            services.AddDbContext<Go2GoContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("StuConn")));

            services.AddScoped<EfCoreUserRepository>();
            services.AddScoped<EfCoreTripRepository>();
            services.AddScoped<EfCoreTripPaymentRepository>();
            services.AddScoped<EfCoreUserLedgerRepository>();
            services.AddScoped<EfCoreUserPaymentRepository>();

            services.AddScoped<ILogicalCalculations, LogicalCalculations>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<Go2GoContext>();

            services.Configure<CompanySettings>(Configuration.GetSection("CompanySettings"));
            services.AddSingleton<ISerialGenarator, SerialGenarator>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;
                var status = exception.GetStatusCodeByException();
                var url = context.Request.GetDisplayUrl();
                var user = context.User?.GetUserName();
                var ip = context.Connection.RemoteIpAddress; //context.User?.GetClientIP();
                context.Response.StatusCode = (int)status;

                if (status >= HttpStatusCode.InternalServerError)
                {
                    logger.LogError(exception, "Unhandled Error (HttpStatus: {HttpStatus}) when calling {Url} for user {User}. ClientIP: {ClientIP}", status, url, user, ip);
                }
                else
                {
                    logger.LogWarning("Message: {Message} HttpStatus: {HttpStatus} when calling {Url} for user {User}. ClientIP: {ClientIP}", exception.Message, status, url, user, ip);
                }

                if (context.Request.IsAjaxRequest() || context.Request.IsApiCall())
                {
                    var settings = new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeHtml };
                    string result;
                    if (exception is MultiValidationException mve)
                    {
                        result = JsonConvert.SerializeObject(new { Message = mve.Message, MultiValidationResults = mve.MultiValidationResults }, settings);
                    }
                    else
                    {
                        result = JsonConvert.SerializeObject(exception.Message, settings);
                    }
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }
                else
                {
                    context.Response.Redirect("/Home/Error");
                }
            }));

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
