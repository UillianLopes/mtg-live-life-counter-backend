namespace MtgLiveLifeCounter.Core.Contracts
{
    public interface IAuthenticatedUser
    {
        public Guid Id { get; }
        public string? Email { get; }
        public string? Username { get; }
    }
}
