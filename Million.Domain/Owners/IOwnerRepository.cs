namespace Million.Domain.Owners
{
    public interface IOwnerRepository
    {
        Task<Owner?> GetByIdAsync(OwnerId id, CancellationToken cancellationToken = default);
        Task<Owner> CreateAsync(Owner entity);
        Task<bool> IsOwnerExistsAsync(OwnerId ownerId, CancellationToken cancellationToken = default);
    }
}
