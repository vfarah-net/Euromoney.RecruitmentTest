using Domain.Models;

namespace Domain.Resolvers
{
    public interface IBadWordResolver
    {
        int GetBadWordCount(string content);
        void AddBadWords(params BadWord[] badWords);
    }
}