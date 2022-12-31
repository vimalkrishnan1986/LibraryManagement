namespace LibraryManagement.Domain.Models
{
    public class BookDomainModel
    {
        public string Name { get; set; } = null!;

        public string AuthorName { get; set; } = null!;

        public Guid ResourceId { get; set; }
    }
}