using Hangfire;

namespace Task1.Services;

public class HangfireService
{
    public void SimpleJob()
    {
        BackgroundJob.Enqueue(() => Console.WriteLine("data is valid"));
    }
    
}
