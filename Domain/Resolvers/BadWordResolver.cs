using System;
using System.Linq;
using Domain.Models;
using Domain.Repository;

namespace Domain.Resolvers
{
    public class BadWordResolver : IBadWordResolver
    {
        private readonly IRepository<BadWord> negativeWordDataRepository;

        public BadWordResolver(IRepository<BadWord> negativeWordDataRepository)
        {
            this.negativeWordDataRepository = negativeWordDataRepository;
        }

        public int GetBadWordCount(string content)
        {
            int badWords = 0;
            if (string.IsNullOrEmpty(content))
            {
                return badWords;
            }

            return negativeWordDataRepository.GetAll().Count(negativeWord => content.Contains(negativeWord.Name));
        }

        public void AddBadWords(params BadWord[] badWords)
        {
            if (badWords == null)
            {
                throw new ArgumentNullException(nameof(badWords));
            }

            foreach (var badWord in badWords.Where(badWord => !negativeWordDataRepository.Contains(item => item.Name.Equals(badWord.Name, StringComparison.OrdinalIgnoreCase))))
            {
                negativeWordDataRepository.Add(badWord);
            }
        }
    }
}
