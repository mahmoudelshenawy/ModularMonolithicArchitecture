namespace Shared.Core.Exceptions
{
    public class EntityNotFound : Exception
    {
        public static string _entity;
        public EntityNotFound(string entity) : base($"{entity} is not found")
        {
            _entity = entity;
        }

        public EntityNotFound() : base("model not found")
        {
            
        }
    }
}
