namespace MicroserviceDemo.Domain.Seedwork.ValueObjects;

using System.Linq;

public abstract class ValueObject : IValueObject
{

    protected abstract IEnumerable<object> GetComponents();

    public override string ToString()
    {
        var components = GetComponents().Select(c => c.ToString());
        var componentsString = String.Join(",\n", components);
        return $"{GetType().Name} [{componentsString}\n]";
    }

    public override bool Equals(object obj)
    {
        return obj is ValueObject valueObject && Equals(valueObject);
    }

    public bool Equals(ValueObject obj)
    {
        if (obj is null || obj.GetType().Equals(GetType()))
        {
            return false;
        }
        return obj.GetComponents().SequenceEqual(GetComponents());
    }

    public override int GetHashCode()
    {
        return GetComponents()
            .Select(c => c?.GetHashCode() ?? 0)
            .Aggregate((c1, c2) => c1 ^ c2);
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return left?.Equals(right) ?? Object.Equals(right, null);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

}
