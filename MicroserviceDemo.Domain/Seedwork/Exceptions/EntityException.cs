namespace MicroserviceDemo.Domain.Seedwork.Exceptions;

public class EntityException : DomainException
{

    public EntityException(string message) : base(message)
    {
    }

}
