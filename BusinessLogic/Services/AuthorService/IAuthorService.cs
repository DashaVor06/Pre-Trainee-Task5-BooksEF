using BusinessLogic.DTO;

namespace BusinessLogic.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<List<AuthorDTO>> GetAllAsync();
        Task<AuthorDTO?> GetByIdOrNullAsync(int id);
        Task<AuthorDTO?> GetByNameOrNullAsync(string name);
        Task<List<AuthorAndBooksAmountDTO>> GetBooksAmountAsync();
        Task<AuthorDTO?> CreateOrNullAsync(AuthorDTO author);
        Task<AuthorDTO?> UpdateOrNullAsync(AuthorDTO author);
        Task<bool> DeleteAsync(int id);
    }
}
