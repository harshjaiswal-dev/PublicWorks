using Data.Constants;

namespace Data.Exceptions
{
    public class NullEntityException : Exception
    {
        public NullEntityException(string entityName)
            : base(string.Format(AppMessages.EntityCannotBeNull, entityName))
        {
        }
    }
}