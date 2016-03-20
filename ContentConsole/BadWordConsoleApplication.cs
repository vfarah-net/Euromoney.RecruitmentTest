using System;
using Domain.Models;
using Domain.Resolvers;

namespace ContentConsole
{
    public class BadWordConsoleApplication : IApplicationShell
    {
        private readonly IBadWordResolver badWordResolver;

        public BadWordConsoleApplication(IBadWordResolver badWordResolver)
        {
            this.badWordResolver = badWordResolver;
            InitializeMandatoryBadWords();
        }

        public void Run(bool ignoreFiltering)
        {
            string content = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            int badWordsCount = badWordResolver.GetBadWordCount(content);
            Console.WriteLine("Scanned the text:");
            Console.WriteLine(ignoreFiltering ? content : badWordResolver.Filter(content));
            Console.WriteLine("Total Number of negative words: " + badWordsCount);
            Console.WriteLine("Press ANY key to exit.");
        }

        private void InitializeMandatoryBadWords()
        {
            badWordResolver.AddBadWords(
               new BadWord { Name = "swine" },
               new BadWord { Name = "bad" },
               new BadWord { Name = "nasty" },
               new BadWord { Name = "horrible" });
        }
    }
}
