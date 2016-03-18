using System;

namespace Domain
{    
    public class BadWordResolver : IBadWordResolver
    {
        public int GetBadWordCount(string content)
        {
            int badWords = 0;
            if (String.IsNullOrEmpty(content))
            {
                return badWords;
            }

            string bannedWord1 = "swine";
            string bannedWord2 = "bad";
            string bannedWord3 = "nasty";
            string bannedWord4 = "horrible";

            if (content.Contains(bannedWord1))
            {
                badWords = badWords + 1;
            }

            if (content.Contains(bannedWord2))
            {
                badWords = badWords + 1;
            }
            if (content.Contains(bannedWord3))
            {
                badWords = badWords + 1;
            }
            if (content.Contains(bannedWord4))
            {
                badWords = badWords + 1;
            }

            return badWords;
        }
    }
}
