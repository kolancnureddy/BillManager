﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using BillManager.Entities;
using Microsoft.EntityFrameworkCore;
using BillManager.Services;
using BillManager.BusinessLogic;

namespace BillManager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.RespectBrowserAcceptHeader = true;
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                });
            String connectionString = @"Server=(localdb)\mssqllocaldb;Database=BillManagerDB;Trusted_Connection=True;";
            services.AddDbContext<BillManagerDbContext>(o => o.UseSqlServer(connectionString));
            services.AddTransient<IFriendHandler, FriendHandler>();
            services.AddTransient<IBillHandler, BillHandler>();            
            services.AddScoped<IBillManagerRepository, BillManagerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}