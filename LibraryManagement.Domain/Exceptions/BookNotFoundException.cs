namespace LibraryManagement.Domain.Exceptions;

public class BookNotFoundException(string message) : Exception(message);