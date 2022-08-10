using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2Lib
{
    public class BinaryBookRepository : IBookRepository
    {
        private string _filePath;

        public BinaryBookRepository(string filePath)
        {
            _filePath = filePath;
        }

        public Book Create(Book book)  // Will create the file
        {
            try 
            {
                FileStream stream = new(_filePath, FileMode.Append);
                BinaryWriter writer = new(stream);
                writer.Write(book.Title);
                writer.Write(book.Cost);
                writer.Write(book.Author);
                writer.Close();


            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return book;
        }

       

        public Book? Read(string id)
        {
            /* Open File
                 * Read data while not end of file process data then end
                 */
            Book? book = null;
            try 
            { 
                FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read);  
                BinaryReader reader = new(stream);
                while(reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    string title = reader.ReadString();
                    double cost = reader.ReadDouble();
                    string author = reader.ReadString();
                    Console.WriteLine($"{title}");
                    if (title == id)
                    {
                        book = new()
                        {
                            Title = title,
                            Cost = cost,
                            Author = author,

                        };
                        break;  // No need to check any other records
                    }
                }
               
                reader.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return book;
        }

        public List<Book> ReadAll()
        {
            List<Book> books = new ();
            try
            {
                FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read);

                BinaryReader reader = new(stream);

                /* will Open File
                 * Check for end-of-file
                 * Read data While not end of file process data then end
                 */

                while(reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Book book = new()
                    {
                        Title = reader.ReadString(),
                        Cost = reader.ReadDouble(),
                        Author = reader.ReadString(),
                    };
                    books.Add(book);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return books;
        }

        public void Delete(string id)
        {
            try
            {
                //Open temp file for writing
                string tempFilename = _filePath + ".temp";
                FileStream tempstream = new(tempFilename, FileMode.Append);
                BinaryWriter writer = new(tempstream);
                //Open file for reading

                FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new(stream);
                //Read a record not necessary here
                //While not EOF
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    string title = reader.ReadString();
                    double cost = reader.ReadDouble();
                    string author = reader.ReadString();
                    Console.WriteLine($"{title}");
                    if (title != id)
                    {
                        writer.Write(title);
                        writer.Write(cost);
                        writer.Write(author);
                    }

                }

                reader.Close();
                writer.Close();

                File.Delete(_filePath);
                File.Move(tempFilename, _filePath); //Rename


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update(string oldId, Book book)
        {
            //Open temp file for writing
            string tempFilename = _filePath + ".temp";
            FileStream tempstream = new(tempFilename, FileMode.Append);
            BinaryWriter writer = new(tempstream);
            //Open file for reading

            FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new(stream);
            //Read a record not necessary here
            //While not EOF
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                string title = reader.ReadString();
                double cost = reader.ReadDouble();
                string author = reader.ReadString();
                Console.WriteLine($"{title}");
                if (title != oldId)
                {
                    writer.Write(title);
                    writer.Write(cost);
                    writer.Write(author);
                }
                else 
                {
                    writer.Write(book.Title);
                    writer.Write(book.Cost);
                    writer.Write(book.Author);
                }
               
            }

            reader.Close();
            writer.Close();

            File.Delete(_filePath);
            File.Move(tempFilename, _filePath); //Rename

        }
    }
}
