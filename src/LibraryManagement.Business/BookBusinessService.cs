using LibraryManagement.Business.Contracts;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Business
{
    public class BookBusinessService : IBookBusinessService
    {
        public Task<BookDomainModel> AddAsync(BookDomainModel bookDomainModel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookDomainModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookDomainModel> GetByIdAsync(Guid resourceId)
        {
            throw new NotImplementedException();
        }
    }
}
