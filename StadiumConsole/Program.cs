using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StadiumConsole;

internal class Program
{
    static void Main(string[] args)
    {
        // Need to add Microsoft.Extensions.Hosting nuget package
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        // register dependency in service container
        builder.Services.AddSingleton<IPerson, Player>();
        //  builder.Services.AddSingleton<IPerson, Coach>();

        using IHost host = builder.Build();

        // get the built-in service container
        using IServiceScope serviceScope = host.Services.CreateScope();
        // get the service provider from the service container
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;
        // get the registerd service from the interface
        IPerson person = serviceProvider.GetRequiredService<IPerson>();

        person.HowIam();
    }

    public interface IPerson { 
        void HowIam();
    }

    public class Player : IPerson
    {
        public void HowIam() => Console.WriteLine($"I am a player");
    }

    public class Coach : IPerson
    {
        public void HowIam() => Console.WriteLine($"I am a coach");
    }
}