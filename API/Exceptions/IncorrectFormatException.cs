namespace PokerAlgoAPI.Exceptions;

[Serializable]
public class IncorrectFormatException : Exception
{
    public IncorrectFormatException() { }
    public IncorrectFormatException(string message) : base(message) { }
    public IncorrectFormatException(string message, System.Exception inner) : base(message, inner) { }
}