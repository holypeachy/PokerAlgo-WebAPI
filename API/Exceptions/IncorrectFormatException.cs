namespace PokerAlgoAPI.Exceptions;

[Serializable]
public class IncorrectFormatExceptionException : Exception
{
    public IncorrectFormatExceptionException() { }
    public IncorrectFormatExceptionException(string message) : base(message) { }
    public IncorrectFormatExceptionException(string message, System.Exception inner) : base(message, inner) { }
}