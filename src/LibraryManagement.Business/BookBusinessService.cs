using AutoMapper;
using LibraryManagement.Business.Contracts;
using LibraryManagement.Data.Models;
using LibraryManagement.Data.Repositories;
using LibraryManagement.Domain.Models;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Business
{
    public class BookBusinessService : IBookBusinessService
    {
        private readonly IRepository<BookDataModel> _bookRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public BookBusinessService(ILogger logger, IRepository<BookDataModel> bookRepsitory, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookRepository = bookRepsitory ?? throw new ArgumentNullException(nameof(bookRepsitory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<BookDomainModel> AddAsync(BookDomainModel bookDomainModel)
        {
            if (bookDomainModel == null)
            {
                throw new ArgumentException(nameof(bookDomainModel));
            }
            var dataModel = _mapper.Map<BookDataModel>(bookDomainModel);
            dataModel.ResourceId = bookDomainModel.ResourceId ?? Guid.NewGuid();
            await _bookRepository.InsertAsync(dataModel).ConfigureAwait(false);
            return _mapper.Map<BookDomainModel>(dataModel);
        }

        public async Task<IEnumerable<BookDomainModel>> GetAllAsync()
        {
            var items = await _bookRepository.GetAllAsync().ConfigureAwait(false);
            if (items == null || !items.Any())
            {
                return Enumerable.Empty<BookDomainModel>();
            }
            return items.Select(x => _mapper.Map<BookDomainModel>(x));
        }

        public async Task<BookDomainModel> GetByIdAsync(Guid resourceId)
        {
            var model = (await _bookRepository
                .GetByFilterAsync(m => m.ResourceId == resourceId).ConfigureAwait(false)).FirstOrDefault();
            if (model == null)
            {
                throw new ArgumentException("Invalid resourceId");
            }
            return _mapper.Map<BookDomainModel>(model);
        }
    }
}
