using System;
using System.Linq;
using Domain.Models;
using Domain.Repository;

namespace Domain.Resolvers
{
    public class BadWordResolver : IBadWordResolver
    {
        private readonly IRepository<BadWord> badWordsRepository;

        public BadWordResolver(IRepository<BadWord> badWordsRepository)
        {
            this.badWordsRepository = badWordsRepository;
        }

        public int GetBadWordCount(string content)
        {
            int badWords = 0;
            if (string.IsNullOrEmpty(content))
            {
                return badWords;
            }

            return badWordsRepository.GetAll()
                .Count(negativeWord => content.Contains(negativeWord.Name));
        }

        public void AddBadWords(params BadWord[] badWords)
        {
            if (badWords == null)
            {
                throw new ArgumentNullException(nameof(badWords));
            }

            foreach (
                var badWord in
                    badWords.Where(
                        badWord =>
                            !badWordsRepository.Contains(
                                item => item.Name.Equals(badWord.Name, StringComparison.OrdinalIgnoreCase))))
            {
                badWordsRepository.Add(badWord);
            }
        }

        public string Filter(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return badWordsRepository.GetAll()
                .Where(negativeWord => content.Contains(negativeWord.Name))
                .Aggregate(content, (current, badWord) => current.Replace(badWord.Name, badWord.FilterName));
        }
    }
}
