using System.Collections.Concurrent;
using System.Collections.Immutable;
using LibraryManagement.Domain;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Instrastructure;

public class InMemoryBookRepository : IBookRepository
{
    private ConcurrentDictionary<Guid, Book> _books;

    public InMemoryBookRepository()
    {
        _books = new();
    }
    
    public Task<Book?> GetBookByIdAsync(Guid bookId)
    {
        _books.TryGetValue(bookId, out Book book);
        
        return Task.FromResult(book);
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return Task.FromResult(_books.Values.AsEnumerable());
    }

    public Task AddBookAsync(Book book)
    {
        _books.TryAdd(book.Id, book);
        return Task.CompletedTask;
    }

    public Task UpdateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookAsync(Guid bookId)
    {
        throw new NotImplementedException();
    }
}