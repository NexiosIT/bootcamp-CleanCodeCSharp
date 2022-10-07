using CleanCode.Domain.Enums;

namespace CleanCode.Domain.Models;

public class Book
{
    public int       Id            { get; set; }
    public string    Name          { get; set; }
    public string    Genre         { get; set; }
    public decimal   Price         { get; set; }
    public PriceType PriceTypes    { get; set; }
    public int       AmountOfPages { get; set; }
    public string    Store         { get; set; }


    public override string ToString() => $"{Store} - {Name} - {Genre} - ${Price} - {PriceTypes} - {AmountOfPages}";
}
