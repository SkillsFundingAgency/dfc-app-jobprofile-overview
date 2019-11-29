﻿using AutoMapper;
using DFC.App.JobProfileOverview.MessageFunctionApp.Models;
using DFC.App.JobProfileOverview.MessageFunctionApp.Services;
using DFC.Functions.DI.Standard;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

[assembly: WebJobsStartup(typeof(DFC.App.JobProfileOverview.MessageFunctionApp.Startup.WebJobsExtensionStartup), "Web Jobs Extension Startup")]

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Startup
{
    [ExcludeFromCodeCoverage]
    public class WebJobsExtensionStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var segmentClientOptions = configuration.GetSection("JobProfileOverviewSegmentClientOptions").Get<SegmentClientOptions>();

            builder.AddDependencyInjection();
            builder?.Services.AddAutoMapper(typeof(WebJobsExtensionStartup).Assembly);
            builder.Services.AddSingleton<SegmentClientOptions>(segmentClientOptions);
            builder.Services.AddSingleton<HttpClient>(new HttpClient());
            builder?.Services.AddSingleton<IHttpClientService, HttpClientService>();
            builder?.Services.AddSingleton<IMessageProcessor, MessageProcessor>();
            builder?.Services.AddSingleton<IMappingService, MappingService>();
            builder?.Services.AddSingleton<ILogger, Logger<WebJobsExtensionStartup>>();
        }
    }
}