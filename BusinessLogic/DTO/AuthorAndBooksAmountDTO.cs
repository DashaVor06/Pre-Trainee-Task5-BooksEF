namespace BusinessLogic.DTO
{
    public class AuthorAndBooksAmountDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int BooksAmount { get; set; }
    }
}
