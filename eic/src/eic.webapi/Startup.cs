﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Couchbase.Core;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using eic.core.Repositories;
using eic.infrastructure.Repositories;
using eic.application;
using eic.infrastructure;
using ew.middleware.common;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ew.middleware.idsrv_wrapper;

namespace eic.webapi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);


            // inject bucket, services, repositories
            services.AddCouchbase(Configuration.GetSection("Couchbase"));
            services.AddCouchbaseBucket<IEicBucketProvider>("eiccb");
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IActionRepository, ActionRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IEicMapper, EicMapper>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IGroupManager, GroupManager>();
            services.AddTransient<IActionManager, ActionManager>();

            services.AddTransient<IIdSrvManager, IdSrvManager>();

            services.AddMemoryCache();
            services.AddMvc();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // this uses the policy called "default"
            app.UseCors("AllowAll");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseProcessingTimeMiddleware();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://id.easyadmincp.com",
                RequireHttpsMetadata = false,

                //ApiName = "apis_fullaccess", // access_token của client được trả về phải có aud chứa Resource này ( ApiName) thì mới xác thực, Không qui định thì là ALL, ko ràng buộc
                //AllowedScopes = { "apis_fullaccess__read", "apis_fullaccess__write" }, // client phải request access_token với 1 trong các scope này mới dc chứng thực, KHONG QUI DINH -> ALL, ko ràng buộc

                //SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both,
                SaveToken = true
            });


            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
