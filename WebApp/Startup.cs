using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            

            //services.AddQuartz(async q =>
            //{

            //    var factory = new StdSchedulerFactory();
            //    var scheduler = await factory.GetScheduler();

            //    await scheduler.Start();

            //    var job = JobBuilder.Create<HelloJob>()
            //        .WithIdentity("job1", "group1")
            //        .Build();


            //    var trigger = TriggerBuilder.Create()
            //        .WithIdentity("trigger1", "group1")
            //        .StartNow()
            //        .WithSimpleSchedule(x => x
            //            .WithIntervalInSeconds(2)
            //            .RepeatForever())
            //        .Build();

            //    await scheduler.ScheduleJob(job, trigger);

            //});

            
            //services.AddQuartzServer(options =>
            //{
                
            //    options.WaitForJobsToComplete = true;
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"Greetings from Web HelloJob! {new Random().Next(1, 100)}");

        }
    }

    
}
