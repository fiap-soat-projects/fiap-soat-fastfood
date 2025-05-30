using Domain.Entities.Interfaces;

namespace Domain.Exceptions;

public class InvalidEntityPropertyException<TEntity> : DomainException where TEntity : IAggregateRoot
{
    private static readonly string _entityClassName = typeof(TEntity).Name;

    private const string INVALID_ENTITY_PROPERTY_TEMPLATE_MESSAGE = "The property {0} of {1} is invalid";

    public InvalidEntityPropertyException(string propertyName)
        : base(string.Format(INVALID_ENTITY_PROPERTY_TEMPLATE_MESSAGE, propertyName, _entityClassName))
    {

    }

    public static void ThrowIfEmptyOrWhiteSpace(string? value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEntityPropertyException<TEntity>(propertyName);
        }
    }
}
