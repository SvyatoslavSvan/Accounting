namespace Accounting.Domain.Models.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
    }
}
