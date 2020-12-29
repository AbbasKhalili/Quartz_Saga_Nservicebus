using System;
using System.Collections;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var ee = new Father("Hosien");
            ee.Create();


            var ff = new Child("Amin");
            ff.Create();







            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();

            await scheduler.Start();

            var job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            
            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            //await Task.Delay(TimeSpan.FromSeconds(30));
            
            //await scheduler.Shutdown();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();

        }
    }

    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"Greetings from HelloJob! {new Random().Next(1, 100)}");
        }
    }

    public class Father 
    {
        public string Name;
        public Child Child;
        public Father(string name) 
        {
            Name = name;
        }

        public void Create()
        {
            Child = new Child("Arad");
        }
    }
    public class Child : Father
    {
        public new string Name;
        public Father Father;
        public Child(string name) : base(name)
        {
            Name = name;
        }

        public new void Create()
        {
            Father = new Father("Abbas");
        }
    }
}
