using LibraryManagement.Application.DTO;
using LibraryManagement.Domain;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<Book> GetBookByIdAsync(Guid bookId)
    {
        var book = await bookRepository.GetBookByIdAsync(bookId);
        
        if (book == null)
            throw new BookNotFoundException($"Book with id: {bookId} not found");
        
        return book;
    }

    public async Task AddBookAsync(BookDto book)
    {
        if (string.IsNullOrWhiteSpace(book.Author))
            throw new Exception($"Author is required");
        if (book.Author.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 1)
            throw new Exception($"Author must have at least one name");
        if (string.IsNullOrWhiteSpace(book.Title))
            throw new Exception($"Title is required");
        if (book.DateOfPublication.Date >= DateTime.Now.Date)
            throw new Exception($"Date of publication is invalid");

        var entity = new Book()
        {
            Id = Guid.NewGuid(),
            Author = book.Author,
            Title = book.Title,
            DateOfPublication = book.DateOfPublication
        };

        await bookRepository.AddBookAsync(entity);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await bookRepository.GetAllBooksAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        var existingBook = await bookRepository.GetBookByIdAsync(book.Id);
        
        if (existingBook == null)
            throw new BookNotFoundException($"Book with id: {book.Id} not found");
        
        if (string.IsNullOrWhiteSpace(book.Author))
            throw new Exception($"Author is required");
        if (book.Author.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 1)
            throw new Exception($"Author must have at least one name");
        if (string.IsNullOrWhiteSpace(book.Title))
            throw new Exception($"Title is required");
        if (book.DateOfPublication.Date >= DateTime.Now.Date)
            throw new Exception($"Date of publication is invalid");
        
        await bookRepository.UpdateBookAsync(book);
    }

    public async Task DeleteBookAsync(Guid bookId)
    {
        var existingBook = await bookRepository.GetBookByIdAsync(bookId);
        
        if (existingBook == null)
            throw new BookNotFoundException($"Book with id: {bookId} not found");
        
        await bookRepository.DeleteBookAsync(bookId);
    }
}