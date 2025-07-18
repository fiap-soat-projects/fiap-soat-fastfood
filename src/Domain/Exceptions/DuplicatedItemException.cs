namespace Business.Exceptions;

public class DuplicatedItemException<TEntity> : Exception where TEntity : class
{
    const string DUPLICATED_ITEM_TEMPLATE_MESSAGE = "A {0} with same {1} already exists";

    private static readonly string _entityClassName = typeof(TEntity).Name;

    public DuplicatedItemException(string propertyName) :
        base(string.Format(DUPLICATED_ITEM_TEMPLATE_MESSAGE, _entityClassName, propertyName))
    {

    }
}
