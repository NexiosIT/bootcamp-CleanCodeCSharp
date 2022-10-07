using CleanCode.Common.Parsers;
using CleanCode.Domain.Models;

namespace CleanCode.Business.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IBookParser _bookParser;

    public BookRepository(IBookParser bookParser)
    {
        _bookParser = bookParser;
    }

    public List<Book> GetAllBooks()
    {
        var allBooks = _bookParser.ParseCsvToBooks();
        var random   = new Random();

        return allBooks.Select(book => new Book
        {
            Id            = random.Next(1000),
            Name          = book.Name,
            Genre         = book.Genre,
            Price         = book.Price,
            PriceTypes    = book.PriceTypes,
            Store         = book.Store,
            AmountOfPages = book.AmountOfPages
        }).ToList();
    }
}

public interface IBookRepository
{
    List<Book> GetAllBooks();
}
