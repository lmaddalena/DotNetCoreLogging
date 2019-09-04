using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            // create and configure the service container
            IServiceCollection serviceCollection = ConfigureServices();

            // build the service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogDebug(99, "Hello");

            // cerate the instance of the class using the service provider
            MyClass cl = serviceProvider.GetService<MyClass>();

            cl.DoWork();

            System.Console.WriteLine("Press [ENTER] to quit:\n");
            Console.ReadLine();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection service = new ServiceCollection();

            /*
            * Service Lifetimes
            * ====================================================================================
            * There are three service lifetimes in ASP.NET Core Dependency Injection:
            *
            * 1. Transient services are created every time they are injected or requested.
            * 2. Scoped services are created per scope. In a web application, every web request 
            *    creates a new separated service scope. 
            *    That means scoped services are generally created per web request.
            * 3. Singleton services are created per DI container. That generally means that they 
            *    are created only one time per application and then used for whole the application life time.

            * Good Practices
            * ====================================================================================
            * - Register your services as transient wherever possible. Because it’s simple to design 
            *   transient services. You generally don’t care about multi-threading and memory leaks 
            *   and you know the service has a short life.
            *
            * - Use scoped service lifetime carefully since it can be tricky if you create child service 
            *   scopes or use these services from a non-web application.
            *
            * - Use singleton lifetime carefully since then you need to deal with multi-threading and 
            *   potential memory leak problems.
            *
            * - Do not depend on a transient or scoped service from a singleton service. 
            *   Because the transient service becomes a singleton instance when a singleton service 
            *   injects it and that may cause problems if the transient service is not designed to support 
            *   such a scenario. ASP.NET Core’s default DI container already throws exceptions in such cases.
            *
            */

            service.AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddTransient<MyClass>();
            
            return service;
        }
    }
}
