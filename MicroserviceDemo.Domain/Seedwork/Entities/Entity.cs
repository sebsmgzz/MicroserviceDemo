namespace MicroserviceDemo.Domain.Seedwork.Entities;

using MediatR;
using System;

public abstract class Entity<T> : IEntity<T>
{

    private T? id;
    private readonly List<INotification> domainEvents = new();

    public virtual T Id
    {
        get => id!;
        set => id = IsTransient() ? value :
            throw new InvalidOperationException();
    }

    public bool IsTransient()
    {
        return (id is object && id == null) || id.Equals(default(T));
    }

    protected void AddDomainEvent(INotification domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    protected void RemoveDomainEvent(INotification domainEvent)
    {
        domainEvents.Remove(domainEvent);
    }

    public IEnumerable<INotification> GetDomainEvents()
    {
        return domainEvents;
    }

    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }

    public bool Equals(Entity<T> entity)
    {
        // False if any is transient
        if (entity.IsTransient() || IsTransient())
        {
            return false;
        }
        // Else, since this is an abstract class
        // ensure base types are a match and ids are equal
        var typeMatches = GetType().Equals(entity.GetType());
        var idMatches = Id.Equals(entity.Id);
        return typeMatches && idMatches;
    }

    public override bool Equals(object obj)
    {
        return obj is Entity<T> entity && Equals(entity);
    }

    public override int GetHashCode()
    {
        return IsTransient() ? base.GetHashCode() : Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{GetType().Name}] {GetHashCode()}";
    }

    public static bool operator ==(Entity<T> left, Entity<T> right)
    {
        return left?.Equals(right) ?? Equals(right, null);
    }

    public static bool operator !=(Entity<T> left, Entity<T> right)
    {
        return !(left == right);
    }

}
