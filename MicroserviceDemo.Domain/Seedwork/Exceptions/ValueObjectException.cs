namespace MicroserviceDemo.Domain.Seedwork.Exceptions;

public class ValueObjectException : DomainException
{

    public ValueObjectException(string message) : base(message)
    {
    }

}
