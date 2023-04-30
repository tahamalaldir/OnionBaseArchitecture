namespace OnionBaseArchitecture.Application.Abstractions.Repositories.ApiUser
{
    public interface IApiUserReadRepository : IReadRepository<Domain.Entities.ApiUser>
    {
        Task<Domain.Entities.ApiUser> GetByUsernameAndPasswordAsync(string Username, string Password);
    }
}
