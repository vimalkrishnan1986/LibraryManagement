using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Models
{
    public class BookDomainModel
    {
        [Required, MaxLength(50), MinLength(1)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(50), MinLength(1)]
        public string AuthorName { get; set; } = null!;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ResourceId { get; set; } = null;
    }
}