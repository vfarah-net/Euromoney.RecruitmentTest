using System;
using System.Linq;
using Castle.Windsor;
using Domain.Models;
using Domain.Resolvers;

namespace ContentConsole
{
    public static class Program
    {
        //TODO: Refactor this so tests can be written
        public static void Main(string[] args)
        {
            bool ignoreFilter = args.Contains("/ignorefiltering", StringComparer.OrdinalIgnoreCase) ||
                                args.Contains("-ignorefiltering", StringComparer.OrdinalIgnoreCase);
            WindsorContainer windsorContainer = BootstrapConfig.Register();
            string content = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            IBadWordResolver badWordResolver = windsorContainer.Resolve<IBadWordResolver>();
            InitializeMandatoryBadWords(badWordResolver);
            int badWordsCount = badWordResolver.GetBadWordCount(content);
            Console.WriteLine("Scanned the text:");
            Console.WriteLine(ignoreFilter ? content : badWordResolver.Filter(content));
            Console.WriteLine("Total Number of negative words: " + badWordsCount);
            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }

        private static void InitializeMandatoryBadWords(IBadWordResolver badWordResolver)
        {
            badWordResolver.AddBadWords(
                new BadWord { Name = "swine", FilterName = "s###e" },
                new BadWord { Name = "bad", FilterName = "b##" },
                new BadWord { Name = "nasty", FilterName = "n###y" },
                new BadWord { Name = "horrible", FilterName = "h######e" });
        }
    }

}
