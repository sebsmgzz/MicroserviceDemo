namespace MicroserviceDemo.Domain.Seedwork.ValueObjects;

public interface IValueObject
{

    string ToString();

    bool Equals(object obj);

    int GetHashCode();

}
