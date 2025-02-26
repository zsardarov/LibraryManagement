using System.Collections.Concurrent;
using LibraryManagement.Domain;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly ConcurrentDictionary<Guid, Book> _books = new();

    public Task<Book?> GetBookByIdAsync(Guid bookId)
    {
        _books.TryGetValue(bookId, out Book? book);
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
        _books.TryUpdate(book.Id, book, _books[book.Id]);
        return Task.CompletedTask;
    }

    public Task DeleteBookAsync(Guid bookId)
    {
        _books.TryRemove(bookId, out _);
        return Task.CompletedTask;
    }
}