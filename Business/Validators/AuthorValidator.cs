using Data.Models;

namespace Business.Validators
{
    public static class AuthorValidator
    {
        public static bool CheckNameExists(List<Author> list, string? name)
        {
            foreach (Author author in list)
            {
                if (author.Name == name)
                    return true;
            }
            return false;       
        }
        public static bool CheckName(string? name)
        {
            return !string.IsNullOrEmpty(name);
        }
        public static bool CheckDateOfBirth(DateTime date)
        {
            return (date < DateTime.Now && date > new DateTime(1500, 1, 1));
        }
        public static bool CheckId(List<Author> list, int id)
        {
            return (id > list.Last().Id);
        }
        public static bool CheckIdExists(List<Author> list, int id)
        {
            foreach (Author author in list)
            {
                if (author.Id == id)
                    return true;
            }
            return false;
        }
        public static bool CheckAuthorForCreate( List<Author> listAuthors, Author? author)
        {
            return (author != null && CheckName(author.Name) && !CheckNameExists(listAuthors, author.Name) && CheckId(listAuthors, author.Id));
        }
        public static bool CheckAuthorForUpdateDelete(List<Author> listAuthors, Author? author)
        {
            return (author != null && CheckName(author.Name) && CheckNameExists(listAuthors, author.Name) && CheckIdExists(listAuthors, author.Id));
        }
    }
}
