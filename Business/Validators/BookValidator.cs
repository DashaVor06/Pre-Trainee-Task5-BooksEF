using Data.Models;

namespace Business.Validators
{
    public static class BookValidator
    {
        public static bool CheckTitle(string? title)
        {
            return !string.IsNullOrEmpty(title);
        }
        public static bool CheckPublishedYear(int year)
        {
            return (year >= 0 && year <= DateTime.Now.Year);
        }
        public static bool CheckId(List<Book> list, int id)
        {
            return (id > 0 && id > list.Last().Id);
        }
        public static bool CheckIdExists(List<Book> list, int id)
        {
            foreach (Book book in list)
            {
                if (book.Id == id)
                    return true;
            }
            return false;
        }
        public static bool CheckAuthorId(List<Author> list, int id)
        {
            foreach (Author author in list)
            {
                if (author.Id == id)
                    return true;
            }
            return false;
        }
        public static bool CheckBookForCreate(List<Book> listBooks, List<Author> listAuthors,Book? book)
        {
            return (book != null && CheckTitle(book.Title) && CheckPublishedYear(book.PublishedYear) && CheckId(listBooks, book.Id) && CheckAuthorId(listAuthors, book.AuthorId));
        }
        public static bool CheckBookForUpdateDelete(List<Book> listBooks, List<Author> listAuthors, Book? book)
        {
            return (book != null && CheckTitle(book.Title) && CheckPublishedYear(book.PublishedYear) && CheckIdExists(listBooks, book.Id) && CheckAuthorId(listAuthors, book.AuthorId));
        }
    }
}
