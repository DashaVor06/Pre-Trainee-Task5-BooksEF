using Data.Database;
using Data.Models;

namespace Data.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        public List<Book> GetAll()
        {
            return DatabaseClass.BooksTable;
        }
        public Book GetById(int id)
        {
            int i = 0;
            while (i < DatabaseClass.BooksTable.Count && DatabaseClass.BooksTable[i].Id != id)
            {
                i++;
            }
            return DatabaseClass.BooksTable[i];
        }
        public Book Create(Book book)
        {
            DatabaseClass.BooksTable.Add(book);
            return book;
        }
        public Book Update(Book book)
        {   
            for (int i = 0; i < DatabaseClass.BooksTable.Count; i++)
            {
                if (DatabaseClass.BooksTable[i].Id == book.Id)
                {
                    DatabaseClass.BooksTable[i] = book;
                }
            }
            return book;
        }
        public void Delete(int id)
        {
            for (int i = 0; i < DatabaseClass.BooksTable.Count; i++)
            {
                if (DatabaseClass.BooksTable[i].Id == id)
                {
                    DatabaseClass.BooksTable.Remove(DatabaseClass.BooksTable[i]);
                }
            }
        }
    }
}
