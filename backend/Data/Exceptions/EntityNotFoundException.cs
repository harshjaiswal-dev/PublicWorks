using Data.Constants;

namespace Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, object key)
            : base(string.Format(AppMessages.EntityNotFound, entityName, key))
        {
        }
    }
}