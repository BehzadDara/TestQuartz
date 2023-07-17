using Quartz;
using Quartz.Impl;

ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
IScheduler scheduler = await schedulerFactory.GetScheduler();

IJobDetail job = JobBuilder.Create<HelloWorldJob>()
    .WithIdentity("helloJob", "group")
    .Build();

ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("helloTrigger", "group")
    .StartNow()
    /*.WithSimpleSchedule(x => x
        .WithIntervalInSeconds(5)
        .RepeatForever())*/
    .WithCronSchedule("*/5 * * ? * * *")
    .Build();

await scheduler.ScheduleJob(job, trigger);
await scheduler.Start();

while (true);

public class HelloWorldJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"Hello world! {DateTime.Now:yyyy:MM:dd} - {DateTime.Now:HH:mm:ss}");
        return Task.CompletedTask;
    }
}