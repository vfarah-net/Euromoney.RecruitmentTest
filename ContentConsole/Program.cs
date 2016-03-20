using System;
using System.Linq;
using Castle.Windsor;

namespace ContentConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            bool ignoreFilterering = args.Contains("/ignorefiltering", StringComparer.OrdinalIgnoreCase) ||
                   args.Contains("-ignorefiltering", StringComparer.OrdinalIgnoreCase);
            WindsorContainer windsorContainer = BootstrapConfig.Register();
            var applicationShell = windsorContainer.Resolve<IApplicationShell>();
            applicationShell.Run(ignoreFilterering);
            Console.ReadKey();
        }
    }
}
