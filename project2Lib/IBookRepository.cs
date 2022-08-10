using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2Lib;
/// <summary>
/// CRRUD
/// </summary>

public interface IBookRepository
{
    Book Create(Book book);
    List<Book> ReadAll();

    Book? Read(string id);

    void Update(string oldId, Book book);

    void Delete(string id);


}
