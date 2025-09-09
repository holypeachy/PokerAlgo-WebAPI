[Serializable]
public class BadNumberOfCommunityCardsException : Exception
{
    public BadNumberOfCommunityCardsException() { }
    public BadNumberOfCommunityCardsException(string message) : base(message) { }
    public BadNumberOfCommunityCardsException(string message, Exception inner) : base(message, inner) { }
}