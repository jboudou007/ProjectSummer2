/* Jean Bilong 
 * CSCI-1260-090
   Project Summer 2
   Last Date of change 8/10/2022*/

using project2Lib;

string userAnswer;



IBookRepository repo = new BinaryBookRepository("Books.data");

//ITaxRepository repo2 = new BinaryTaxRepository("Tax.data");

IComparer<Book> comparer = new BookTitleComparer();
string compareHow = "ByTitle";



List<Book> books = repo.ReadAll();

TaxRate DefaultRate = new TaxRate() { Rate = .1 };



do 
{
    books = repo.ReadAll();   /// Will reload the file and list
    if(compareHow == "ByTitle") comparer = new BookTitleComparer();
    else if(compareHow == "ByAuthor") comparer = new BookAuthorNameComparer();
    else comparer = new BookPriceComparer();
    books.Sort(comparer);
    Console.WriteLine(comparer);
    ShowAllBooks(books, DefaultRate);
    Console.WriteLine(new String('-', 80));
    Console.WriteLine();
    ShowMainMenu();
    userAnswer = ReadString("Enter your choice:  ");
    switch (userAnswer.ToUpper())
    {
        case "N":
            repo.Create(CreateBook());
            break;

        case "S":
            LookForBook(repo);
            Thread.Sleep(3000);  //to slow down the app
            break;

        case "R":
            DeleteBook(repo);
            Thread.Sleep(5000); //to slow down the app
            break;
        case "U":
            UpdateBook(repo);
            Thread.Sleep(3000);  //to slow down the app
            break;

        case "T":
            UpdateCurrentRate(DefaultRate);
            break;
        case "P":
            compareHow = "ByPrice";
            break;
        case "L":
            compareHow = "ByTitle";
            break;
        case "H":
            compareHow = "ByAuthor";
            break;
    }
    
    
}while (userAnswer.ToUpper() != "X");



static Book CreateBook()  //Instantiate Book with his three parameters: Title, Cost, Author
{
    Book book = new ();
    book.Title = ReadString("Enter Title: ");
    book.Author = ReadString("Enter Author's name: ");
    EnterCost(book);
    return book;
}


static void EnterCost(Book book)  //Method to enter the cost of the book
{
    do 
    {
        double cost = ReadDouble("Enter the cost: ");
        try 
        {
            book.Cost = cost;
            break;
        } catch(InvalidCostException ex) //Invalid Cost Exception class
        {
            Console.WriteLine(ex.Message);
        }

    } while (true);

}

static void ShowAllBooks(List<Book> books, TaxRate taxRate)
{
    //Book Menu
    Console.WriteLine();
    Console.WriteLine(new String('-', 80));
    string h1 = "Title";
    string h2 = "Cost";
    string h3 = "Author";
    Console.WriteLine($"{h1,-30} {h2,4} {h3,30} ");
    Console.WriteLine(new String('-', 80));
    foreach(Book book in books)
    {
        Console.WriteLine($"{book.Title, -30} {book.Cost.ToString("C"), 4} {book.Author, 30}");
    }
    //Tax Book Menu
    string h4 = "Total Cost";
    string h5 = "Total Tax";
    string h6 = "Total Cost with Tax";
    Console.WriteLine(new String('-', 80));
    Console.WriteLine($"{h4,-30} {SumOfCost(books).ToString("C"), 4}\n"+ 
                      $"\n{h5,-30} {SumOfTax(books, taxRate).ToString("C"), 4}\n" 
                    + $"\n{h6,-30} {SumOfCostWithTax(books, taxRate).ToString("C"),4}\n");

    //Tax Menu
    Console.WriteLine($"press T to update Tax Rate. Current: {taxRate.Rate.ToString("0.00 % ")}");
  




}


static void ShowMainMenu() //This is the main menu
{
    Console.WriteLine($"Enter your choice\n N = add new book\n R = remove book\n S = search book\n" +
                      $" R = delete book\n U = update book\n P = Sort By Price\n " +
                      $"L = Sort By Title\n H = Sort By Author\n  X = Exit\n");
}






static double ReadDouble(string promptUser)  //Method to read a double that can handle any exceptions
{
    double value;

    do
    {
        Console.Write(promptUser);
        string? valueStr = Console.ReadLine();
        if (valueStr != null)
        {
            try   // try catch block to handle exceptions
            {
                value = double.Parse(valueStr);
                break;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);

            }

        }
    } while (true);
    return value;

}



static string ReadString(string promptUser)  //Method to read all the string the user entered
{
    string value = "";
    Console.Write(promptUser);
    string? valueStr = Console.ReadLine();
    if (valueStr != null)
    {
        value = valueStr.Trim();

    }
    return value;

}

static void LookForBook(IBookRepository repo)   //Method to search a book from it's title using Read method from IRepository
{
    var bookTitle = ReadString("Enter the title you are looking for ");

    var book = repo.Read(bookTitle);

    if (book != null)
    {
        Console.WriteLine(book);
        
    }
    else
    {
        Console.WriteLine($"{bookTitle} was not found");
        
    }

}

static void DeleteBook(IBookRepository repo)  //Delete Book from file method
{
    var bookTitle = ReadString("Enter the title you are looking for ");

    var book = repo.Read(bookTitle);

    if (book != null)
    {
        Console.WriteLine($"Do you want to remove {bookTitle}? Y or N");  //Will confirm if the user wants to perform this action if not he will be sent to main menu
        string? answer = Convert.ToString(Console.ReadLine());
        if(answer == "Y" || answer == "y")
        {
            repo.Delete(bookTitle);
            Console.WriteLine($"{bookTitle} was removed");
        }
        if(answer == "N" || answer == "n")
        {

            Console.WriteLine($"{bookTitle} was not removed");
        }
        
    }
    else
    {
        Console.WriteLine($"{bookTitle} was not found");
    }
}

static void UpdateBook(IBookRepository repo)
{
    var bookTitle = ReadString("Enter the title you are looking for ");

    var book = repo.Read(bookTitle);

    if (book != null)
    {
        Book newBook = CreateBook();    //Will create new book
        repo.Update(bookTitle, newBook);    // will delete the old one and replace with new one
        Console.WriteLine($"{bookTitle} was removed");
    }
    else
    {
        Console.WriteLine($"{bookTitle} was not found");
    }

}

static double SumOfCost(List<Book> books)   //Method to calculate the total cost without tax
{
    double sum = 0;
    
    for (int i = 0; i < books.Count; i++)
    {
        sum += books[i].Cost;
    }

    return sum;
   
}

static double SumOfTax(List<Book> books, TaxRate taxRate)   //Method to calculate total tax
{
    double sum = 0;

    for (int i = 0; i < books.Count; i++)
    {
        sum += books[i].Tax(taxRate);
    }

    return sum;

}

static double SumOfCostWithTax(List<Book> books, TaxRate taxRate)   //Method to calculate total cost with tax
{
    double sum = 0;

    for (int i = 0; i < books.Count; i++)
    {
        sum += books[i].CostWithTax(taxRate);
    }

    return sum;

}

static void UpdateCurrentRate(TaxRate taxRate)     //Method to update the tax Rate. Note: All the exceptions will be handled.
{
    do 
    {
        var newTaxRate = ReadDouble("Enter the new tax rate  ");
 
        try 
        {
                
            taxRate.UpdateRate(newTaxRate);
            Console.WriteLine($"The tax rate was changed.");
            Thread.Sleep(3000);
            break;

        }
        catch (InvalidTaxException ex) 
        {
            Console.WriteLine(ex.Message);
        }
        

    } 
    while(true);
}












