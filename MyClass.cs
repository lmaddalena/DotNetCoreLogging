using Microsoft.Extensions.Logging;

namespace DotNetCoreLogging
{
    internal class MyClass {
        private readonly ILogger _logger;
        
        public MyClass(ILogger<MyClass> logger)
        {
            _logger = logger;
        }

        public void DoWork() {
            _logger.LogInformation("Hello from DoWork method");
        }

    }
}