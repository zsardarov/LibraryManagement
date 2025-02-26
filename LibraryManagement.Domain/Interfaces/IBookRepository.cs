namespace LibraryManagement.Domain.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetBookByIdAsync(Guid bookId);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(Guid bookId);
}