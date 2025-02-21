using Hangfire;

namespace Task1.Services;

public class HangfireService
{
    public void SimpleJob()
    {
        BackgroundJob.Enqueue(() => Console.WriteLine("data is valid"));
    }
    public void SendEmails()
    {
        BackgroundJob.Schedule(() => Console.WriteLine("data is valid"), TimeSpan.FromMinutes(10));
    }
    public void RecurringJob()
    {
        //simulation only 
        Console.WriteLine("data sent");
    }
}
