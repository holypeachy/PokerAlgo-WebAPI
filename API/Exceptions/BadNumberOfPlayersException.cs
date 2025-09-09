[Serializable]
public class BadNumberOfPlayersException : Exception
{
    public BadNumberOfPlayersException() { }
    public BadNumberOfPlayersException(string message) : base(message) { }
    public BadNumberOfPlayersException(string message, Exception inner) : base(message, inner) { }
}