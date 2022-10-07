using CleanCode.Business.Repositories;
using CleanCode.Domain.Models;

namespace CleanCode.Business.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<Book> GetBooks(int amount) => _bookRepository.GetAllBooks().Take(amount).ToList();

    public List<Book> GetBooksByStore(string store)
    {
        var allBooks = _bookRepository.GetAllBooks();

        return allBooks.Where(x => x.Store == store).ToList();
    }
}

public interface IBookService
{
    List<Book> GetBooks(int amount);
    List<Book> GetBooksByStore(string store);
}
