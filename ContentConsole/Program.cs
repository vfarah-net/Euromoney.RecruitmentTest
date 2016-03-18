using System;
using Castle.Windsor;
using Domain;

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
            int badWordsCount = badWordResolver.GetBadWordCount(content);
            Console.WriteLine("Scanned the text:");
            Console.WriteLine(content);
            Console.WriteLine("Total Number of negative words: " + badWordsCount);
            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }
    }

}
