[Serializable]
public class NotEnoughPlayersExceptionException : Exception
{
    public NotEnoughPlayersExceptionException() { }
    public NotEnoughPlayersExceptionException(string message) : base(message) { }
    public NotEnoughPlayersExceptionException(string message, Exception inner) : base(message, inner) { }
}