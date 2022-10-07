using System.Reflection;
using Microsoft.VisualBasic.FileIO;

namespace CleanCode.Common.Helpers;

public static class DataHelper
{
    public static TextFieldParser CsvParser()
    {
        try
        {
            var file = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, @"Data\Books.csv");

            return new TextFieldParser(file)
            {
                Delimiters = new[]
                {
                    ";"
                }
            };
        }
        catch (Exception)
        {
            return null;
        }
    }
}
