

namespace project2Lib
{

    public class BookTitleComparer : IComparer<Book> //Will be used to sort books by title
    {
        public int Compare(Book? x, Book? y)
        {
            if(x != null && y != null) 
            { return x.Title.CompareTo(y.Title); }
            return 1;
        }
    }

    public class BookAuthorNameComparer : IComparer<Book> //Will be used to sort books by author
    {
        public int Compare(Book? x, Book? y)
        {
            if (x != null && y != null) { return x.Author.CompareTo(y.Author); }
            return 1;
                
        }
    }

    public class BookPriceComparer : IComparer<Book> // will be used to compare books by prices
    {
        public int Compare(Book? x, Book? y)
        {
            if (x != null && y != null) { return x.Cost.CompareTo(y.Cost); }
            return 1;
        }
    }


    public class Book
    {
        private double _cost;
        public string Title { get; set; } = String.Empty;

        public string Author { get; set; } = String.Empty;

        public double Cost
        {
            get { return _cost; }

            set
            {
                if (value <= 0 || value >= 9999)  // Exception message for cost: Cost must be between 0 and 9999
                {
                    throw new InvalidCostException("The cost must be between 0 and 9999!");
                }
                _cost = value;
            }
        }

        public double Tax(TaxRate taxrate) //This is the Dependency Relationship from book to tax rate
        {
            return taxrate.Rate * Cost;

        }

        public double CostWithTax(TaxRate taxrate)    //Another Dependency Relationship from book to tax rate
        {
            return (taxrate.Rate * Cost) + Cost;
        }

        public override string ToString()
        {
            return $"Title :{Title}  Author: {Author}  Cost:{Cost.ToString("C")} ";
        }




    }
}