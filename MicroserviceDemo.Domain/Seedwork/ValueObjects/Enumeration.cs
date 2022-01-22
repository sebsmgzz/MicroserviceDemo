namespace MicroserviceDemo.Domain.Seedwork.ValueObjects;

using System;

/// <summary>
/// Use enumeration classes instead of enum types.
/// <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types"/>
/// </summary>
public abstract class Enumeration<T, U> : IValueObject, IComparable where T : IComparable
{

    public T Key { get; }

    public U Value { get; }

    protected Enumeration(T key, U value)
    {
        Key = key;
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override bool Equals(object obj)
    {
        if (obj is Enumeration<T, U> enumeration)
        {
            return Equals(enumeration);
        }
        return false;
    }

    public bool Equals(Enumeration<T, U> enumeration)
    {
        var typeMatches = GetType().Equals(enumeration.GetType());
        var keyMatches = Key.Equals(enumeration.Key);
        return typeMatches && keyMatches;
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }

    public int CompareTo(object obj)
    {
        if (obj is Enumeration<T, U> enumeration)
        {
            return Key.CompareTo(enumeration.Key);
        }
        throw new ArgumentException(
            $"Cannot compare type '{obj.GetType()}' " +
            $"since it is not of the same enumeration type.");
    }

    public static bool operator ==(Enumeration<T, U> left, Enumeration<T, U> right)
    {
        return left?.Equals(right) ?? Equals(right, null);
    }

    public static bool operator !=(Enumeration<T, U> left, Enumeration<T, U> right)
    {
        return !(left == right);
    }

}
