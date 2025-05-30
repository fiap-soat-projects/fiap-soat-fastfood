using Domain.Entities.Interfaces;

namespace Domain.Exceptions;

public abstract class EntityNotFoundException<TEntity> : DomainException where TEntity : IAggregateRoot
{
    private static readonly string _entityClassName = typeof(TEntity).Name;

    private const string ENTITY_NOT_FOUND_TEMPLATE_MESSAGE = "The {0} '{1}' was not found";

    protected EntityNotFoundException(string id)
        : base(string.Format(ENTITY_NOT_FOUND_TEMPLATE_MESSAGE, _entityClassName, id))
    {

    }
}
