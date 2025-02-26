using LibraryManagement.Application.DTO;
using LibraryManagement.Domain;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<Book> GetBookByIdAsync(Guid bookId)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookId);
        
        if (book == null)
            throw new BookNotFoundException($"Book with id: {bookId} not found");
        
        return book;
    }

    public async Task AddBookAsync(BookDto book)
    {
        if (string.IsNullOrWhiteSpace(book.Author))
            throw new Exception($"Author is required");
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

        await _bookRepository.AddBookAsync(entity);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllBooksAsync();
    }
}