namespace LibraryManagement.Data.Models
{
    public class BookDataModel : IDataModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public Guid ResourceId { get; set; }
    }
}
