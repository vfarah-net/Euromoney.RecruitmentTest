using System;
using Castle.Windsor;
using Domain.Models;
using Domain.Resolvers;

namespace ContentConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WindsorContainer windsorContainer = BootstrapConfig.Register();
            string content =
                "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            IBadWordResolver badWordResolver = windsorContainer.Resolve<IBadWordResolver>();
            InitializeMandatoryBadWords(badWordResolver);
            int badWordsCount = badWordResolver.GetBadWordCount(content);
            Console.WriteLine("Scanned the text:");
            Console.WriteLine(content);
            Console.WriteLine("Total Number of negative words: " + badWordsCount);
            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }

        private static void InitializeMandatoryBadWords(IBadWordResolver badWordResolver)
        {
            badWordResolver.AddBadWords(
                new BadWord { Name = "swine" },
                new BadWord { Name = "bad" },
                new BadWord { Name = "nasty" },
                new BadWord { Name = "horrible" });
        }
    }

}
