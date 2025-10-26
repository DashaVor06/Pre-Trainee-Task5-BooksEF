namespace Business.DTO
{
    public class BookAndAuthorDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
