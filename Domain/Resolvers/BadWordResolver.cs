using System;
using System.Globalization;
using System.Linq;
using System.Text;
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
            foreach (var badWord in badWords.Where(badWord =>
                !badWordsRepository.Contains(item => item.Name.Equals(badWord.Name))))
            {
                badWordsRepository.Add(badWord);
            }
        }

        public string Filter(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} is missing", nameof(content)));
            }

            return badWordsRepository.GetAll()
                .Where(negativeWord => content.Contains(negativeWord.Name))
                .Aggregate(content, (current, badWord) => current.Replace(badWord.Name, DisplayHash(badWord.Name)));
        }

        private string DisplayHash(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
            {
                return name;
            }
            StringBuilder result = new StringBuilder();
            int strLength = name.Length;
            result.Append(name[0]);
            for (int i = 1; i < strLength - 1; i++)
            {
                result.Append("#");
            }
            result.Append(name[strLength - 1]);
            return result.ToString();
        }
    }
}
