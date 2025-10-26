using Data.Models;

namespace Data.Database
{
    public static class DatabaseClass
    {
        public static List<Book> BooksTable { get; set; } = new List<Book>
        {
            new Book { Id = 1, Title = "Война и мир", PublishedYear = 1869, AuthorId = 1 },
            new Book { Id = 2, Title = "Анна Каренина", PublishedYear = 1877, AuthorId = 1 },
            new Book { Id = 3, Title = "Преступление и наказание", PublishedYear = 1866, AuthorId = 2 },
            new Book { Id = 4, Title = "Идиот", PublishedYear = 1869, AuthorId = 2 },
            new Book { Id = 5, Title = "Отцы и дети", PublishedYear = 1862, AuthorId = 3 },
            new Book { Id = 6, Title = "Записки охотника", PublishedYear = 1852, AuthorId = 3 }
        };
        public static List<Author> AuthorsTable { get; set; } = new List<Author>()
        {
            new Author { Id = 1, Name = "Лев Толстой", DateOfBirth = new DateTime(1828, 9, 9) },
            new Author { Id = 2, Name = "Фёдор Достоевский", DateOfBirth = new DateTime(1821, 11, 11) },
            new Author { Id = 3, Name = "Иван Тургенев", DateOfBirth = new DateTime(1818, 11, 9) }
        };
    }
}
