namespace Business.Exceptions;

public abstract class DomainException(string message) : Exception(message)
{

}