
public class ObstructedPathException : Exception
{
    public ObstructedPathException()
    {
    }

    public ObstructedPathException(string message)
        : base(message)
    {
    }

    public ObstructedPathException(string message, Exception inner)
        : base(message, inner)
    {
    }
}