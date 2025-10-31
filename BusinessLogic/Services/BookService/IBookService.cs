using BusinessLogic.DTO;

namespace BusinessLogic.Services.BookService
{
    public interface IBookService
    {
        Task<List<BookAndAuthorDTO>> GetAllBooksAsync();
        Task<BookAndAuthorDTO?> GetByIdOrNullAsync(int id);
        Task<List<BookAndAuthorDTO>> GetPublishedAfterAsync(int year);
        Task<BookDTO?> CreateOrNullAsync(BookDTO book);
        Task<BookDTO?> UpdateOrNullAsync(BookDTO book);
        Task<bool> DeleteAsync(int id);
    }
}
