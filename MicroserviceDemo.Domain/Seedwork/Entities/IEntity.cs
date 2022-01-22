namespace MicroserviceDemo.Domain.Seedwork.Entities;

public interface IEntity<T>
{

    T Id { get; }

    string ToString();

    bool Equals(object obj);

    int GetHashCode();

}
