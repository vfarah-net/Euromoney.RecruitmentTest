namespace Domain
{
    public interface IBadWordResolver
    {
        int GetBadWordCount(string content);
    }
}