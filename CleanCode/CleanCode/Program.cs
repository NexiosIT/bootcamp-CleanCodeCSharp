using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;


namespace CleanCode
{
    internal class Program
    {
        private static bool isProduction;//Handy bool for later

        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = GetServiceProvider();
            serviceProvider.GetRequiredService<App>().Run();
        }

        public int providerCOUNT { get; set; }

        public static IServiceProvider GetServiceProvider() => GetServiceCollection().BuildServiceProvider();

        private static IServiceCollection GetServiceCollection()
        {
            var config = GetConfiguration();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<App>();

            SetupConfiguration(serviceCollection, config);

            return serviceCollection;
        }

        private static void SetupConfiguration(IServiceCollection serviceCollection, IConfigurationRoot config)
        {
            serviceCollection.Configure<AppConfiguration>(Options.DefaultName, options => config.GetSection("App").Bind(options));
            serviceCollection.AddScoped(cfg => cfg.GetRequiredService<IOptionsSnapshot<AppConfiguration>>().Value);
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName).AddJsonFile("appsettings.json", false, true);

            return configurationBuilder.Build();
        }


    }

    public class AppConfiguration
    {
        private int _booksToBuy;

        public int BooksToBuy
        {
            get => _booksToBuy;
            set => _booksToBuy = value;
        }

        public String Store { get; set; }
    }

    public class App
    {
         AppConfiguration appConfiguration;
        public App(AppConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
        }

        public void Run()
        {
            var allBooks = getAllBooks(appConfiguration.BooksToBuy);

            Console.WriteLine("Book Store");
            Console.WriteLine();
            Console.WriteLine("All books");
            Console.WriteLine("---------------");
            Console.WriteLine();

            foreach (var allBook in allBooks)
            {
                Console.WriteLine(allBook.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("All books by store");
            Console.WriteLine("---------------");
            Console.WriteLine();


            var allBooksFromStore = getAllBooksFromStore(appConfiguration.Store);

            foreach (var allBook in allBooksFromStore)
            {
                Console.WriteLine(allBook.ToString());
            }

            Console.ReadKey(true);
        }

        public List<Book> getAllBooks(int booksToBuy)
        {
            var allBooks = Parse();
            var books = new List<Book>();

            for (int i = 0; i < booksToBuy; i++)
            {
                var random = new Random();
                books.Add(new Book
                {
                    _id = random.Next(1000),
                    Name = allBooks[i].Name,
                    BookType = allBooks[i].BookType,
                    Price = allBooks[i].Price,
                    PriceTypes = allBooks[i].PriceTypes,
                    Store = allBooks[i].Store,
                    pages = allBooks[i].pages
                });
            }

            return books;
        }

        public List<Book> getAllBooksFromStore(string store)
        {
            var allBooks = Parse();
            var books    = new List<Book>();

            foreach (var allBook in allBooks.Where(x => x.Store == store))
            {
                books.Add(allBook);
            }

            return books;
        }

        private List<Book> Parse()
        {
            var books  = new List<Book>();
            var parser = Data.CsvParser();

            if (parser == null)
            {
                return new List<Book>();
            }

            var id = 0;

            while (!parser.EndOfData)
            {
                var row = parser.ReadFields();

                if (row![0].Equals("Name"))
                {
                    //Skip header row
                    continue;
                }
                
                var transaction = new Book
                {
                    Name       = row[0],
                    BookType   =  row[1],
                    Price      = int.Parse(row[2]),
                    PriceTypes = int.Parse(row[2]) < 50 ? PriceTypes.cheap : int.Parse(row[2]) > 200 ? PriceTypes.expensive : PriceTypes.moderate,
                    Store = row[3],
                    pages = int.Parse(row[4])
                };

                books.Add(transaction);
            }

            return books;
        }
    }

    public enum PriceTypes
    {
        cheap,
        moderate,
        expensive
    }

    public class Book
    {
        public int              _id;
        public String    Name     { get; set; }
        public String BookType { get; set; }
        public decimal         Price        { get; set; }
        public PriceTypes         PriceTypes        { get; set; }
        public int         pages        { get; set; }
        public String         Store        { get; set; }
        

        public override string ToString() => $"{Store} - {Name} - {BookType} - ${Price}";
    }

    public static class Data
    {

        public static TextFieldParser CsvParser()
        {
            try
            {
                var file = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName!, "Data.csv");

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

    
}