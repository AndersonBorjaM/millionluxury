namespace Million.Domain.Abstractions
{
    public abstract class Entity<TEntityId>: IEntity
    {
        public TEntityId? Id { get; init; }

        protected Entity(TEntityId entityId)
        {
        }

    }
}
