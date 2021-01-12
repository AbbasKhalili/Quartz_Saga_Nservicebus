using System;
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace ConsoleApp1
{
    
    

    public delegate int Operation(int x, int y);

    public delegate void DelEventHandler();

    

    class Program
    {
        Func<string, string, string> StringJoin = (str1, str2) => string.Concat(str1, str2);
        static Expression<Func<string, string, string>> StringJoinExpr = (str1, str2) => string.Concat(str1, str2);

        public static event DelEventHandler Add1;

        static int Method(int x, int y)
        {
            return x + y;
        }
        

        static void USA()
        {
            Console.WriteLine("USA");
        }
        static void India()
        {
            Console.WriteLine("India");
        }
        static void England()
        {
            Console.WriteLine("England");
        }
        static void Main(string[] args)
        {

            var obj1 = new Operation(Method);
            var obj2 = new Operation(Method);
            var obj3 = new Operation(Method);
            Console.WriteLine(obj1(2, 7));
            Console.WriteLine(obj2(3, 7));
            Console.WriteLine(obj3(4, 7));

            Add1 += USA;
            Add1 += India;
            Add1 += England;
            Add1.Invoke();


            var func = StringJoinExpr.Compile();

            var result = func("Smith", "Jones");

            var ee = new Father("Hosien");
            ee.Create();


            var ff = new Child("Amin");
            ff.Create();


            IWriteLine line = new WriteLine(); 
            line.WriteLine();
            Console.WriteLine(line.Tree("OH"));


            int? nullableString = null;
            Console.WriteLine((int?) null);


            Index i1 = 3; // number 3 from beginning  
            Index i2 = ^4; // number 4 from end  
            int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine($"{a[i1]}, {a[i2]}"); // "3, 6"  
            var slice = a[i1..i2];
            Console.WriteLine(slice);

            string variable = null;

            variable ??= "";
            Console.WriteLine(variable);

            var t= new XValue();
            t.IncreaseX();

            //var factory = new StdSchedulerFactory();
            //var scheduler = await factory.GetScheduler();

            //await scheduler.Start();

            //var job = JobBuilder.Create<HelloJob>()
            //    .WithIdentity("job1", "group1")
            //    .Build();

            
            //var trigger = TriggerBuilder.Create()
            //    .WithIdentity("trigger1", "group1")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(2)
            //        .RepeatForever())
            //    .Build();

            //await scheduler.ScheduleJob(job, trigger);

            //await Task.Delay(TimeSpan.FromSeconds(30));
            
            //await scheduler.Shutdown();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();

            
        }
    }

    public struct XValue
    {
        private int X { get; set; }

        public readonly int IncreaseX()
        {
            // This will not compile: C# 8   
            // X = X + 1;   

            var newX = X + 1; // OK   
            return newX;
        }
    }

    public partial class Tree1 { } // C# 7  
    public  class Tree2 { } // C# 8  

    public struct Point 
    {
        public Point(int x, int y,int z)
        {
            
        }

    }

    interface IWriteLine
    {
        public void WriteLine()
        {
            Console.WriteLine("Wow C# 8!");
        }

        public string Tree(string pa)
        {
            return $"tree; + {pa}";
        }
    }

    public class WriteLine : IWriteLine
    {

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
