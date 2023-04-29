namespace OnionBaseArchitecture.Application.Abstractions.Repositories.User
{
    public interface IUserReadRepository : IReadRepository<Domain.Entities.User>
    {
        Task<Domain.Entities.User> GetUsernameOrEmailAndPassword(string UsernameOrEmail, string Password);
    }
}
