using Data.Models;
using Data.Database;

namespace Data.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        public List<Author> GetAll()
        {
            return DatabaseClass.AuthorsTable;
        }
        public Author GetById(int id)
        {
            int i = 0;
            while (i < DatabaseClass.AuthorsTable.Count && DatabaseClass.AuthorsTable[i].Id != id)
            {
                i++;
            }
            return DatabaseClass.AuthorsTable[i];
        }
        public Author Create(Author author)
        {
            DatabaseClass.AuthorsTable.Add(author);
            return author;
        }
        public Author Update(Author author)
        {
            for (int i = 0; i < DatabaseClass.AuthorsTable.Count; i++)
            {
                if (DatabaseClass.AuthorsTable[i].Id == author.Id)
                {
                    DatabaseClass.AuthorsTable[i] = author;
                }
            }
            return author;
        }
        public void Delete(int id)
        {
            for (int i = 0; i < DatabaseClass.AuthorsTable.Count; i++)
            {
                if (DatabaseClass.AuthorsTable[i].Id == id)
                {
                    DatabaseClass.AuthorsTable.Remove(DatabaseClass.AuthorsTable[i]);
                }
            }
        }
    }
}
