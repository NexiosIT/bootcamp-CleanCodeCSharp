using CleanCode.Common.Helpers;
using CleanCode.Domain.Constants;
using CleanCode.Domain.Enums;
using CleanCode.Domain.Models;

namespace CleanCode.Common.Parsers;

public class BookParser : IBookParser
{
    public List<Book> ParseCsvToBooks()
    {
        var books  = new List<Book>();
        var parser = DataHelper.CsvParser();

        if (parser == null)
        {
            return new List<Book>();
        }

        while (!parser.EndOfData)
        {
            var row = parser.ReadFields();

            if (row![0].Equals(CommonConstants.Name))
            {
                //Skip header row
                continue;
            }

            var price = int.Parse(row[2]);


            var transaction = new Book
            {
                Name          = row[0],
                Genre         = row[1],
                Price         = price,
                PriceTypes    = GetPriceType(price),
                Store         = row[3],
                AmountOfPages = int.Parse(row[4])
            };

            books.Add(transaction);
        }

        return books;
    }

    private PriceType GetPriceType(int price) => price < 50
        ? PriceType.Cheap
        : price > 200
            ? PriceType.Expensive
            : PriceType.Moderate;
}

public interface IBookParser
{
    List<Book> ParseCsvToBooks();
}
