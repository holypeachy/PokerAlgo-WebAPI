[Serializable]
public class IncorrectNumberOfCommunityCardsException : Exception
{
    public IncorrectNumberOfCommunityCardsException() { }
    public IncorrectNumberOfCommunityCardsException(string message) : base(message) { }
    public IncorrectNumberOfCommunityCardsException(string message, Exception inner) : base(message, inner) { }
}