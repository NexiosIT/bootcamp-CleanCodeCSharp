using CleanCode.Business.Services;
using CleanCode.Domain.Constants;
using CleanCode.Domain.Models;

namespace CleanCode.App;

public class App
{
    private readonly AppConfiguration _appConfiguration;
    private readonly IBookService     _bookService;

    public App(AppConfiguration appConfiguration, IBookService bookService)
    {
        _appConfiguration = appConfiguration;
        _bookService      = bookService;
    }

    public void Run()
    {
        var booksToBuy = _bookService.GetBooks(_appConfiguration.AmountOfBooksToBuy);

        Console.WriteLine("Book Store");
        Console.WriteLine();
        Console.WriteLine("All books");
        Console.WriteLine(UiConstants.DottedLine);
        Console.WriteLine();

        foreach (var bookToBuy in booksToBuy)
        {
            Console.WriteLine(bookToBuy.ToString());
        }

        Console.WriteLine();
        Console.WriteLine("All books by store");
        Console.WriteLine(UiConstants.DottedLine);
        Console.WriteLine();


        var allBooksFromStore = _bookService.GetBooksByStore(_appConfiguration.Store);

        foreach (var book in allBooksFromStore)
        {
            Console.WriteLine(book.ToString());
        }

        Console.ReadKey(true);
    }
}
