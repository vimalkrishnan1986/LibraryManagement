using LibraryManagement.Domain.Models;

namespace LibraryManagement.Business.Contracts
{
    public interface IBookBusinessService
    {
        Task<BookDomainModel> AddAsync(BookDomainModel bookDomainModel);
        Task<BookDomainModel> GetByIdAsync(Guid resourceId);
        Task<IEnumerable<BookDomainModel>> GetAllAsync();
    }
}
