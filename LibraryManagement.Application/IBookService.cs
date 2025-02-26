using LibraryManagement.Application.DTO;
using LibraryManagement.Domain;

namespace LibraryManagement.Application;

public interface IBookService
{
    Task<Book> GetBookByIdAsync(Guid bookId);
    Task AddBookAsync(BookDto book);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(Guid bookId);
}